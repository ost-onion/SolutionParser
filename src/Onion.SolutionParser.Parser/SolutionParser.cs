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
            using (var reader = new StreamReader(path))
            {
                solutionContents = reader.ReadToEnd();
            }
        }

        public ISolution Parse()
        {
            return new Solution
                {
                    Global = (new GlobalSectionParser(solutionContents)).Parse(),
                    Projects = (new ProjectParser(solutionContents)).Parse()
                };
        }

        public static ISolution Parse(string path)
        {
            var parser = new SolutionParser(path);
            return parser.Parse();
        }
    }
}
