using System;
using System.IO;
using Onion.SolutionParser.Parser.Model;

namespace Onion.SolutionParser.Parser
{
    public class SolutionParser : ISolutionParser
    {
        private readonly string _solutionContents;

        public SolutionParser(string path)
        {
            if (! File.Exists(path))
                throw new FileNotFoundException(string.Format("Solution file {0} does not exist", path));
            using (var reader = new StreamReader(path))
            {
                _solutionContents = reader.ReadToEnd();
            }
        }

        public SolutionParser(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            using (var reader = new StreamReader(stream))
            {
                _solutionContents = reader.ReadToEnd();
            }
        }

        public SolutionParser(TextReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            _solutionContents = reader.ReadToEnd();
        }

        public ISolution Parse()
        {
            return new Solution
                {
                    Header = (new HeaderParser(_solutionContents)).Parse(),
                    Global = (new GlobalSectionParser(_solutionContents)).Parse(),
                    Projects = (new ProjectParser(_solutionContents)).Parse()
                };
        }

        public static ISolution Parse(string path)
        {
            var parser = new SolutionParser(path);
            return parser.Parse();
        }

        public static ISolution Parse(Stream stream)
        {
            var parser = new SolutionParser(stream);
            return parser.Parse();
        }

        public static ISolution Parse(TextReader reader)
        {
            var parser = new SolutionParser(reader);
            return parser.Parse();
        }
    }
}
