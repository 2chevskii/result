using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Utilities.Collections;

interface IHazArtifacts : INukeBuild
{
    ArtifactPathCollection ArtifactPaths =>
        new ArtifactPathCollection { Root = RootDirectory / "artifacts" };

    void InitializeArtifactsDirectories() =>
        ArtifactPaths.All.ForEach(x => x.CreateDirectory());

    class ArtifactPathCollection
    {
        public AbsolutePath Root { get; init; }
        public AbsolutePath Packages => Root / "pkg";
        public AbsolutePath Libraries => Root / "lib";
        public AbsolutePath Docs => Root / "docs";
        public AbsolutePath TestResults => Root / "test_results";
        public AbsolutePath Coverage => TestResults / "coverage";
        public AbsolutePath[] All => [Root, Packages, Libraries, Docs, TestResults, Coverage];
    }
}
