using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using B8L0TF.Models;


namespace B8L0TF.Controller
{
    internal class Controller
    {

        private string _command = "";
        private User user;

        public void init()
        {
            start();
        }

        public void destroy() { }

        public void start()
        {
            Console.WriteLine("Start!");
            readUserName();

            while (true)
            {
                Console.WriteLine("Adj meg egy parancsot:");
                _command = Console.ReadLine();

                if (!IsCommandInvalid()) runCommand();
                else Console.WriteLine("Nem adtal meg parancsot!");
            }
        }

        private void runCommand()
        {
            throw new NotImplementedException();
        }

        private bool IsCommandInvalid()
        {
            return _command == null || _command.Trim() == "";
        }


        public void readUserName()
        {
            Console.WriteLine("Add meg a jatekos nevet!");
            user.Name = Console.ReadLine();

            int line = 0;

            while (!int.TryParse(user.Name, out line))
            {
                Console.WriteLine("Try again.");
                user.Name = Console.ReadLine();
            }
        }

        
    }
}
