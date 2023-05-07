using B8L0TF.Models;
using B8L0TF.Json;

namespace B8L0TF.Controller
{
    internal class Controller
    {
        public Dictionary<string,string> Commands = new() { { "help" , "kiirja az osszes hasznalahto parancsot"} , { "list" , "listazza az eddig lementett jatekokat"}, { "start", "uj jatek inditasa" }, { "exit", "kilepes a programbol" }, { "myscore", "kiirja az eddig elert legjobb eredmenyed" }, { "clear", "torli az osszes parancsot a console-rol" } };
        private ReadAndWriteJson ReadAndWriteJson = new();
        private string _command = "";
        private readonly User user = new();
        private readonly LessonsController lessonsController = new();


        public void init()
        {
            ReadAndWriteJson.InitPlayedGames();
            start();
        }

        public void start()
        {
            Console.WriteLine("Start!");
            ReadUserName();
            Console.WriteLine($"Udv {user.Name}");
            Console.WriteLine("Adj meg egy parancsot:\t(help)");
            while (true)
            {
                _command = Console.ReadLine();

                if (!IsCommandInvalid()) RunCommand();
                else Console.WriteLine("Nem adtal meg ervenyes parancsot!");
            }
        }

        private void RunCommand()
        {
            switch (_command)
            {
                case "help":
                    WriteCommands();
                    break;
                case "list":
                    WritePlayedGamesStats();
                    break;
                case "start":
                    CreateGameLoop();
                    break;
                case "exit":
                    ExitControllerLoop();
                    break;
                case "myscore":
                    getMyBestScore();
                    break;
                case "clear":
                    Console.Clear();
                    break;
                default: Console.WriteLine("Nincs ilyen Parancs!");
                    break;
            }
       
        }

        private void getMyBestScore()
        {
            var bestScore = ReadAndWriteJson.getUserBestScore(user.Name);
            if (bestScore != -1)
            {
            Console.WriteLine($"A legjobb eredmenyed: {bestScore}");
            }
            else
            {
                Console.WriteLine("Nincs meg elmentve eredmenyed.");
            }
        }

        private static void ExitControllerLoop()
        {
            Environment.Exit(0);
        }

        private void CreateGameLoop()
        {
            lessonsController.Init(user.Name);
        }

        private async void WritePlayedGamesStats()
        {
            List<Game> games = await ReadAndWriteJson.ReadPlayedGames();

            foreach (Game game in games)
            {
                Console.WriteLine(game.ToString());
            }

        }

        private void WriteCommands()
        {
            Console.WriteLine("Parancsok listaja");
            foreach(var command in Commands)
            {
                Console.WriteLine($"{command.Key}\t {command.Value}");
            }
        }

        private bool IsCommandInvalid()
        {
            return _command == null || _command.Trim() == "";
        }


        public void ReadUserName()
        {
            Console.WriteLine("Add meg a jatekos nevet!");
            user.Name = Console.ReadLine();

            while (user.Name == null)
            {
                Console.WriteLine("Ervenytelen nev adj meg egy ujat.");
                user.Name = Console.ReadLine();
            }
        }
    }
}
