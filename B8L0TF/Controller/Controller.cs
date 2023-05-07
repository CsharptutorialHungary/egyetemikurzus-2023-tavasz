using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using B8L0TF.Models;
using B8L0TF.Commands;
using B8L0TF.Json;

namespace B8L0TF.Controller
{
    internal class Controller
    {
        private ReadAndWriteJson ReadAndWriteJson = new();
        private string _command = "";
        private readonly User user = new();
        private readonly LessonsController lessonsController = new();


        public async void init()
        {
            //await ReadAndWriteJson.InitPlayedGames();
            start();
        }

        public void start()
        {
            Console.WriteLine("Start!");
            ReadUserName();
            Console.WriteLine($"Udv {user.Name}");
            while (true)
            {
                Console.WriteLine("Adj meg egy parancsot:");
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
                default: Console.WriteLine("Nincs ilyen Parancs!");
                    break;
            }
       
        }

        private void getMyBestScore()
        {
            try
            {
            var bestScore = ReadAndWriteJson.getUserBestScore(user.Name);
            Console.WriteLine($"A legjobb eredmenyed: {bestScore}");

            }catch (Exception) {
                Console.WriteLine("Ezen a neven nincs elmentve jatek."); 
            }
        }

        private static void ExitControllerLoop()
        {
            Environment.Exit(0);
        }

        private async void CreateGameLoop()
        {
           await lessonsController.Init(user.Name);
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
            throw new NotImplementedException();
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
