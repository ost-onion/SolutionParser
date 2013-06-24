using System.IO;
using Onion.SolutionParser.Parser.Model;

namespace Onion.SolutionParser.Parser
{
    public class SolutionParser : ISolutionParser
    {
        private readonly string solutionContents;

        public SolutionParser(string path)
        {
            if (! File.Exists(path))
                throw new FileNotFoundException(string.Format("Solution file {0} does not exist", path));
        }

        public ISolution Parse()
        {
            return new Solution();
        }
    }
}
