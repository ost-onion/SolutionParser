using System;

namespace Onion.SolutionParser.Parser.Model
{
    public class Project : ISolutionItem
    {
        public Project(Guid typeGuid, string name, string path, Guid guid)
        {
            TypeGuid = typeGuid;
            Name = name;
            Path = path;
            Guid = guid;
        }

        public Guid TypeGuid { get; private set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Guid Guid { get; private set; }
        public ProjectSection ProjectSection { get; set; }
    }
}
