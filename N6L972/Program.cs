using System.Text.Encodings.Web;
using System.Text.Json;
using N6L972;

Console.WriteLine("Ez a program egy csv file sorait olvassa ki, a megadott prefix alapján kiszűri azokat és rendezi az eredményt!");
Console.WriteLine("Kérlek add meg a file abszolút elérési útvonalát amiből be szeretnéd olvasni a sorokat!");
var fileName = Console.ReadLine();

Console.WriteLine("add meg a prefixet, ami alapján a hős nevét szürjük!");
var prefix = Console.ReadLine();

Console.WriteLine("add meg a minimum kacsaerőt, ami alapján szürjük a hősöket, egész integer szám!");
var minDuckPowerInput = Console.ReadLine();
int minDuckPower;
try
{
    minDuckPower = int.Parse(minDuckPowerInput);
}
catch (Exception ex) when (ex is FormatException or ArgumentNullException or OverflowException)
{
    Console.Error.WriteLine($"Hibás kacsaerő input: {minDuckPowerInput} hiba: {ex.Message}");
    return;
}

var heroes = new List<Hero>();
if (File.Exists(fileName) is false)
    Console.WriteLine($"a file elérési útvonala rossz: {fileName}");

await using var file = File.OpenRead(fileName);
using var reader = new StreamReader(file);
try
{
    while (await reader.ReadLineAsync() is { } line)
    {
        var values = line.Split(";");
        if (values.Length != 4)
        {
            Console.Error.WriteLine($"Hibás adat, a sorban több/kevesebb érték van a szükségesnél: {line}");
            return;
        }
        heroes.Add(Hero.CreateFromFileLine(values));
    }
}
catch (IOException ex)
{
    Console.Error.WriteLine($"Hiba történt: ${ex.Message}");
}

var sortedHeroes = heroes
    .FindAll(hero => hero.HeroName.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase)
                     && hero.DuckStrength >= minDuckPower)
    .OrderBy(hero => -hero.DuckStrength);

var jsonSerializedHeroes = JsonSerializer.Serialize(sortedHeroes, new JsonSerializerOptions()
{
    WriteIndented = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
});

Console.WriteLine(jsonSerializedHeroes);
await WriteHeroes(jsonSerializedHeroes);

static async Task WriteHeroes(string json)
{
    await File.WriteAllTextAsync(Path.Combine(AppContext.BaseDirectory, "output.json"), json);
}