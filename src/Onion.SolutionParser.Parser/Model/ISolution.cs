using System.Collections.Generic;

namespace Onion.SolutionParser.Parser.Model
{
    public interface ISolution
    {
        IEnumerable<string> Header { get; }
        IEnumerable<GlobalSection> Global { get; }
        IEnumerable<Project> Projects { get; }
    }
}
