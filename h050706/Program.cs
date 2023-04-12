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
            Console.WriteLine("Kérlek válaszd ki mit szeretnél csinálni: ");
            Console.WriteLine("\t 0. Kilépés");
            Console.WriteLine("\t 1. Pokémonok listázása a megadott tojáskategória alapján");
            Console.WriteLine("\t 2. Megadott típusú Pokémonok listázása a keltetéshez szükséges Candy alapján");
            Console.Write("\nAdd meg a sorszámot: ");
        }

        private static void SelectMethods()
        {
            Console.WriteLine("Üdvözöllek a Pokémonokkal foglalkozó programomban\n");
            WriteInstructions();
            string selectMethod = "";
            while (selectMethod != "0")
            {
                selectMethod = Console.ReadLine();
                switch (selectMethod)
                {
                    case "0":
                        break;
                    case "1":
                        SelectPokemonsByEggs();
                        Console.WriteLine("---Lekérdezés vége---\n\n");
                        WriteInstructions();
                        break;
                    case "2":
                        SelectNameByTypeOrderByCandyCount();
                        Console.WriteLine("---Lekérdezés vége---\n\n");
                        WriteInstructions();
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
            //Console.WriteLine(result.GetType());

            if (result.Any())
            {
                foreach (var item in result)
                {
                    Console.WriteLine(item);
                }
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