var args = new List<string>(Args);

bool PopSwitch(string name1, string name2 = null, string name3 = null)
{
    var i = args.FindIndex(arg => arg == name1 || arg == name2 || arg == name3);
    var found = i >= 0;
    if (found)
        args.RemoveAt(i);
    return found;
}

var log = PopSwitch("--verbose", "-v")
        ? Console.Error.WriteLine
        : (Action<string>)null;

var isDryRun = PopSwitch("--dry-run", "-n");

var rootDirPath = Environment.CurrentDirectory;

var srcDirPath = Path.Combine(rootDirPath, "docs.src");
var moves =
    from path in Directory.EnumerateFiles(srcDirPath, "*.md", SearchOption.AllDirectories)
    where !path.StartsWith(".", StringComparison.Ordinal)
       && !path.EndsWith(".source.md", StringComparison.OrdinalIgnoreCase)
       && File.Exists(Path.ChangeExtension(path, ".source.md"))
    select new
    {
        Source = path,
        Destination = Path.Combine(rootDirPath, "docs", Path.GetRelativePath(srcDirPath, path)),
    };

foreach (var move in moves)
{
    if (isDryRun)
    {
        Console.WriteLine($"Will \"{move.Source}\" -> {move.Destination}\"");
    }
    else
    {
        log?.Invoke($"\"{move.Source}\" -> {move.Destination}\"");

        var fileDestBasePath = Path.GetDirectoryName(move.Destination);
        if (!Directory.Exists(fileDestBasePath))
        {
            log?.Invoke($"Creating directory \"{fileDestBasePath}\"");
            Directory.CreateDirectory(fileDestBasePath);
        }

        if (File.Exists(move.Destination))
            File.Delete(move.Destination);
        File.Move(move.Source, move.Destination);
    }
}
