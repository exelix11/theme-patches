using System.Text.Json;

var files = Directory.GetFiles("../../systemPatches").Where(x => x.EndsWith(".ips"));

if (files.Any(x => Path.GetFileNameWithoutExtension(x).Any(Char.IsLower)))
{
    Console.Error.WriteLine("Ips file names should all be uppercase");
    return -1;
}

Directory.CreateDirectory("../../deploy");
Directory.CreateDirectory("../../deploy/ips");

foreach (var f in files)
{
    File.Copy(f, "../../deploy/ips/" + Path.GetFileNameWithoutExtension(f));
}

var s = JsonSerializer.Serialize(new
{
    domain = "https://exelix11.github.io/theme-patches/ips/",
    ids = files.Select(x => Path.GetFileNameWithoutExtension(x)).ToArray()
});

File.WriteAllText("../../deploy/index.json", s);

return 0;