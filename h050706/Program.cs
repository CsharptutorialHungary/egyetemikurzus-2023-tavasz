using h050706.IEnumPokemon;
using Newtonsoft.Json;

namespace Kotprog
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var pokemons = AddToCollection();
            if (pokemons != null)
            {
                foreach (var item in pokemons)
                {
                    Console.WriteLine(item);
                }
            }
        }
        
        static PokemonCollection AddToCollection()
        {
            var pokemons = ReadJSON();
            var pokemonCollection = new PokemonCollection();

            foreach (var item in pokemons)
            {
                pokemonCollection.Add(item);
            }
            return pokemonCollection;
        }
        
        static List<Pokemon>? ReadJSON()
        {
            string fileName = @"pokemon.json";
            try
            {
                var pokemons = JsonConvert.DeserializeObject<List<Pokemon>>(File.ReadAllText(fileName));
                return pokemons;
            }
            catch (IOException ioEx)
            {
                Console.WriteLine(ioEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
    }
}