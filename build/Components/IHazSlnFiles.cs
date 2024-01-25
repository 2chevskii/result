using System.Linq;
using Nuke.Common;
using Nuke.Common.ProjectModel;

interface IHazSlnFiles : INukeBuild
{
    [Solution, Required]
    Solution Sln => TryGetValue(() => Sln);

    Project MainProject => Sln.GetAllProjects("Dvchevskii.Result").First();
    Project TestsProject => Sln.GetAllProjects("Dvchevskii.Result.Tests").First();
}