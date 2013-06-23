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

        [Test]
        public void Parse_should_set_ProjectSection_if_present_on_project()
        {
            var nuget = Parser.Parse().First(p => p.Name == ".nuget");
            Assert.NotNull(nuget.ProjectSection);
        }

        [Test]
        public void Parse_should_set_entries_on_ProjectSection()
        {
            var nuget = Parser.Parse().First(p => p.Name == ".nuget");
            var entries = nuget.ProjectSection.Entries;
            Assert.AreEqual(".nuget\\NuGet.Config", entries[".nuget\\NuGet.Config"]);
            Assert.AreEqual(".nuget\\NuGet.exe", entries[".nuget\\NuGet.exe"]);
            Assert.AreEqual(".nuget\\NuGet.targets", entries[".nuget\\NuGet.targets"]);
        }
    }
}
