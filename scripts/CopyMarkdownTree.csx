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
var copies =
    from path in Directory.EnumerateFiles(srcDirPath, "*.md", SearchOption.AllDirectories)
    where !path.StartsWith(".", StringComparison.Ordinal)
       && !path.EndsWith(".source.md", StringComparison.OrdinalIgnoreCase)
       && !Path.GetFileNameWithoutExtension(path).Equals("README", StringComparison.OrdinalIgnoreCase)
    select new
    {
        Source = path,
        Destination = Path.Combine(rootDirPath, "docs", Path.GetRelativePath(srcDirPath, path)),
    };

foreach (var copy in copies)
{
    if (isDryRun)
    {
        Console.WriteLine($"Will \"{copy.Source}\" -> {copy.Destination}\"");
    }
    else
    {
        log?.Invoke($"\"{copy.Source}\" -> {copy.Destination}\"");

        var fileDestBasePath = Path.GetDirectoryName(copy.Destination);
        if (!Directory.Exists(fileDestBasePath))
        {
            log?.Invoke($"Creating directory \"{fileDestBasePath}\"");
            Directory.CreateDirectory(fileDestBasePath);
        }

        File.Copy(copy.Source, copy.Destination, overwrite: true);
    }
}
