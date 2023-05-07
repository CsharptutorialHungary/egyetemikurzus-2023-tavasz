using B8L0TF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace B8L0TF.Json
{
    internal class ReadAndWriteJson
    {

        private List<Game> _gameList  = new();

        private readonly string _FilePath = Path.Combine(AppContext.BaseDirectory, "results.txt");


        public void AddGameToList(string username, int result)
        {
            _gameList.Add(
                new Game(
                    Guid.NewGuid().ToString(),
                    username,
                    result));
        }

        public List<Game> GetGameList() {
            return _gameList; 
        }

        public async void SaveGames ()
        {
            await WriteGamesToFile(GetGameList()); 
        }


        public async Task InitPlayedGames()
        {
            _gameList = await ReadPlayedGames();
        }

        public async Task<List<Game>> ReadPlayedGames()
        {

            var gameList = new List<Game>();

            try
            {
                var SerializedGameList = await File.ReadAllTextAsync(_FilePath);
                gameList = JsonSerializer.Deserialize<List<Game>>(SerializedGameList, new JsonSerializerOptions { WriteIndented = true });
            }
            catch (FileNotFoundException)
            {
                await WriteGamesToFile(new List<Game>());
            }
            catch (IOException)
            {
                Console.WriteLine("Hiba a Json olvasasa kozben");
            }
            return gameList ?? new List<Game>();
        }

        public async Task WriteGamesToFile(List<Game> Games)
        {
            await using var file = File.OpenWrite(_FilePath);
            await using var writer = new StreamWriter(file);

            try
            {
                await writer.WriteLineAsync(JsonSerializer.Serialize(Games, new JsonSerializerOptions { WriteIndented = true }));
            }
            catch (IOException)
            {
                Console.WriteLine("Hiba a Json irasa kozben");
            }
        }

        internal int getUserBestScore(string username)
        {
                var result = _gameList
                    .Where(x => x.Username == username)
                    .OrderBy(x => x)
                    .FirstOrDefault();

            if (result != null)
            {
                return result.Result;
            }
            return -1;
        }
    }
}