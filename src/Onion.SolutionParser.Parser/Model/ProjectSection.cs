using System.Collections.Generic;

namespace Onion.SolutionParser.Parser.Model
{
    public class ProjectSection
    {
        public ProjectSection(string name, ProjectSectionType sectionType)
        {
            Name = name;
            Type = sectionType;
            Entries = new Dictionary<string, string>();
        }

        public string Name { get; set; }
        public ProjectSectionType Type { get; set; }
        public IDictionary<string, string> Entries { get; set; }
    }

    public enum ProjectSectionType
    {
        PostProject,
        PreProject
    }
}
