using System.Collections.Generic;
using System.Text.RegularExpressions;
using Onion.SolutionParser.Parser.Model;

namespace Onion.SolutionParser.Parser
{
    public class GlobalSectionParser : SolutionItemParserBase<GlobalSection>
    {
        private static readonly Regex GlobalPattern = new Regex(@"GlobalSection\((?<name>[\w]+)\)\s+=\s+(?<type>(?:post|pre)Solution)(?<content>.*?)EndGlobalSection", RegexOptions.Singleline | RegexOptions.ExplicitCapture);
        private static readonly Regex EntryPattern = new Regex(@"^\s*(?<key>.*?)=(?<value>.*?)$", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.Multiline);

        public GlobalSectionParser(string solutionContents) : base (solutionContents)
        {
            
        }

        public override IEnumerable<GlobalSection> Parse()
        {
            var match = GlobalPattern.Match(SolutionContents);
            while (match.Success)
            {
                var section = CreateGlobalSectionFromMatch(match);
                PopulateSectionEntries(match, section);
                yield return section;
                match = match.NextMatch();
            }
        }

        private static void PopulateSectionEntries(Match match, GlobalSection section)
        {
            var content = match.Groups["content"].Value;
            var entryMatch = EntryPattern.Match(content);
            while (entryMatch.Success)
            {
                var key = entryMatch.Groups["key"].Value.Trim();
                var value = entryMatch.Groups["value"].Value.Trim();
                section.Entries.Add(key, value);
                entryMatch = entryMatch.NextMatch();
            }
        }

        private static GlobalSection CreateGlobalSectionFromMatch(Match match)
        {
            var sectionType = (match.Groups["type"].Value == "preSolution")
                                  ? GlobalSectionType.PreSolution
                                  : GlobalSectionType.PostSolution;
            var section = new GlobalSection(match.Groups["name"].Value, sectionType);
            return section;
        }
    }
}
