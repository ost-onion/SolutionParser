using System.Collections.Generic;
using Onion.SolutionParser.Parser.Model;

namespace Onion.SolutionParser.Parser
{
    public interface ISolutionItemParser<out T> where T : ISolutionItem
    {
        IEnumerable<T> Parse();
    }
}
