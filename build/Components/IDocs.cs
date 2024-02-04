using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Serilog;

interface IDocs : IHazSlnFiles, IHazArtifacts, IRestore
{
    AbsolutePath DocsDirectory => RootDirectory / "docs";
    AbsolutePath DocfxConfig => DocsDirectory / "docfx.json";
    AbsolutePath DocsDistDirectory => DocsDirectory / "dist";
    AbsolutePath DocsGeneratedApiDirectory => DocsDirectory / "api";
    AbsolutePath DocsArtifactPath => DocsArtifactDirectory / "github-pages.tar";

    Target Docs => _ => _.DependsOn(DocsCompile, DocsGzip);

    Target DocsPreview =>
        _ =>
            _.DependsOn(DocsCompile)
                .Executes(() => DotNetTasks.DotNet($"docfx serve {DocsDistDirectory}"));

    Target DocsClean =>
        _ =>
            _.Executes(
                () => DocsArtifactDirectory.DeleteDirectory(),
                () => DocsGeneratedApiDirectory.DeleteDirectory(),
                () => DocsDistDirectory.DeleteDirectory()
            );

    Target DocsCompile =>
        _ =>
            _.DependsOn(RestoreTools)
                .Executes(
                    () => Log.Information("Generating documentation website..."),
                    () =>
                    {
                        Log.Debug("Generating metadata files...");
                        DotNetTasks.DotNet($"docfx metadata {DocfxConfig}");
                    },
                    () =>
                    {
                        Log.Verbose("Generating HTML...");
                        DotNetTasks.DotNet($"docfx build {DocfxConfig}");
                    }
                );

    Target DocsGzip =>
        _ =>
            _.DependsOn(DocsCompile)
                .Executes(() =>
                {
                    Log.Information(
                        "Compressing documentation website files to {ArtifactPath}",
                        DocsArtifactPath
                    );
                    DocsArtifactPath.Parent.CreateOrCleanDirectory();
                    ProcessTasks.StartShell(
                        $"tar --dereference --directory {DocsDistDirectory} -cvf {DocsArtifactPath} ."
                    );
                });
}
