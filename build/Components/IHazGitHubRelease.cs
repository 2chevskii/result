using NuGet.Versioning;
using Nuke.Common;
using Nuke.Common.Tooling;

interface IHazGitHubRelease : INukeBuild
{
    [LatestGitHubRelease("2chevskii/result", TrimPrefix = true)]
    NuGetVersion LatestGitHubReleaseVersion =>
        NuGetVersion.Parse(TryGetValue<string>(() => LatestGitHubReleaseVersion) ?? "0.0.0");
}
