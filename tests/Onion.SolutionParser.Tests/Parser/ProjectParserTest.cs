using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Onion.SolutionParser.Parser;
using Onion.SolutionParser.Parser.Model;

namespace Onion.SolutionParser.Tests.Parser
{
    [TestFixture]
    public class ProjectParserTest
    {
        public ProjectParser Parser { get; set; }
        public string SolutionContents { get; set; }

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            SolutionContents = Utility.GetFixtureContents("NDriven.sln");
        }

        [SetUp]
        public void BeforeEach()
        {
            Parser = new ProjectParser(SolutionContents);
        }

        [Test]
        public void Parse_should_return_IEnumerable_of_GlobalSection_objects_with_correct_count()
        {
            var sections = Parser.Parse();
            var count = sections.Count();
            Assert.IsInstanceOf<IEnumerable<Project>>(sections);
            Assert.AreEqual(11, count);
        }
    }
}
