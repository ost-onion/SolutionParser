using System.Collections.Generic;
using System.IO;

namespace Onion.SolutionParser.Parser
{
    public class HeaderParser
    {
        private readonly string _solutionContents;

        public HeaderParser(string solutionContents)
        {
            _solutionContents = solutionContents;
        }

        public IEnumerable<string> Parse()
        {
            var result = new List<string>();
            using (var sr = new StringReader(_solutionContents))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    if (line.TrimStart().StartsWith("Project(") || line.TrimStart().StartsWith("Global("))
                    {
                        break;
                    }
                    result.Add(line);
                    line = sr.ReadLine();
                }
            }
            return result;
        }

    }
}
