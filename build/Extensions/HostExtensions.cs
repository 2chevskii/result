using Nuke.Common;
using Nuke.Common.CI.GitHubActions;

public static class HostExtensions
{
    public static bool IsGitHubActions(this Host host) => host is GitHubActions;
}
