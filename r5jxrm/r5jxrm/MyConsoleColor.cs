using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _r5jxrm_;

namespace _r5jxrm_
{
    public record class MyConsoleColor
    {
        public List<ConsoleColor> colorsList;

        public void adding(List<ConsoleColor> colorsList)
        {
            colorsList.Add(ConsoleColor.Black);
            colorsList.Add(ConsoleColor.DarkBlue);
            colorsList.Add(ConsoleColor.DarkGreen);
            colorsList.Add(ConsoleColor.DarkCyan);
            colorsList.Add(ConsoleColor.DarkRed);
            colorsList.Add(ConsoleColor.DarkMagenta);
            colorsList.Add(ConsoleColor.DarkYellow);
            colorsList.Add(ConsoleColor.Gray);
            colorsList.Add(ConsoleColor.DarkGray);
            colorsList.Add(ConsoleColor.Blue);
            colorsList.Add(ConsoleColor.Green);
            colorsList.Add(ConsoleColor.Cyan);
            colorsList.Add(ConsoleColor.Red);
            colorsList.Add(ConsoleColor.Magenta);
            colorsList.Add(ConsoleColor.Yellow);
            colorsList.Add(ConsoleColor.White);
        }


        public override string ToString()
        {
            foreach (ConsoleColor color in colorsList)
            {
                return color.ToString();
            }
            return "Black";
        }
    }
}
