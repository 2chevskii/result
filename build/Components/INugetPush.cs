using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities;
using Octokit;
using FileMode = System.IO.FileMode;

interface INugetPush : INukeBuild, ICreateGitHubRelease, IControlNuGetSources
{
    string NugetApiKey => EnvironmentInfo.GetVariable("NUGET_API_KEY");

    Target NugetPush =>
        _ =>
            _.Requires(() => !string.IsNullOrEmpty(NugetApiKey))
                .Requires(() => NugetFeed)
                .DependsOn(EnsureHasNugetFeed)
                .Executes(
                    () =>
                        DotNetTasks.DotNetNuGetPush(settings =>
                            settings
                                .SetSource(NugetSourceName)
                                .SetApiKey(NugetApiKey)
                                .CombineWith(
                                    ArtifactPaths.Packages.GlobFiles("*.nupkg"),
                                    (settings, package) => settings.SetTargetPath(package)
                                )
                        )
                );

    Target DownloadReleaseAssets =>
        _ =>
            _.Executes(async () =>
            {
                Release release = await GetOrCreateRelease();

                IEnumerable<Task> downloadTasks =
                    from asset in release.Assets
                    let name = asset.Name
                    where name.EndsWith("nupkg")
                    let path = ArtifactPaths.Packages / name
                    select HttpTasks.HttpDownloadFileAsync(
                        asset.BrowserDownloadUrl,
                        path,
                        FileMode.CreateNew
                    );

                await Task.WhenAll(downloadTasks);
            });
}
