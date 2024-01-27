using System.Linq;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.Coverlet;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.ReportGenerator;

interface ITest : ICompile, IHazArtifacts
{
    AbsolutePath CoverageDirectory => TestResultsDirectory / "coverage";

    Target Test =>
        _ =>
            _.DependsOn(CompileTests)
                .Executes(
                    () =>
                        DotNetTasks.DotNetTest(settings =>
                            settings
                                .Apply(TestSettingsBase)
                                .CombineWith(
                                    TestProjects,
                                    (settings, project) =>
                                        settings
                                            .Apply(TestSettingsLoggers(project))
                                            .SetProjectFile(project)
                                )
                        )
                );

    Target CollectCoverage =>
        _ =>
            _.DependsOn(CompileTests)
                .Executes(
                    () =>
                        CoverletTasks.Coverlet(settings =>
                            settings
                                .SetFormat("cobertura")
                                .SetTarget("dotnet")
                                .CombineWith(
                                    TestProjects,
                                    (settings, project) =>
                                        settings
                                            .Apply(CoverletSettingsProject(project))
                                            .SetOutput(CoveragePath(project))
                                )
                        )
                );

    Target ReportCoverage =>
        _ =>
            _.DependsOn(CollectCoverage)
                .Executes(
                    () =>
                        ReportGeneratorTasks.ReportGenerator(settings =>
                            settings.CombineWith(
                                TestProjects,
                                (settings, project) =>
                                    settings
                                        .SetReports(CoveragePath(project))
                                        .SetTargetDirectory(CoverageReportPath(project))
                            )
                        )
                );

    Target Coverage => _ => _.DependsOn(CollectCoverage, ReportCoverage);

    AbsolutePath CoverageReportPath(Project project) =>
        CoverageDirectory / $"report.{MainProjectName(project)}";

    AbsolutePath CoveragePath(Project project) =>
        CoverageDirectory / $"coverage.{MainProjectName(project)}.xml";

    string MainProjectName(Project testProject) => testProject.Name.Replace(".Tests", string.Empty);

    Configure<CoverletSettings> CoverletSettingsProject(Project project) =>
        settings =>
            settings
                .SetAssembly(ProjectDllPath(project))
                .SetTargetArgs($"test {project} --no-build")
                .SetInclude($"[{MainProjectName(project)}]*");

    AbsolutePath ProjectDllPath(Project project) =>
        project.Directory
        / "bin"
        / Configuration
        / project.GetTargetFrameworks()!.First()
        / $"{project.Name}.dll";

    Configure<DotNetTestSettings> TestSettingsBase =>
        settings => settings.EnableNoBuild().SetConfiguration(Configuration);

    Configure<DotNetTestSettings> TestSettingsLoggers(Project project) =>
        settings =>
            settings
                .AddLoggers("console;verbosity=detailed")
                .When(
                    HtmlTestResults,
                    settings =>
                        settings.AddLoggers($"html;logfilename=test-results.{project.Name}.html")
                )
                .When(
                    Host.IsGitHubActions(),
                    settings =>
                        settings.AddLoggers(
                            "GitHubActions;summary.includePassedTests=true;summary.includeSkippedTests=true"
                        )
                )
                .SetResultsDirectory(TestResultsDirectory);

    [Parameter]
    bool HtmlTestResults => TryGetValue<bool?>(() => HtmlTestResults).GetValueOrDefault();
}
