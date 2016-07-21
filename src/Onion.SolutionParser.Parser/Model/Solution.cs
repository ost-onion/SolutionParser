using System.Collections.Generic;

namespace Onion.SolutionParser.Parser.Model
{
    public class Solution : ISolution
    {
        public IEnumerable<string> Header { get; set; }
        public IEnumerable<GlobalSection> Global { get; set; }
        public IEnumerable<Project> Projects { get; set; } 
    }
}
