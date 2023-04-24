using _r5jxrm_;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Runtime.Serialization;


namespace ProgramTest
{
    [TestClass]
    public class ProgrammingTest
    {
        [TestMethod]
        public void Keveres_KevertElemekATombben()
        {
            var kartyaPakli = new[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
            var eredetiPakli = new string[kartyaPakli.Length];
            Array.Copy(kartyaPakli, eredetiPakli, kartyaPakli.Length);

            var kevertpakli = Keveres(kartyaPakli);

            Assert.AreNotEqual(eredetiPakli, kevertpakli);
            CollectionAssert.AreEquivalent(eredetiPakli, kevertpakli);

        }
        static public string[] Keveres(string[] pakli)
        {
            Random kevero = new Random();
            int keveroindex;
            int keveroindex2;
            string temp;
            for (int i = 0; i < 10000; i++)
            {
                keveroindex = kevero.Next(0, pakli.Length);
                keveroindex2 = kevero.Next(0, pakli.Length);
                temp = pakli[keveroindex];
                pakli[keveroindex] = pakli[keveroindex2];
                pakli[keveroindex2] = temp;
            }
            return pakli;
        }

        static public int checkHaVanAsz(string[] pakli)
        {
            bool containsAsz = Array.Exists(pakli, element => element == "A");
            if (!containsAsz)
            {
                return -1;
            }

            int index = -1;
            bool vegzett = false;
            while (!vegzett)
            {
                index++;
                if (pakli[index] == "A" || index == pakli.Length - 1)
                {
                    vegzett = true;
                }
            }
            return index;
        }
        [TestMethod]
        public void checkHaVanAsz_VanAszBenne()
        {
            string[] pakliWithAsz = { "3", "9", "10", "A", "K", "7" };
            int indexWithAsz = checkHaVanAsz(pakliWithAsz);

            Assert.AreEqual(3, indexWithAsz);
        }
        [TestMethod]
        public void checkHaVanAsz_NincsAszBenne()
        {
            string[] pakliWithoutAsz = { "2", "2", "2", "2" };
            int indexWithoutAsz = checkHaVanAsz(pakliWithoutAsz);

            Assert.AreEqual(-1, indexWithoutAsz);
        }

        static private int convertToInt(string[] pakli, int index)
        {
            int returnValue;
            switch (pakli[index])
            {
                case "A":
                    returnValue = 11;
                    break;
                case "K":
                case "Q":
                case "J":
                case "10":
                    returnValue = 10;
                    break;
                case "9":
                    returnValue = 9;
                    break;
                case "8":
                    returnValue = 8;
                    break;
                case "7":
                    returnValue = 7;
                    break;
                case "6":
                    returnValue = 6;
                    break;
                case "5":
                    returnValue = 5;
                    break;
                case "4":
                    returnValue = 4;
                    break;
                case "3":
                    returnValue = 3;
                    break;
                case "2":
                    returnValue = 2;
                    break;
                default:
                    returnValue = 0;
                    break;
            }
            return returnValue;
        }

        [TestMethod]
        public void convertToInt_HelyesKonvertalas()
        {
            string[] pakli = { "A", "K", "Q", "J", "10", "9", "8", "7", "6", "5", "4", "3", "2" };
            int[] vartErtekek = { 11, 10, 10, 10, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int[] aktualisErtekek = new int[vartErtekek.Length];
            for (int i = 0; i < pakli.Length; i++)
            {
                aktualisErtekek[i] = convertToInt(pakli, i);
            }

            CollectionAssert.AreEqual(vartErtekek, aktualisErtekek);
        }

        /*
        static void szinek(string beszin, List<ConsoleColor> szineklista)
        {
            switch (beszin)
            {
                case "black":
                    Console.BackgroundColor = szineklista[0]; //Console.BackgroundColor = ConsoleColor.Black
                    Console.ForegroundColor = szineklista[15];
                    break;
                case "darkblue":
                    Console.BackgroundColor = szineklista[1]; //Console.BackgroundColor = ConsoleColor.DarkBlue
                    break;
                case "darkgreen":
                    Console.BackgroundColor = szineklista[2]; //Console.BackgroundColor = ConsoleColor.DarkGreen
                    Console.ForegroundColor = szineklista[15]; //Console.ForegroundColor = ConsoleColor.White
                    break;
                case "darkcyan":
                    Console.BackgroundColor = szineklista[3]; //Console.BackgroundColor = ConsoleColor.DarkCyan
                    Console.ForegroundColor = szineklista[15]; //Console.ForegroundColor = ConsoleColor.White
                    break;
                case "darkred":
                    Console.BackgroundColor = szineklista[4]; //Console.BackgroundColor = ConsoleColor.DarkRed
                    Console.ForegroundColor = szineklista[15]; //Console.ForegroundColor = ConsoleColor.White
                    break;
                case "darkmagenta":
                    Console.BackgroundColor = szineklista[5]; //Console.BackgroundColor = ConsoleColor.DarkMagenta
                    Console.ForegroundColor = szineklista[15]; //Console.ForegroundColor = ConsoleColor.White
                    break;
                case "darkyellow":
                    Console.BackgroundColor = szineklista[6]; //Console.BackgroundColor = ConsoleColor.DarkYellow
                    Console.ForegroundColor = szineklista[15]; //Console.ForegroundColor = ConsoleColor.White
                    break;
                case "darkgray":
                    Console.BackgroundColor = szineklista[8]; //Console.BackgroundColor = ConsoleColor.DarkGray
                    break;
                case "gray":
                    Console.BackgroundColor = szineklista[7]; //Console.BackgroundColor = ConsoleColor.Gray
                    Console.ForegroundColor = szineklista[0]; //Console.ForegroundColor = ConsoleColor.Black
                    break;
                case "blue":
                    Console.BackgroundColor = szineklista[9]; //Console.BackgroundColor = ConsoleColor.Blue
                    Console.ForegroundColor = szineklista[15]; //Console.ForegroundColor = ConsoleColor.White
                    break;
                case "green":
                    Console.BackgroundColor = szineklista[10]; //Console.BackgroundColor = ConsoleColor.Green
                    Console.ForegroundColor = szineklista[15]; //Console.ForegroundColor = ConsoleColor.White
                    break;
                case "cyan":
                    Console.BackgroundColor = szineklista[11]; //Console.BackgroundColor = ConsoleColor.Cyan
                    Console.ForegroundColor = szineklista[0]; //Console.ForegroundColor = ConsoleColor.Black
                    break;
                case "red":
                    Console.BackgroundColor = szineklista[12]; //Console.BackgroundColor = ConsoleColor.Red
                    Console.ForegroundColor = szineklista[15]; //Console.ForegroundColor = ConsoleColor.White
                    break;
                case "magenta":
                    Console.BackgroundColor = szineklista[13]; //Console.BackgroundColor = ConsoleColor.Magenta
                    Console.ForegroundColor = szineklista[15]; //Console.ForegroundColor = ConsoleColor.White
                    break;
                case "yellow":
                    Console.BackgroundColor = szineklista[14]; //Console.BackgroundColor = ConsoleColor.Yellow
                    Console.ForegroundColor = szineklista[0]; //Console.ForegroundColor = ConsoleColor.Black
                    break;
                case "white":
                    Console.BackgroundColor = szineklista[15]; //Console.BackgroundColor = ConsoleColor.White
                    Console.ForegroundColor = szineklista[0]; //Console.ForegroundColor = ConsoleColor.Black
                    break;
                default:
                    Console.WriteLine("Hibás szín! Adj meg létezõ színt a listából!");
                    var beszin2 = Console.ReadLine();
                    beszin2 = beszin2.ToLower();
                    szinek(beszin2, szineklista);
                    break;
            }

        }

        [TestMethod]
        public void szinek_ElsoreHelyesSzin()
        {
            var szineklista = new List<ConsoleColor> {
                ConsoleColor.Black,
                ConsoleColor.DarkBlue,
                ConsoleColor.DarkGreen,
                ConsoleColor.DarkCyan,
                ConsoleColor.DarkRed,
                ConsoleColor.DarkMagenta,
                ConsoleColor.DarkYellow,
                ConsoleColor.Gray,
                ConsoleColor.DarkGray,
                ConsoleColor.Blue,
                ConsoleColor.Green,
                ConsoleColor.Cyan,
                ConsoleColor.Red,
                ConsoleColor.Magenta,
                ConsoleColor.Yellow,
                ConsoleColor.White
            };
            szinek("red", szineklista);
            Assert.AreEqual(ConsoleColor.Red, Console.BackgroundColor);
            Assert.AreEqual(ConsoleColor.White, Console.ForegroundColor);
        }*/

        [TestMethod]
        public void DeserializeTest()
        {
            Deser des = new Deser();
            // Arrange
            var expected = new Osszeg
            {
                nyeremeny = 0,
                osszesNyeremeny = 0,
                legnagyobbOsszeg = 0
            };

            // Act
            var result = des.DeserializeTheObject();

            // Assert
            Assert.AreEqual(expected.nyeremeny, result.nyeremeny);
            Assert.AreEqual(expected.osszesNyeremeny, result.osszesNyeremeny);
            Assert.AreEqual(expected.legnagyobbOsszeg, result.legnagyobbOsszeg);
        }



    }

}