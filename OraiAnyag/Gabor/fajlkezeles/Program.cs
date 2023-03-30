using System.IO;
using System.Text.Json;

namespace fajlkezeles
{
    class CuteCat
    {
        public string Color { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string name = Console.ReadLine() ?? "hello.txt";
            string path = Path.Combine(AppContext.BaseDirectory, name);
            using (FileStream file = File.OpenWrite(path))
            {
                using (StreamWriter writer = new StreamWriter(file))
                {
                    try
                    {
                        writer.WriteLine("Hello File world");
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine("Hiba történt");
                        //Pokémon
                    }
                }
            }

            //string read = File.ReadAllText(path);

            var garfield = new CuteCat
            {
                Color = "orange",
                Name = "Garfield",
                Weight = 12_000
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };


            var jsonString = JsonSerializer.Serialize(garfield, options);

            Console.WriteLine(jsonString);

            CuteCat? readed = JsonSerializer.Deserialize<CuteCat>(jsonString, options);
        }
    }
}