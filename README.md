SolutionParser
==============

SolutionParser parses a Visual Studio solution file into an object structure.

Get it on NuGet
---------------
`Install-Package Onion.SolutionParser.Parser`

Usage
-----

SolutionParser is easy to use:

```csharp
var solution = SolutionParser.Parse("/path/to/solution.sln");

//IEnumerable<GlobalSection>
var global = solution.Global;

//IEnumerable<Project>
var projects = solution.Projects;
```

Types
-----
The included types mirror the various sections contained in a solution file.

###GlobalSection###

The `GlobalSection` type is made up of a name, a `GlobalSectionType` (either preSolution or postSolution), and a dictionary of environment entries.

```csharp
public class GlobalSection : ISolutionItem
{
    public string Name { get; set; }
    public GlobalSectionType Type { get; set; }
    public IDictionary<string, string> Entries { get; set; }
}

public enum GlobalSectionType
{
    PostSolution,
    PreSolution
}
```

###Project###

The `Project` type is made up of a type guid, a name, a path to the project file, and a unique guid.
A project may also contain a `ProjectSection` type that is composed of a name, a `ProjectSectionType`
(either preProject or postProject) and a dictionary of environment entries.

```csharp
public class Project : ISolutionItem
{
    public Guid TypeGuid { get; private set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public Guid Guid { get; private set; }
    public ProjectSection ProjectSection { get; set; }
}

public class ProjectSection
{
    public string Name { get; set; }
    public ProjectSectionType Type { get; set; }
    public IDictionary<string, string> Entries { get; set; }
}

public enum ProjectSectionType
{
    PostProject,
    PreProject
}
```

###Solution###

The `Solution` type is composed of an `IEnumerable<GlobalSection>` and
an `IEnumerable<Project>`.

```csharp
public class Solution : ISolution
{
    public IEnumerable<GlobalSection> Global { get; set; }
    public IEnumerable<Project> Projects { get; set; } 
}
```

Parsers
-------

SolutionParser contains three parsers for more or less detailed access to a solution file.

###SolutionParser###

The `SolutionParser` class is the most direct parser, and returns a fully composed `Solution` object.

```csharp
var solution = SolutionParser.Parse('/path/to/solution.sln');
```

###GlobalSectionParser###

A `GlobalSectionParser` accepts the text contents of a solution file. The parse method
returns an `IEnumerable<GlobalSection>`.

```csharp
var parser = new GlobalSectionParser(solutionContents);

//IEnumerable<GlobalSection>
var sections = parser.Parse();
```

###ProjectParser###

A `ProjectParser` accepts the contents of a solution file. The parse method
returns an `IEnumerable<Project>`.

```csharp
var parser = new ProjectParser(solutionContents);

//IEnumerable<Project>
var projects = parser.Parse();
```

Running unit tests
------------------
SolutionParser assumes package restore is enabled. To run the tests
just clone the project, hit build, and run the unit tests with your
favorite NUnit runner.