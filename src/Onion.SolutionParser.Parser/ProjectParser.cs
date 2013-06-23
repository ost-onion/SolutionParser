using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Onion.SolutionParser.Parser.Model;

namespace Onion.SolutionParser.Parser
{
    public class ProjectParser
    {
        private readonly string _solutionContents;
        private static readonly Regex ProjectPattern = new Regex(@"Project\(\""(?<typeGuid>.*?)\""\)\s+=\s+\""(?<name>.*?)\"",\s+\""(?<path>.*?)\"",\s+\""(?<guid>.*?)\""(?<content>.*?)\bEndProject\b", RegexOptions.ExplicitCapture | RegexOptions.Singleline);
        private static readonly Regex SectionPattern = new Regex(@"ProjectSection\((?<name>.*?)\)\s+=\s+(?<type>.*?)\s+(?<entries>.*?)\bEndProjectSection\b", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.ExplicitCapture);
        private static readonly Regex EntryPattern = new Regex(@"^\s*(?<key>.*?)=(?<value>.*?)$", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.Multiline);

        public ProjectParser(string solutionContents)
        {
            _solutionContents = solutionContents;
        }

        public IEnumerable<Project> Parse()
        {
            var match = ProjectPattern.Match(_solutionContents);
            while (match.Success)
            {
                var typeGuid = new Guid(match.Groups["typeGuid"].Value);
                var guid = new Guid(match.Groups["guid"].Value);
                var project = new Project(typeGuid, match.Groups["name"].Value, match.Groups["path"].Value, guid);
                yield return project;
                match = match.NextMatch();
            }
        }
    }
}
