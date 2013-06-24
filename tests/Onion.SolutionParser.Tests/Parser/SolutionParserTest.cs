using System.IO;
using NUnit.Framework;
using RealParser = Onion.SolutionParser.Parser;

namespace Onion.SolutionParser.Tests.Parser
{
    [TestFixture]
    public class SolutionParserTest
    {
        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Constructing_with_unknown_path_throws_exception()
        {
            var parser = new RealParser.SolutionParser("nope");
        }
    }
}
