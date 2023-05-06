using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using B8L0TF.Models;
using B8L0TF.Commands;


namespace B8L0TF.Controller
{
    internal class Controller
    {

        private string _command = "";
        private User user = new User();
        private LessonsController lessonsController = new LessonsController();

        public void init()
        {
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
                else Console.WriteLine("Nem adtal meg parancsot!");
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
                case Commands.Commands.CreateGame:
                    CreateGameLoop();
                    break;
                case Commands.Commands.Exit:
                    ExitControllerLoop();
                    break;
                default: Console.WriteLine("Nincs ilyen Parancs!");
                    break;
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

        private void WritePlayedGamesStats()
        {
            throw new NotImplementedException();
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
