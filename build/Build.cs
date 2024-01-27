using Components;
using Nuke.Common;

class Build : NukeBuild, IPack, IDocs, IClean
{
    public static int Main() => Execute<Build>(x => x.From<ICompile>().CompileMain);

    protected override void OnBuildInitialized() =>
        From<IHazArtifacts>().InitializeArtifactsDirectories();

    T From<T>()
        where T : class => this as T;
}
