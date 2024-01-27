using Nuke.Common;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;

interface ICompile : IRestore, IHazVersion, IHazConfiguration, IHazArtifacts
{
    Target CompileMain =>
        _ =>
            _.DependsOn(Restore)
                .Executes(
                    () =>
                        DotNetTasks.DotNetBuild(settings =>
                            settings
                                .Apply(BuildSettingsBase)
                                .CombineWith(
                                    MainProjects,
                                    (settings, project) => settings.SetProjectFile(project)
                                )
                        )
                );

    Target CompileTests =>
        _ =>
            _.DependsOn(Restore, CompileMain)
                .Executes(
                    () =>
                        DotNetTasks.DotNetBuild(settings =>
                            settings
                                .Apply(BuildSettingsBase)
                                .CombineWith(
                                    TestProjects,
                                    (settings, project) => settings.SetProjectFile(project)
                                )
                        )
                );

    Target Compile => _ => _.DependsOn(CompileMain, CompileTests);

    Configure<DotNetBuildSettings> BuildSettingsBase =>
        settings =>
            settings
                .EnableNoRestore()
                .EnableNoDependencies()
                .SetConfiguration(Configuration)
                .SetVersion(Version.SemVer);
}
