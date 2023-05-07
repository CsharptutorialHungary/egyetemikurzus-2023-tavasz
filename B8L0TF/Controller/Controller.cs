using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using B8L0TF.Models;
using B8L0TF.Commands;
using B8L0TF.Json;
using System.Reflection;

namespace B8L0TF.Controller
{
    internal class Controller
    {
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
                case Commands.Commands.Help:
                    WriteCommands();
                    break;
                case Commands.Commands.ListGames:
                    WritePlayedGamesStats();
                    break;
                case Commands.Commands.StartGame:
                    CreateGameLoop();
                    break;
                case Commands.Commands.Exit:
                    ExitControllerLoop();
                    break;
                case Commands.Commands.MyScore:
                    getMyBestScore();
                    break;
                case Commands.Commands.Clear:
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
            var finfos = typeof(Commands.Commands).GetFields(BindingFlags.NonPublic | BindingFlags.Public |
                  BindingFlags.Static);
            foreach (var finfo in finfos)
            {
                Console.WriteLine("\t " + $"{finfo.GetValue(null)}");
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
