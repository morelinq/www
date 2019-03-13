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

var srcDirPath = Path.Combine(rootDirPath, "src", "docs");
var targetDirPath = Path.Combine(rootDirPath, "docs");

bool ExcludeFile(string path)
    => path.StartsWith(".", StringComparison.Ordinal)
    || Path.GetFileNameWithoutExtension(path).Equals("README", StringComparison.OrdinalIgnoreCase);

var sources =
    from path in Directory.EnumerateFiles(srcDirPath, "*.md", SearchOption.AllDirectories)
    where !ExcludeFile(path)
    select (path, Path.GetRelativePath(srcDirPath, path));

var relativeSourcePaths = new List<string>();

foreach (var (path, relativePath) in sources)
{
    relativeSourcePaths.Add(relativePath);

    if (path.EndsWith(".source.md", StringComparison.OrdinalIgnoreCase))
        continue;

    var targetPath = Path.Combine(targetDirPath, relativePath);

    bool move = false;

    if (targetPath.EndsWith(".r.md", StringComparison.OrdinalIgnoreCase))
        targetPath = Path.ChangeExtension(Path.ChangeExtension(targetPath, null), ".md");
    else
        move = true;

    if (isDryRun)
    {
        Console.WriteLine($"Will {(move ? "move" : "copy")} \"{path}\" -> \"{targetPath}\"");
    }
    else
    {
        log?.Invoke($"{(move ? "mv" : "cp")} \"{path}\" \"{targetPath}\"");

        var fileDestBasePath = Path.GetDirectoryName(targetPath);
        if (!Directory.Exists(fileDestBasePath))
        {
            log?.Invoke($"Creating directory \"{fileDestBasePath}\"");
            Directory.CreateDirectory(fileDestBasePath);
        }

        if (move)
        {
            if (File.Exists(targetPath))
                File.Delete(targetPath);
            File.Move(path, targetPath);
        }
        else
        {
            File.Copy(path, targetPath, overwrite: true);
        }
    }
}

var relativeNormalSourcePathSet =
    Enumerable.ToHashSet(
        from s in relativeSourcePaths
        select Path.ChangeExtension(Path.ChangeExtension(s, null), ".md"),
        StringComparer.OrdinalIgnoreCase);

var targets =
    from path in Directory.EnumerateFiles(targetDirPath, "*.md", SearchOption.AllDirectories)
    where !ExcludeFile(path)
    select (path, Path.GetRelativePath(targetDirPath, path));

foreach (var (path, relativePath) in targets)
{
    if (relativeNormalSourcePathSet.Contains(relativePath))
        continue;

    if (isDryRun)
    {
        Console.WriteLine($"Will delete \"{path}\"");
    }
    else
    {
        log?.Invoke($"rm \"{path}\"");
        File.Delete(path);
    }
}
