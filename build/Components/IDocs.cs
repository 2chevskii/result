using System.Collections.Generic;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.Npm;
using Nuke.Common.Utilities;

interface IDocs : IHazArtifacts, IRestore, IHazVersion, IHazGitHubRelease
{
    AbsolutePath DocsDirectory => RootDirectory / "docs";
    AbsolutePath DocsPackageJson => DocsDirectory / "package.json";
    AbsolutePath DocsDistDirectory => DocsDirectory / ".vitepress/dist";

    Target Docs => _ => _.DependsOn(DocsCompile, DocsCopy);

    Target DocsClean =>
        _ =>
            _.Executes(
                () => DocsDistDirectory.DeleteDirectory(),
                () => ArtifactPaths.Docs.CreateOrCleanDirectory()
            );

    Target DocsRestore =>
        _ =>
            _.Executes(
                () =>
                    NpmTasks.NpmInstall(settings =>
                        settings.SetProcessWorkingDirectory(DocsDirectory)
                    )
            );

    Target PatchDocsPackageJson =>
        _ =>
            _.Executes(() =>
            {
                Dictionary<string, object> props = DocsPackageJson.ReadJson<
                    Dictionary<string, object>
                >();
                props["version"] = Version.SemVer;
                props["latestReleaseVersion"] = LatestGitHubReleaseVersion.OriginalVersion;
                DocsPackageJson.WriteJson(props);
            });

    Target DocsDev =>
        _ =>
            _.DependsOn(DocsRestore, PatchDocsPackageJson)
                .Executes(
                    () =>
                        NpmTasks.NpmRun(settings =>
                            settings
                                .SetProcessWorkingDirectory(DocsDirectory)
                                .SetCommand("docs:dev")
                        )
                );

    Target DocsCompile =>
        _ =>
            _.DependsOn(DocsRestore, PatchDocsPackageJson)
                .Executes(
                    () =>
                        NpmTasks.NpmRun(settings =>
                            settings
                                .SetProcessWorkingDirectory(DocsDirectory)
                                .SetCommand("docs:build")
                        )
                );

    Target DocsPreview =>
        _ =>
            _.DependsOn(DocsCompile)
                .Executes(
                    () =>
                        NpmTasks.NpmRun(settings =>
                            settings
                                .SetProcessWorkingDirectory(DocsDirectory)
                                .SetCommand("docs:preview")
                        )
                );

    Target DocsCopy =>
        _ =>
            _.DependsOn(DocsCompile)
                .Executes(
                    () => ArtifactPaths.Docs.CreateOrCleanDirectory(),
                    () =>
                        FileSystemTasks.CopyDirectoryRecursively(
                            DocsDistDirectory,
                            ArtifactPaths.Docs,
                            DirectoryExistsPolicy.Merge
                        )
                );
}
