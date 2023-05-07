namespace N6L972;

public record Hero
{
    public string HeroName { get; set; }
    public string RealName { get; set; }
    public string SuperPower { get; set; }
    public int DuckStrength { get; set;  }

    public static Hero BuildFromFileLine(string[] fields)
    {
        if (int.TryParse(fields[3], out var parsedDuckPower) is false)
            Console.Error.WriteLine($"a ${fields[1]} nevű hőshöz tartozó kacsaerő parsolása sikertelen, ezért 0-ra állítódik");
        return new Hero()
        {
            HeroName = fields[0],
            RealName = fields[1],
            SuperPower = fields[2],
            DuckStrength = parsedDuckPower
        };
    }
}