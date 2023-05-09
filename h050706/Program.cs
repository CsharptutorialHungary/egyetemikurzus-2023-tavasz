using h050706.EnumPokemon;
using Microsoft.Win32.SafeHandles;
using System.Collections.Specialized;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Threading.Channels;
using System.Net.WebSockets;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace Kotprog
{
    internal class Program
    {
        static PokemonCollection globalPokemons = new PokemonCollection();

        private static void Main(string[] args)
        {
            MakeCollectionAndListPokemons(false, globalPokemons);
            SelectMethods();

            Console.WriteLine("Program vége");
            Console.ReadKey();
        }
        private static void WriteInstructions()
        {
            Console.WriteLine("\n----------------");
            Console.WriteLine("Kérlek válaszd ki mit szeretnél csinálni: ");
            Console.WriteLine("\t 0. Kilépés");
            Console.WriteLine("\t 1. Pokémonok listázása a megadott tojáskategória alapján");
            Console.WriteLine("\t 2. Megadott típusú Pokémonok listázása a keltetéshez szükséges Candy alapján");
            Console.WriteLine("\t 3. Legnagyobb eséllyel spawn-oló Pokémon");
            Console.WriteLine("\t 4. Átlagos spawn_change");
            Console.Write("\nAdd meg a sorszámot: ");
        }

        private static void SelectMethods()
        {
            Console.WriteLine("Üdvözöllek a Pokémonokkal foglalkozó programomban");
            string selectMethod = "";
            while (selectMethod != "0")
            {
                WriteInstructions();
                selectMethod = Console.ReadLine();
                Console.WriteLine();
                switch (selectMethod)
                {
                    case "0":
                        break;
                    case "1":
                        Console.WriteLine("1. Pokémonok listázása a megadott tojáskategória alapján:");
                        SelectPokemonsByEggs();
                        break;
                    case "2":
                        Console.WriteLine("2. Megadott típusú Pokémonok listázása a keltetéshez szükséges Candy alapján:");
                        SelectNameByTypeOrderByCandyCount();
                        break;
                    case "3":
                        Console.WriteLine("3. Legnagyobb eséllyel spawn-oló Pokémon:");
                        SelectMaxSpawnChance();
                        break;
                    case "4":
                        Console.WriteLine("4. Átlagos spawn_change:");
                        SelectAvgSpawnChance();
                        break;
                    default:
                        Console.WriteLine("Helytelen sorszám, kérlek a fentiek közül válassz!");
                        break;
                        break;
                }
            }
        }
        private static void SelectNameByTypeOrderByCandyCount()
        {
            #region Creating typeList
            List<string> typeList = OptionListing("type", true);
            if (typeList == null)
            {
                Console.WriteLine("[Hiba] Nem létező opció " + nameof(SelectNameByTypeOrderByCandyCount));
                return;
            }
            #endregion

            #region Reading input, check if it's valid
            string typeInput;
            Console.WriteLine("Adja meg a szűrni kívánt típust!");
            while (true)
            {
                Console.Write("Opciók: ");
                typeList.ForEach(n => Console.Write("[" + n + "] "));
                Console.WriteLine();
                typeInput = Console.ReadLine();

                if (typeList.Any(n => n.Equals(typeInput, StringComparison.OrdinalIgnoreCase)))
                {
                    break;
                }
                Console.WriteLine("Ismeretlen típus, válassz az alábbiakból:");
            }
            #endregion

            #region Select the given types with not zero candy_count, sorting and grouping by candy_count and
            var result = from pokemon in globalPokemons
                         where pokemon.type.Any(n => n.Equals(typeInput, StringComparison.OrdinalIgnoreCase)) && pokemon.candy_count > 0
                         orderby pokemon.candy_count ascending
                         group pokemon by pokemon.candy_count into candy_countGroup
                         select candy_countGroup;

            foreach (var item in result)
            {
                Console.WriteLine(item.Key + ": ");
                foreach (var poke_item in item)
                {
                    Console.WriteLine("\t" + poke_item.name);
                }
            }
            #endregion
        }
        private static void SelectPokemonsByEggs()
        {
            List<string> eggList = OptionListing("egg", true);

            string condition;
            while (true)
            {
                Console.WriteLine("Írd be az értéket: ");
                condition = Console.ReadLine();

                if (eggList.Any(n => n.Equals(condition, StringComparison.OrdinalIgnoreCase)))
                {
                    break;
                }
                Console.WriteLine("Ismeretlen típus, válassz a fenti opciók közül!");
            }

            var result = from elem in globalPokemons
                         where elem.egg.ToLower() == condition.ToLower()
                         select elem;

            if (result.Any())
            {
                PokemonCollection outputColletion = new PokemonCollection();
                foreach (var item in result)
                {
                    outputColletion.Add(item);
                    Console.WriteLine(item);
                }
                WriteJSONAsync("pokemonsByEggs", outputColletion);
            }
        }
        private static void SelectMaxSpawnChance()
        {
            var result = from elem in globalPokemons
                         where elem.spawn_chance == globalPokemons.Max(i => i.spawn_chance)
                         select elem;

            PokemonCollection outputColletion = new PokemonCollection();
            var maxChancePokemon = result.First();
            if (maxChancePokemon != null)
            {
                Console.WriteLine("Name:\t" + maxChancePokemon.name + "\nChance:\t" + maxChancePokemon.spawn_chance);
                outputColletion.Add(maxChancePokemon);
                WriteJSONAsync("maxSpawnChance", outputColletion);
            }
            else
            {
                Console.WriteLine("Invalid érték");
            }
        }
        private static void SelectAvgSpawnChance()
        {
            var result = globalPokemons.Average(i => i.spawn_chance);

            if (result != null)
            {
                Console.WriteLine("Average chance:\t" + Math.Round(result, 2));
            }
            else
            {
                Console.WriteLine("Invalid érték");
            }
        }

        public static List<string> OptionListing(string propertyKey, bool sorted)
        {
            List<string> optionNamesList = new List<string>();
            List<string> optionList = new List<string>();
            PropertyInfo[] properties = typeof(Pokemon).GetProperties();

            Array.ForEach(properties, item => optionNamesList.Add(item.Name));

            if (!optionNamesList.Any(n => n.Equals(propertyKey.ToLower(), StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("[Error] Invalid option in " + nameof(OptionListing));
                return null;
            }

            if (typeof(Pokemon).GetProperty(propertyKey).PropertyType.IsGenericType &&
                typeof(IEnumerable).IsAssignableFrom(typeof(Pokemon).GetProperty(propertyKey).PropertyType))
            {
                foreach (var pokemon in globalPokemons)
                {
                    foreach (var item in (IEnumerable)pokemon.GetType().GetProperty(propertyKey).GetValue(pokemon))
                    {
                        if (!optionList.Contains(item))
                        {
                            optionList.Add(item.ToString());
                        }
                    }
                }
            }
            else
            {
                var result = from pokemon in globalPokemons
                             orderby pokemon.GetType().GetProperty(propertyKey).GetValue(pokemon) descending
                             group pokemon by pokemon.GetType().GetProperty(propertyKey).GetValue(pokemon) into list
                             select list;

                foreach (var item in result)
                {
                    optionList.Add((string)item.Key);
                }
            }


            if (sorted)
            {
                optionList.Sort();
            }


            Console.Write("\nOpciók: ");
            optionList.ForEach(n => Console.Write("[" + n + "] "));
            Console.WriteLine();

            return optionList;
        }
        private static List<Pokemon> ReadJSON()
        {
            string fileName = @"pokemon.json";
            string path = Path.Combine(AppContext.BaseDirectory, fileName);
            List<Pokemon> pokemons = new List<Pokemon>();
            using (FileStream file = File.OpenRead(path))
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    try
                    {
                        pokemons = JsonSerializer.Deserialize<List<Pokemon>>(reader.ReadToEnd());
                        return pokemons;
                    }
                    catch (IOException ioEx)
                    {
                        Console.WriteLine("Hiba keletkezett a beolvasás során: [" + ioEx.Message + "]");
                    }
                    catch (Exception ex) //Pokemon exception handling hehe
                    {
                        Console.WriteLine("Hiba keletkezett: [" + ex.Message + "]");
                    }
                    Console.WriteLine("Kilépéshez nyomjon meg egy gombot");
                    Console.ReadKey();
                    Environment.Exit(1);
                    return null;
                }
            }
        }



        private static async Task WriteJSONAsync(string fileName, PokemonCollection output)
        {
            string json = JsonSerializer.Serialize(output, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            using (StreamWriter writer = new StreamWriter(fileName + ".json"))
            {
                await writer.WriteAsync(json);
            }
        }


        private static void MakeCollectionAndListPokemons(bool listing, PokemonCollection pokemonCollection)
        {
            var readedPokemons = ReadJSON();
            foreach (var item in readedPokemons)
            {
                pokemonCollection.Add(item);
            }

            if (listing)
            {
                readedPokemons.ForEach(n => Console.WriteLine(n));
            }
        }
    }
}