using System.Collections.Generic;
using System.Linq;
using Nuke.Common;
using Nuke.Common.ProjectModel;

interface IHazSlnFiles : INukeBuild
{
    [Solution, Required]
    Solution Sln => TryGetValue(() => Sln);

    ICollection<Project> MainProjects =>
        Sln.GetAllProjects("Dvchevskii.Result*")
            .Where(project => !project.Name.EndsWith(".Tests"))
            .ToArray();
    ICollection<Project> TestProjects => Sln.GetAllProjects("*.Tests").ToArray();
}
