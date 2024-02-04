using Nuke.Common;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;

interface IPack : ICompile
{
    Target Pack =>
        _ =>
            _.DependsOn(CompileMain)
                .Executes(
                    () =>
                        DotNetTasks.DotNetPack(settings =>
                            settings
                                .EnableNoBuild()
                                .SetVersion(Version.SemVer)
                                .SetOutputDirectory(PackagesDirectory)
                                .SetConfiguration(Configuration)
                                .CombineWith(
                                    MainProjects,
                                    (settings, project) => settings.SetProject(project)
                                )
                        )
                );
}
