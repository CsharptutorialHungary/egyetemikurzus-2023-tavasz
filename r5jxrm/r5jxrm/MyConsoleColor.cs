using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _r5jxrm_;

namespace _r5jxrm_
{
    public record class MyConsoleColor
    {
        public List<ConsoleColor> colorsList;
        /*
        public void hozzaadas(List<ConsoleColor> colorsList)
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
        }*/

        public void kiiratas(List<ConsoleColor> colorsList)
        {
            ConsoleColor[] consoleColors = {ConsoleColor.Black, ConsoleColor.DarkBlue, ConsoleColor.DarkGreen, ConsoleColor.DarkCyan, ConsoleColor.DarkRed,
            ConsoleColor.DarkMagenta, ConsoleColor.DarkYellow, ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Blue, ConsoleColor.Green, ConsoleColor.Cyan,
            ConsoleColor.Red, ConsoleColor.Magenta, ConsoleColor.Yellow, ConsoleColor.White};
            colorsList.AddRange(consoleColors);
            foreach (var szinek in colorsList)
            {
                Console.WriteLine($"A választható színek: {szinek}");
            }
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
