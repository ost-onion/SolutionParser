using NUnit.Framework;
using Onion.SolutionParser.Parser;
using RealParser = Onion.SolutionParser.Parser;

namespace Onion.SolutionParser.Tests.Renderer
{
    [TestFixture]
    public class SolutionRendererTest
    {

        [Test]
        public void Renderer1_should_render_same_contents()
        {
            var contents = Utility.GetFixtureContents("NDriven.sln");
            var parser = new RealParser.SolutionParser(Utility.GetFixturePath("NDriven.sln"));
            var solution = parser.Parse();
            var renderer = new SolutionRenderer(solution);
            var rendered = renderer.Render();
            Assert.AreEqual(contents, rendered);
        }

        [Test]
        public void Renderer2_should_render_same_contents()
        {
            var contents = Utility.GetFixtureContents("Microsoft.AspNet.SignalR.sln");
            var parser = new RealParser.SolutionParser(Utility.GetFixturePath("Microsoft.AspNet.SignalR.sln"));
            var solution = parser.Parse();
            var renderer = new SolutionRenderer(solution);
            var rendered = renderer.Render();
            Assert.AreEqual(contents, rendered);
        }

    }
}
