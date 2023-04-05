

[Serializable]
public class Pokemon
{
    public int id { get; set; }
    public string num { get; set; }
    public string name { get; set; }
    public string img { get; set; }
    public List<string> type { get; set; }
    public string height { get; set; }
    public string weight { get; set; }
    public string candy { get; set; }
    public int candy_count { get; set; }
    public string egg { get; set; }
    public double spawn_chance { get; set; }
    public double avg_spawns { get; set; }
    public string spawn_time { get; set; }
    public List<double>? multipliers { get; set; }
    public List<string> weaknesses { get; set; }
    public List<object>? prev_evolution { get; set; }
    public List<object> next_evolution { get; set; }

    public Pokemon(
        int id,
        string num,
        string name,
        string img,
        List<string> type,
        string height,
        string weight,
        string candy,
        int candy_count,
        string egg,
        double spawn_chance,
        double avg_spawns,
        string spawn_time,
        List<double>? multipliers,
        List<string> weaknesses,
        List<object> prev_evolution,
        List<object> next_evolution)
    {
        this.id = id;
        this.num = num;
        this.name = name;
        this.img = img;
        this.type = type;
        this.height = height;
        this.weight = weight;
        this.candy = candy;
        this.candy_count = candy_count;
        this.egg = egg;
        this.spawn_chance = spawn_chance;
        this.avg_spawns = avg_spawns;
        this.multipliers = multipliers;
        this.weaknesses = weaknesses;
        this.prev_evolution = prev_evolution;
        this.next_evolution = next_evolution;
    }

    public Pokemon()
    {
        type = new List<string>();
        multipliers = new List<double>();
        weaknesses = new List<string>();
        prev_evolution = new List<object>();
        next_evolution = new List<object>();
    }

    public override string ToString()
    {
        string result =
            "id: " + id + "\n"
            + "num: " + num + "\n"
            + "name: " + name + "\n"
            + "img: " + img + "\n"
            + "type:\n";
        foreach (string element in type)
        {
            result += "\t" + element + "\n";
        };
        result +=
            "height: " + height + "\n"
            + "weight: " + weight + "\n"
            + "candy: " + candy + "\n"
            + "candy_count: " + candy_count + "\n"
            + "egg: " + egg + "\n"
            + "spawn_chance: " + spawn_chance + "\n"
            + "avg_spawns: " + avg_spawns + "\n"
            + "spawn_time: " + spawn_time + "\n"
        + "multipliers:\n";

        if (multipliers != null)
        {
            foreach (double element in multipliers)
            {
                if (element != null)
                {
                    result += "\t" + element + "\n";
                }
            };
        }
        result += "weaknesses:\n";
        foreach (string element in weaknesses)
        {
            result += "\t" + element + "\n";
        };

        result += "prev_evolution:\n";
        foreach (object element in prev_evolution)
        {
            result += "\t" + element + "\n";
        };

        result += "next_evolution:\n";
        foreach (object element in next_evolution)
        {
            result += "\t" + element + "\n";
        };

        return result;
    }


    public class Next
    {
        public string num { get; set; }
        public string name { get; set; }
    }

}
