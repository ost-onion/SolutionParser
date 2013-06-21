using System.Collections.Generic;
using Onion.SolutionParser.Parser.Model;

namespace Onion.SolutionParser.Parser
{
    public class ProjectParser
    {
        private readonly string _solutionContents;

        public ProjectParser(string solutionContents)
        {
            _solutionContents = solutionContents;
        }

        public IEnumerable<Project> Parse()
        {
            yield return null;
        }
    }
}
