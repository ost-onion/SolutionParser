using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        [Test]
        public void Parse_larger_solution_file()
        {
            var solution = RealParser.SolutionParser.Parse(Utility.GetFixturePath("Microsoft.AspNet.SignalR.sln"));
            Assert.IsInstanceOf<ISolution>(solution);
        }

        [Test]
        public void SolutionParser_should_parse_pre_and_post_project_sections()
        {
            var solution = RealParser.SolutionParser.Parse(Utility.GetFixturePath("Microsoft.AspNet.SignalR.sln"));
            Assert.IsTrue(solution.Projects.Any(p => p.ProjectSection != null && p.ProjectSection.Type == ProjectSectionType.PostProject));
            Assert.IsTrue(solution.Projects.Any(p => p.ProjectSection != null && p.ProjectSection.Type == ProjectSectionType.PreProject));
        }
    }
}
