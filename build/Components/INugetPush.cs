using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitHub;
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
                long repositoryId = await GetRepositoryId();
                Release release = await GitHubTasks.GitHubClient.Repository.Release.Get(
                    repositoryId,
                    TagName
                );

                AbsolutePath GetArtifactPath(string artifactName)
                {
                    return ArtifactPaths.Packages / artifactName;
                }

                IEnumerable<Task> downloadTasks = release
                    .Assets.Where(asset => asset.Name.EndsWith("nupkg"))
                    .Select(asset =>
                    {
                        var path = GetArtifactPath(asset.Name);
                        return HttpTasks.HttpDownloadFileAsync(asset.BrowserDownloadUrl, path);
                    });

                await Task.WhenAll(downloadTasks);
            });
}
