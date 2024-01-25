using Nuke.Common;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;

interface ITest : ICompile
{
    Target Test =>
        _ =>
            _.DependsOn(CompileTests)
                .Executes(
                    () =>
                        DotNetTasks.DotNetTest(settings =>
                            settings.Apply(TestSettingsBase).SetProjectFile(TestsProject)
                        )
                );

    Configure<DotNetTestSettings> TestSettingsBase =>
        settings => settings.EnableNoBuild().SetConfiguration(Configuration);
}
