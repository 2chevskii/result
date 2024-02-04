using Nuke.Common;
using Nuke.Common.IO;

interface IHazArtifacts : INukeBuild
{
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";
    AbsolutePath PackagesDirectory => ArtifactsDirectory / "pkg";
    AbsolutePath LibrariesDirectory => ArtifactsDirectory / "lib";
    AbsolutePath TestResultsDirectory => ArtifactsDirectory / "test_results";
    AbsolutePath CoverageDirectory => TestResultsDirectory / "coverage";
    AbsolutePath DocsArtifactsDirectory => ArtifactsDirectory / "docs";

    void InitializeArtifactsDirectories()
    {
        ArtifactsDirectory.CreateDirectory();
        PackagesDirectory.CreateDirectory();
        LibrariesDirectory.CreateDirectory();
        DocsArtifactsDirectory.CreateDirectory();
        TestResultsDirectory.CreateDirectory();
        CoverageDirectory.CreateDirectory();
    }
}
