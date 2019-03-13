using System.Text.RegularExpressions;

if (Args.Count == 0)
{
    throw new Exception("Missing Markdown document name argument.");
}

var projectPath = "doc.proj";

if (!File.Exists(projectPath))
    throw new Exception($"Project file named \"{projectPath}\" not found.");

var name = Args[0];

if (   name.EndsWith(".r", StringComparison.OrdinalIgnoreCase)
    || name.EndsWith(".source", StringComparison.OrdinalIgnoreCase))
{
    name += ".md";
}
else if (string.IsNullOrEmpty(Path.GetExtension(name)))
{
    name += ".r.md";
}

File.WriteAllLines(
    Path.Combine("src", "docs", name),
    new[] { "# Title" },
    new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));

File.SetLastWriteTime(projectPath, DateTime.Now);
