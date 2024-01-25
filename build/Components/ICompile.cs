using Nuke.Common;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;

interface ICompile : IRestore, IHazVersion, IHazConfiguration
{
    Target CompileMain =>
        _ =>
            _.DependsOn(Restore)
                .Executes(
                    () =>
                        DotNetTasks.DotNetBuild(settings =>
                            settings.Apply(BuildSettingsBase).SetProjectFile(MainProject)
                        )
                );

    Target CompileTests =>
        _ =>
            _.DependsOn(Restore, CompileMain)
                .Executes(
                    () =>
                        DotNetTasks.DotNetBuild(settings =>
                            settings.Apply(BuildSettingsBase).SetProjectFile(TestsProject)
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