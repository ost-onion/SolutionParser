using System;
using System.Collections.Generic;
using System.IO;
using Onion.SolutionParser.Parser.Model;

namespace Onion.SolutionParser.Parser
{
    public class SolutionRenderer : ISolutionRenderer
    {
        private readonly ISolution _solution;

        public SolutionRenderer(ISolution solution)
        {
            if (solution == null)
            {
                throw new ArgumentNullException("solution");
            }
            _solution = solution;
        }

        public string Render()
        {
            using (var sb = new StringWriter())
            {
                foreach (var header in _solution.Header)
                {
                    sb.WriteLine(header);
                }
                RenderProjects(_solution.Projects, sb);
                RenderGlobal(_solution.Global, sb);
                return sb.ToString();
            }
        }

        private void RenderProjects(IEnumerable<Project> projects, StringWriter sb)
        {
            foreach (var project in _solution.Projects)
            {
                RenderProject(project, sb);
            }
        }

        private void RenderProject(Project project, StringWriter sb)
        {
            sb.WriteLine("Project(\"{0}\") = \"{1}\", \"{2}\", \"{3}\"", project.TypeGuid.ToString("B").ToUpperInvariant(), project.Name, project.Path, project.Guid.ToString("B").ToUpperInvariant());
            if (project.ProjectSection != null)
            {
                sb.WriteLine("\tProjectSection({0}) = {1}", project.ProjectSection.Name, Render(project.ProjectSection.Type));
                foreach (var entry in project.ProjectSection.Entries)
                {
                    sb.WriteLine("\t\t{0} = {1}", entry.Key, entry.Value);

                }
                sb.WriteLine("\tEndProjectSection");
            }
            sb.WriteLine("EndProject");
        }

        private void RenderGlobal(IEnumerable<GlobalSection> global, StringWriter sb)
        {
            sb.WriteLine("Global");
            foreach (var globalSection in _solution.Global)
            {
                RenderGlobalSection(globalSection, sb);
            }
            sb.WriteLine("EndGlobal");
        }

        private void RenderGlobalSection(GlobalSection globalSection, StringWriter sb)
        {
            sb.WriteLine("\tGlobalSection({0}) = {1}", globalSection.Name, Render(globalSection.Type));
            foreach (var entry in globalSection.Entries)
            {
                sb.WriteLine("\t\t{0} = {1}", entry.Key, entry.Value);
            }
            sb.WriteLine("\tEndGlobalSection");
        }

        private string Render(ProjectSectionType type)
        {
            switch (type)
            {
                case ProjectSectionType.PostProject:
                    return "postProject";
                case ProjectSectionType.PreProject:
                    return "preProject";
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }
        }

        private string Render(GlobalSectionType type)
        {
            switch (type)
            {
                case GlobalSectionType.PostSolution:
                    return "postSolution";
                case GlobalSectionType.PreSolution:
                    return "preSolution";
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }
        }

        public static string Render(ISolution solution)
        {
            var renderer = new SolutionRenderer(solution);
            return renderer.Render();
        }
    }
}
