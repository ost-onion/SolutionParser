using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Onion.SolutionParser.Parser.Model;
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

        [Test]
        public void Parse_should_populate_Global_and_Projects_properties()
        {
            var parser = new RealParser.SolutionParser(Utility.GetFixturePath("NDriven.sln"));
            var solution = parser.Parse();
            Assert.IsInstanceOf<IEnumerable<GlobalSection>>(solution.Global);
            Assert.IsInstanceOf<IEnumerable<Project>>(solution.Projects);
        }

        [Test]
        public void Static_Parse_should_return_solution_object()
        {
            var path = Utility.GetFixturePath("NDriven.sln");
            var solution = RealParser.SolutionParser.Parse(path);
            Assert.IsInstanceOf<ISolution>(solution);
        }
    }
}
