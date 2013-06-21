using System.IO;

namespace Onion.SolutionParser.Tests
{
    public static class Utility
    {
        public static string GetFixtureContents(string fileName)
        {
            var fixturesPath = Path.GetFullPath("Fixtures");
            var slnPath = fixturesPath + Path.DirectorySeparatorChar + fileName;
            using (var reader = new StreamReader(slnPath))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
