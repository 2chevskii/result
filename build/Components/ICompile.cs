﻿using System.Linq;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;

interface ICompile : IRestore, IHazVersion, IHazConfiguration, IHazArtifacts
{
    [Parameter]
    bool CopyLibs => TryGetValue<bool?>(() => CopyLibs).GetValueOrDefault();

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

    Target CopyLibraryArtifacts =>
        _ =>
            _.OnlyWhenStatic(() => CopyLibs)
                .TriggeredBy(CompileMain)
                .OnlyWhenStatic(() => Configuration == Configuration.Release)
                .Unlisted()
                .Executes(
                    () =>
                        MainProjects.ForEach(project =>
                            FileSystemTasks.CopyDirectoryRecursively(
                                project.Directory
                                    / "bin"
                                    / Configuration
                                    / project.GetTargetFrameworks()!.First(),
                                ArtifactPaths.Libraries
                                    / $"{project.Name}.{project.GetTargetFrameworks()!.First()}",
                                DirectoryExistsPolicy.Merge,
                                FileExistsPolicy.Overwrite
                            )
                        )
                );

    Configure<DotNetBuildSettings> BuildSettingsBase =>
        settings =>
            settings
                .EnableNoRestore()
                .EnableNoDependencies()
                .SetConfiguration(Configuration)
                .SetVersion(Version.SemVer);
}
