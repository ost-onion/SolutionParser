using System.Collections.Generic;
using Onion.SolutionParser.Parser.Model;

namespace Onion.SolutionParser.Parser
{
    abstract public class SolutionItemParserBase<T> : ISolutionItemParser<T> where T : ISolutionItem
    {
        protected readonly string SolutionContents;

        protected SolutionItemParserBase(string solutionContents)
        {
            SolutionContents = solutionContents;
        }

        public virtual IEnumerable<T> Parse()
        {
            throw new System.NotImplementedException();
        }
    }
}
