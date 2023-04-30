namespace N6L972;

public record Hero
{
    public string HeroName { get; set; }
    public string RealName { get; set; }
    public string SuperPower { get; set; }
    public int DuckStrength { get; set;  }

    public static Hero BuildFromFileLine(string[] fields)
    {
        return new Hero()
        {
            HeroName = fields[0],
            RealName = fields[1],
            SuperPower = fields[2],
            DuckStrength = int.Parse(fields[3])
        };
    }
}