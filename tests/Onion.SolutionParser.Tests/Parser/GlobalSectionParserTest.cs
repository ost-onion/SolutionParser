using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Onion.SolutionParser.Parser;
using Onion.SolutionParser.Parser.Model;

namespace Onion.SolutionParser.Tests.Parser
{
    [TestFixture]
    public class GlobalSectionParserTest
    {
        public GlobalSectionParser Parser { get; set; }
        public string SolutionContents { get; set; }

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            SolutionContents = Utility.GetFixtureContents("NDriven.sln");
        }

        [SetUp]
        public void BeforeEach()
        {
            Parser = new GlobalSectionParser(SolutionContents);
        }

        [Test]
        public void Parse_should_return_IEnumerable_of_GlobalSection_objects_correct_count()
        {
            var sections = Parser.Parse();
            var count = sections.Count();
            Assert.IsInstanceOf<IEnumerable<GlobalSection>>(sections);
            Assert.AreEqual(4, count);
        }

        [Test]
        public void Parse_should_hydrate_GlobalSection_object_name_and_type_properties()
        {
            var sections = Parser.Parse();
            foreach(var section in sections)
            {
                Assert.IsNotEmpty(section.Name);
                Assert.IsInstanceOf<GlobalSectionType>(section.Type);
            }
        }

        [Test]
        public void Parse_should_hydrate_entries()
        {
            var sections = Parser.Parse();
            foreach (var section in sections)
            {
                Assert.True(section.Entries.Count > 0);
            }
        }

        [Test]
        public void GlobalSections_should_have_correct_number_of_entries()
        {
            var sections = Parser.Parse();
            Assert.AreEqual(2, sections.ElementAt(0).Entries.Count);
            Assert.AreEqual(32, sections.ElementAt(1).Entries.Count);
            Assert.AreEqual(1, sections.ElementAt(2).Entries.Count);
            Assert.AreEqual(6, sections.ElementAt(3).Entries.Count);
        }
    }
}
