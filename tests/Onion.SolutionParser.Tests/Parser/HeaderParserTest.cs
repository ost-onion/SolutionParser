using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Onion.SolutionParser.Parser;

namespace Onion.SolutionParser.Tests.Parser
{
    [TestFixture]
    public class HeaderParserTest
    {
        public HeaderParser Parser { get; set; }
        public string SolutionContents { get; set; }

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            SolutionContents = Utility.GetFixtureContents("NDriven.sln");
        }

        [SetUp]
        public void BeforeEach()
        {
            Parser = new HeaderParser(SolutionContents);
        }

        [Test]
        public void Parse_should_return_IEnumerable_of_string_with_correct_count()
        {
            var lines = Parser.Parse();
            var count = lines.Count();
            Assert.IsInstanceOf<IEnumerable<string>>(lines);
            Assert.AreEqual(3, count);
        }

        [Test]
        public void Parse_should_return_correct_lines()
        {
            var lines = Parser.Parse();
            Assert.AreEqual("", lines.ElementAt(0));
            Assert.AreEqual("Microsoft Visual Studio Solution File, Format Version 12.00", lines.ElementAt(1));
            Assert.AreEqual("# Visual Studio 2012", lines.ElementAt(2));
        }
    }
}
