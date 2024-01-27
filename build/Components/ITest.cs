using Nuke.Common;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;

interface ITest : ICompile
{
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
                                    (settings, project) => settings.SetProjectFile(project)
                                )
                        )
                );

    Configure<DotNetTestSettings> TestSettingsBase =>
        settings => settings.EnableNoBuild().SetConfiguration(Configuration);
}
