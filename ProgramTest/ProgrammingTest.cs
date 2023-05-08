using _r5jxrm_;
using System.Xml.Serialization;

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

            Assert.AreEqual(0, indexWithoutAsz);
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

        [TestMethod]
        public void valasztasMethodTest_HaTetetAkarRakni()
        {
            double penz = 500;
            string valasztasa = "1";
            bool eredmeny = valasztas(penz, valasztasa);

            Assert.IsFalse(eredmeny);
        }

        [TestMethod]
        public void valasztasMethodTest_HaKilepniAkar()
        {
            double penz = 500;
            string valasztasa = "2";
            bool eredmeny = valasztas(penz, valasztasa);

            Assert.IsTrue(eredmeny);
        }

        [TestMethod]
        public void MegadasMethodTest_FalseMinden()
        {
            double tet = 500;
            bool vesztett = false;
            bool isPlaying = false;
            double eredmeny = MegadásMethod(tet, out vesztett, out isPlaying);

            Assert.AreEqual(eredmeny, tet / 2);
        }

        [TestMethod]
        public void MegadasMethodTest_TrueMinden()
        {
            double tet = 500;
            bool vesztett = true;
            bool isPlaying = true;
            double eredmeny = MegadásMethod(tet, out vesztett, out isPlaying);

            Assert.AreEqual(eredmeny, tet / 2);
        }

        [TestMethod]
        public void MegadasMethodTest_NegativErtekuTet()
        {
            //Kamu teszt, mert a program többi része nem engedné, hogy negatív legyen a tét, viszont így kiragadva meg lehet csinálni
            double tet = -500;
            bool vesztett = false;
            bool isPlaying = false;
            double eredmeny = MegadásMethod(tet, out vesztett, out isPlaying);

            Assert.AreEqual(eredmeny, tet / 2);
        }

        [TestMethod]
        public void VegeredmenyHaNemValasztottSzetTest_Dontetlen_BiztositottNelkul()
        {
            double tet = 500;
            bool vesztett = false;
            bool nyert = false;
            double biztositottZseton = 0;
            int osztoKartyaErtek = 21;
            int jatekosKartyaErtek = 21;
            double eredmeny = VegeredmenyHaNemValasztottSzet(ref tet, vesztett, nyert, biztositottZseton, osztoKartyaErtek, jatekosKartyaErtek);

            Assert.AreEqual(eredmeny, tet);
        }

        [TestMethod]
        public void VegeredmenyHaNemValasztottSzetTest_Dontetlen_Biztositott()
        {
            double tet = 500;
            bool vesztett = false;
            bool nyert = false;
            double biztositottZseton = 200;
            int osztoKartyaErtek = 21;
            int jatekosKartyaErtek = 21;
            double eredmeny = VegeredmenyHaNemValasztottSzet(ref tet, vesztett, nyert, biztositottZseton, osztoKartyaErtek, jatekosKartyaErtek);

            Assert.AreEqual(eredmeny, -200);
        }

        [TestMethod]
        public void VegeredmenyHaNemValasztottSzetTest_NyertEsBiztositottIs()
        {
            double tet = 500;
            bool vesztett = false;
            bool nyert = false;
            double biztositottZseton = 200;
            int osztoKartyaErtek = 20;
            int jatekosKartyaErtek = 21;
            double eredmeny = VegeredmenyHaNemValasztottSzet(ref tet, vesztett, nyert, biztositottZseton, osztoKartyaErtek, jatekosKartyaErtek);

            Assert.AreEqual(eredmeny, 300);
        }

        [TestMethod]
        public void VegeredmenyHaNemValasztottSzetTest_NyertEsNemBiztositott()
        {
            double tet = 500;
            bool vesztett = false;
            bool nyert = false;
            double biztositottZseton = 0;
            int osztoKartyaErtek = 20;
            int jatekosKartyaErtek = 21;
            double eredmeny = VegeredmenyHaNemValasztottSzet(ref tet, vesztett, nyert, biztositottZseton, osztoKartyaErtek, jatekosKartyaErtek);

            Assert.AreEqual(eredmeny, 500);
        }

        [TestMethod]
        public void VegeredmenyHaNemValasztottSzetTest_VesztettEsBiztositott()
        {
            double tet = 500;
            bool vesztett = true;
            bool nyert = false;
            double biztositottZseton = 200;
            int osztoKartyaErtek = 21;
            int jatekosKartyaErtek = 20;
            double eredmeny = VegeredmenyHaNemValasztottSzet(ref tet, vesztett, nyert, biztositottZseton, osztoKartyaErtek, jatekosKartyaErtek);

            Assert.AreEqual(eredmeny, -700);
        }

        [TestMethod]
        public void VegeredmenyHaNemValasztottSzetTest_VesztettEsNemBiztositott()
        {
            double tet = 500;
            bool vesztett = false;
            bool nyert = false;
            double biztositottZseton = 0;
            int osztoKartyaErtek = 21;
            int jatekosKartyaErtek = 20;
            double eredmeny = VegeredmenyHaNemValasztottSzet(ref tet, vesztett, nyert, biztositottZseton, osztoKartyaErtek, jatekosKartyaErtek);

            Assert.AreEqual(eredmeny, -500);
        }

        [TestMethod]
        public void VegeredmenyHaSzetvalasztottTest_NyertEsNemBiztositott()
        {
            double tet = 500;
            bool splitLost = false;
            bool vesztett = false;
            bool nyert = true;
            double biztositottZseton = 0;
            int osztoKartyaErtek = 21;
            int jatekosKartyaErtek = 21;
            int jatekosSzetvalasztottKartyaErtek = 10;

            double eredmeny = VegeredmenyHaSzetvalasztott(ref tet, splitLost, vesztett, nyert, biztositottZseton, osztoKartyaErtek, jatekosKartyaErtek, jatekosSzetvalasztottKartyaErtek);

            Assert.AreEqual(eredmeny, 500);
        }

        [TestMethod]
        public void VegeredmenyHaSzetvalasztottTest_NyertEsBiztositott()
        {
            double tet = 500;
            bool splitLost = false;
            bool vesztett = false;
            bool nyert = true;
            double biztositottZseton = 200;
            int osztoKartyaErtek = 21;
            int jatekosKartyaErtek = 21;
            int jatekosSzetvalasztottKartyaErtek = 10;

            double eredmeny = VegeredmenyHaSzetvalasztott(ref tet, splitLost, vesztett, nyert, biztositottZseton, osztoKartyaErtek, jatekosKartyaErtek, jatekosSzetvalasztottKartyaErtek);

            Assert.AreEqual(eredmeny, 300);
        }

        [TestMethod]
        public void VegeredmenyHaSzetvalasztottTest_DontetlenEsNemBiztositott()
        {
            double tet = 500;
            bool splitLost = false;
            bool vesztett = false;
            bool nyert = true;
            double biztositottZseton = 0;
            int osztoKartyaErtek = 20;
            int jatekosKartyaErtek = 19;
            int jatekosSzetvalasztottKartyaErtek = 21;

            double eredmeny = VegeredmenyHaSzetvalasztott(ref tet, splitLost, vesztett, nyert, biztositottZseton, osztoKartyaErtek, jatekosKartyaErtek, jatekosSzetvalasztottKartyaErtek);

            Assert.AreEqual(eredmeny, 500);
        }

        [TestMethod]
        public void VegeredmenyHaSzetvalasztottTest_DontetlenEsBiztositott()
        {
            double tet = 500;
            bool splitLost = false;
            bool vesztett = false;
            bool nyert = true;
            double biztositottZseton = 200;
            int osztoKartyaErtek = 20;
            int jatekosKartyaErtek = 19;
            int jatekosSzetvalasztottKartyaErtek = 21;

            double eredmeny = VegeredmenyHaSzetvalasztott(ref tet, splitLost, vesztett, nyert, biztositottZseton, osztoKartyaErtek, jatekosKartyaErtek, jatekosSzetvalasztottKartyaErtek);

            Assert.AreEqual(eredmeny, 300);
        }

        [TestMethod]
        public void VegeredmenyHaSzetvalasztottTest_DontetlenEsNemBiztositottEsEgyenloErtekek()
        {
            double tet = 500;
            bool splitLost = false;
            bool vesztett = false;
            bool nyert = true;
            double biztositottZseton = 0;
            int osztoKartyaErtek = 21;
            int jatekosKartyaErtek = 21;
            int jatekosSzetvalasztottKartyaErtek = 21;

            double eredmeny = VegeredmenyHaSzetvalasztott(ref tet, splitLost, vesztett, nyert, biztositottZseton, osztoKartyaErtek, jatekosKartyaErtek, jatekosSzetvalasztottKartyaErtek);

            Assert.AreEqual(eredmeny, 500);
        }

        [TestMethod]
        public void VegeredmenyHaSzetvalasztottTest_DontetlenEsBiztositottEsEgyenloErtekek()
        {
            double tet = 500;
            bool splitLost = false;
            bool vesztett = false;
            bool nyert = true;
            double biztositottZseton = 200;
            int osztoKartyaErtek = 21;
            int jatekosKartyaErtek = 21;
            int jatekosSzetvalasztottKartyaErtek = 21;

            double eredmeny = VegeredmenyHaSzetvalasztott(ref tet, splitLost, vesztett, nyert, biztositottZseton, osztoKartyaErtek, jatekosKartyaErtek, jatekosSzetvalasztottKartyaErtek);

            Assert.AreEqual(eredmeny, 300);
        }

        [TestMethod]
        public void VegeredmenyHaSzetvalasztottTest_NyertEsSzetvalasztottEsBiztositott()
        {
            double tet = 500;
            bool splitLost = false;
            bool vesztett = false;
            bool nyert = false;
            double biztositottZseton = 200;
            int osztoKartyaErtek = 20;
            int jatekosKartyaErtek = 21;
            int jatekosSzetvalasztottKartyaErtek = 20;

            double eredmeny = VegeredmenyHaSzetvalasztott(ref tet, splitLost, vesztett, nyert, biztositottZseton, osztoKartyaErtek, jatekosKartyaErtek, jatekosSzetvalasztottKartyaErtek);

            Assert.AreEqual(eredmeny, 50);
        }

        [TestMethod]
        public void VegeredmenyHaSzetvalasztottTest_NyertEsSzetvalasztottEsNemBiztositott()
        {
            double tet = 500;
            bool splitLost = false;
            bool vesztett = false;
            bool nyert = false;
            double biztositottZseton = 0;
            int osztoKartyaErtek = 20;
            int jatekosKartyaErtek = 21;
            int jatekosSzetvalasztottKartyaErtek = 20;

            double eredmeny = VegeredmenyHaSzetvalasztott(ref tet, splitLost, vesztett, nyert, biztositottZseton, osztoKartyaErtek, jatekosKartyaErtek, jatekosSzetvalasztottKartyaErtek);

            Assert.AreEqual(eredmeny, 250);
        }

        [TestMethod]
        public void VegeredmenyHaSzetvalasztottTest_VesztettEsNemBiztositott()
        {
            double tet = 500;
            bool splitLost = false;
            bool vesztett = false;
            bool nyert = false;
            double biztositottZseton = 0;
            int osztoKartyaErtek = 21;
            int jatekosKartyaErtek = 21;
            int jatekosSzetvalasztottKartyaErtek = 20;

            double eredmeny = VegeredmenyHaSzetvalasztott(ref tet, splitLost, vesztett, nyert, biztositottZseton, osztoKartyaErtek, jatekosKartyaErtek, jatekosSzetvalasztottKartyaErtek);

            Assert.AreEqual(eredmeny, -250);
        }

        [TestMethod]
        public void VegeredmenyHaSzetvalasztottTest_VesztettEsBiztositott()
        {
            double tet = 500;
            bool splitLost = false;
            bool vesztett = false;
            bool nyert = false;
            double biztositottZseton = 200;
            int osztoKartyaErtek = 21;
            int jatekosKartyaErtek = 21;
            int jatekosSzetvalasztottKartyaErtek = 20;

            double eredmeny = VegeredmenyHaSzetvalasztott(ref tet, splitLost, vesztett, nyert, biztositottZseton, osztoKartyaErtek, jatekosKartyaErtek, jatekosSzetvalasztottKartyaErtek);

            Assert.AreEqual(eredmeny, -450);
        }

        [TestMethod]
        public void VegeredmenyHaSzetvalasztottTest_VesztettEsNagyobbOsztoErtekEsNemBiztositott()
        {
            double tet = 500;
            bool splitLost = true;
            bool vesztett = false;
            bool nyert = false;
            double biztositottZseton = 0;
            int osztoKartyaErtek = 21;
            int jatekosKartyaErtek = 20;
            int jatekosSzetvalasztottKartyaErtek = 20;

            double eredmeny = VegeredmenyHaSzetvalasztott(ref tet, splitLost, vesztett, nyert, biztositottZseton, osztoKartyaErtek, jatekosKartyaErtek, jatekosSzetvalasztottKartyaErtek);

            Assert.AreEqual(eredmeny, -500);
        }

        [TestMethod]
        public void VegeredmenyHaSzetvalasztottTest_VesztettEsNagyobbOsztoErtekEsBiztositott()
        {
            double tet = 500;
            bool splitLost = true;
            bool vesztett = false;
            bool nyert = false;
            double biztositottZseton = 200;
            int osztoKartyaErtek = 21;
            int jatekosKartyaErtek = 20;
            int jatekosSzetvalasztottKartyaErtek = 20;

            double eredmeny = VegeredmenyHaSzetvalasztott(ref tet, splitLost, vesztett, nyert, biztositottZseton, osztoKartyaErtek, jatekosKartyaErtek, jatekosSzetvalasztottKartyaErtek);

            Assert.AreEqual(eredmeny, -700);
        }

        [TestMethod]
        public void DeserializeTheObjectTest_HelyesMukodes()
        {
            string filename = "MentesOsszeg.xml";
            Osszeg elvart = new Osszeg
            {
                nyeremeny = 1000,
                osszesNyeremeny = 5000,
                legnagyobbOsszeg = 2500
            };
            XmlSerializer serializer = new XmlSerializer(typeof(Osszeg));
            using (StreamWriter writer = new StreamWriter(filename))
            {
                serializer.Serialize(writer, elvart);
            }

            Osszeg aktualis = DeserializeTheObject();

            Assert.AreEqual(elvart.nyeremeny, aktualis.nyeremeny);
            Assert.AreEqual(elvart.osszesNyeremeny, aktualis.osszesNyeremeny);
            Assert.AreEqual(elvart.legnagyobbOsszeg, aktualis.legnagyobbOsszeg);

            File.Delete(filename);
        }

        [TestMethod]
        public void MaxTest()
        {
            Deser method = new Deser();
            Osszeg osszeg = new Osszeg
            {
                nyeremeny = 1000,
                osszesNyeremeny = 5000,
                legnagyobbOsszeg = 2500
            };
            string filename = "MentesOsszeg.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(Osszeg));
            using (StreamWriter writer = new StreamWriter(filename))
            {
                serializer.Serialize(writer, osszeg);
            }
            double aktualis = 3000;
            double expected = 3000;
            double actual = method.max(aktualis);
            Assert.AreEqual(expected, actual);

            File.Delete(filename);
        }

        [TestMethod]
        public void OsszeadasTest() 
        {
            Deser method = new Deser();
            Osszeg osszeg = new Osszeg
            {
                nyeremeny = 1000,
                osszesNyeremeny = 5000,
                legnagyobbOsszeg = 2500
            };
            string filename = "MentesOsszeg.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(Osszeg));
            using (StreamWriter writer = new StreamWriter(filename))
            {
                serializer.Serialize(writer, osszeg);
            }

            double aktualis = 3000;
            double expected = 8000;

            double actual = method.osszeadas(aktualis);

            Assert.AreEqual(expected, actual);

            File.Delete(filename);
        }

        [TestMethod]
        public void Kiiratas_SzinekEllenorzese()
        {
            MyConsoleColor myConsoleColor = new MyConsoleColor();
            List<ConsoleColor> expectedColors = new List<ConsoleColor> {
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

            List<ConsoleColor> actualColors = new List<ConsoleColor>();
            myConsoleColor.kiiratas(actualColors);

            CollectionAssert.AreEquivalent(expectedColors, actualColors);
        }

        [TestMethod]
        public void OsztoUtes_HaAzInputHelyes_UpdateOutput()
        {
            string[] paklik = new[] { "10", "4", "7", "A", "Q" };
            bool nyert = false;
            bool isPlaying = true;
            int kartyak = 4;
            int osztoKartyaErtek = 9;
            string[] jatekosKartyai = new[] { "8", "K" };
            string[] osztoKartyai = new string[5];
            int i = 0;

            OsztoUtes(paklik, ref nyert, ref isPlaying, ref kartyak, ref osztoKartyaErtek, jatekosKartyai, osztoKartyai, ref i);

            Assert.AreEqual("Q", osztoKartyai[1]);
            Assert.AreEqual(19, osztoKartyaErtek);
            Assert.AreEqual(3, kartyak);
            Assert.IsTrue(isPlaying);
            Assert.IsFalse(nyert);
        }

        [TestMethod]
        public void KartyaOsztas_Ertekeles_HelyesErtekeketAdVissza()
        {
            string[] paklik = { "2", "5", "A", "J", "9", "10" };
            int kartyak = paklik.Length - 1;
            int osztoKartyaErtek = 0;
            int jatekosKartyaErtek = 0;
            string[] jatekosKartyai = new string[2];
            string[] osztoKartyai = new string[2];
            int i = 0;

            KartyaOsztas(paklik, ref kartyak, ref osztoKartyaErtek, ref jatekosKartyaErtek, jatekosKartyai, osztoKartyai, ref i);

            Assert.AreEqual(2, jatekosKartyai.Length);
            Assert.AreEqual(2, osztoKartyai.Length);
            Assert.AreEqual("10", jatekosKartyai[0]);
            Assert.AreEqual("J", jatekosKartyai[1]);
            Assert.AreEqual("9", osztoKartyai[0]);
            Assert.AreEqual("A", osztoKartyai[1]);
            Assert.AreEqual(20, jatekosKartyaErtek);
            Assert.AreEqual(20, osztoKartyaErtek);
        }

        [TestMethod]
        public void SikeresenBiztositott_Ertek21_JatekosErtekNem21_VesztettEsTetCsokken()
        {
            double tet = 15;
            bool biztositas = true;
            bool vesztett = false;
            bool nyert = false;
            bool isPlaying = true;
            double biztositottZseton = 5;
            int osztoKartyaErtek = 21;
            int jatekosKartyaErtek = 18;

            SikeresenBiztositott(ref tet, ref biztositas, ref vesztett, ref nyert, ref isPlaying, ref biztositottZseton, osztoKartyaErtek, jatekosKartyaErtek);

            Assert.IsFalse(isPlaying);
            Assert.IsTrue(vesztett);
            Assert.IsFalse(nyert);
            Assert.AreEqual(0, biztositottZseton);
            Assert.AreEqual(5, tet);
        }


        //----------------------Metódusok bemásolva-----------------------------

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

        static public int checkHaVanAsz(string[] pakli)
        {
            bool containsAsz = Array.Exists(pakli, element => element == "A");
            if (!containsAsz)
            {
                return 0;
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

        public bool valasztas(double penz, string valasztott)
        {
            //Valamiért ki kell venni a Console-ra írt részeket, így kicsit átírva a method
            bool quit = false;
            bool NoOption = true;
            while (NoOption)
            {
                //Zsetonjaid kiírása
                //Console.WriteLine($"Zsetonjaid száma:  {penz}\n");
                //Választás kiírása
                //Console.WriteLine("Válassz:\n1.Tét\n2.Kilépés");
                //Választási változó deklarálása
                //string valasztott = Console.ReadLine();
                //Választási lehetõségek
                switch (valasztott)
                {
                    case "1":
                        //Console.Clear();
                        NoOption = false;
                        break;
                    case "2":
                        //Console.Clear();
                        quit = true;
                        NoOption = false;
                        break;
                    default:
                        //Console.WriteLine("Rossz lehetõséget választottál");
                        //tovabbLepesSzovegClear();
                        break;
                }
            }
            return quit;
        }

        public static double MegadásMethod(double tet, out bool vesztett, out bool isPlaying)
        {
            tet = tet / 2;
            vesztett = true;
            isPlaying = false;
            return tet;
        }

        public static double VegeredmenyHaNemValasztottSzet(ref double tet, bool vesztett, bool nyert, double biztositottZseton, int osztoKartyaErtek, int jatekosKartyaErtek)
        {
            if (vesztett == false && osztoKartyaErtek < jatekosKartyaErtek || nyert == true && vesztett == false)
            {
                Console.WriteLine($"Nyertél! Nyereményed: {tet}");
                return (tet - biztositottZseton);
            }
            else if (osztoKartyaErtek == jatekosKartyaErtek || nyert == true && vesztett == true)
            {
                Console.WriteLine("Döntetlen! Nincs zseton nyereség vagy veszteség.");
                tet = 0 - biztositottZseton;
                return tet;
            }
            else if (vesztett == true && nyert == false || osztoKartyaErtek > jatekosKartyaErtek)
            {
                Console.WriteLine($"Vesztettél! A veszteség: {tet}");
                tet = -tet - biztositottZseton;
                return tet;
            }
            else
            {
                return tet;
            }
        }

        public static double VegeredmenyHaSzetvalasztott(ref double tet, bool splitLost, bool vesztett, bool nyert, double biztositottZseton, int osztoKartyaErtek, int jatekosKartyaErtek, int jatekosSzetvalasztottKartyaErtek)
        {
            if (vesztett == false && splitLost == false && osztoKartyaErtek < jatekosKartyaErtek && osztoKartyaErtek < jatekosSzetvalasztottKartyaErtek
                                || nyert == true && vesztett == false && splitLost == false)
            {
                Console.WriteLine($"Nyertél! Nyeremény: {tet} zseton");
                return tet - biztositottZseton;
            }
            else if (osztoKartyaErtek == jatekosKartyaErtek && osztoKartyaErtek == jatekosSzetvalasztottKartyaErtek
                || nyert == true && vesztett == true && splitLost == true)
            {
                Console.WriteLine("Döntetlen! Nincs zseton nyeremény vagy veszteség.");
                return 0 - biztositottZseton;
            }
            else if (vesztett == false && splitLost == true && osztoKartyaErtek < jatekosKartyaErtek ||
                vesztett == true && splitLost == false && osztoKartyaErtek < jatekosSzetvalasztottKartyaErtek ||
                vesztett == false && splitLost == false && jatekosSzetvalasztottKartyaErtek < osztoKartyaErtek && osztoKartyaErtek < jatekosKartyaErtek ||
                vesztett == false && splitLost == false && jatekosKartyaErtek < osztoKartyaErtek && osztoKartyaErtek < jatekosSzetvalasztottKartyaErtek ||
                vesztett == false && splitLost == true && nyert == true || vesztett == true && splitLost == false && nyert == true)
            {
                Console.WriteLine("Döntetlen! Nincs zseton nyeremény vagy veszteség.");
                return 0 - biztositottZseton;
            }
            else if (osztoKartyaErtek == jatekosKartyaErtek && osztoKartyaErtek < jatekosSzetvalasztottKartyaErtek && vesztett == false && splitLost == false ||
                osztoKartyaErtek == jatekosSzetvalasztottKartyaErtek && osztoKartyaErtek < jatekosKartyaErtek && vesztett == false && splitLost == false)
            {
                tet = tet / 2;
                Console.WriteLine($"Nyertél! Nyereményed: {tet}");
                return tet - biztositottZseton;
            }
            else if (osztoKartyaErtek == jatekosKartyaErtek && splitLost == true ||
                osztoKartyaErtek == jatekosSzetvalasztottKartyaErtek && vesztett == true ||
                osztoKartyaErtek == jatekosKartyaErtek && osztoKartyaErtek > jatekosSzetvalasztottKartyaErtek && vesztett == false && splitLost == false ||
                osztoKartyaErtek == jatekosSzetvalasztottKartyaErtek && osztoKartyaErtek > jatekosKartyaErtek && vesztett == false && splitLost == false)
            {
                tet = tet / 2;
                Console.WriteLine($"Vesztettél! A veszteség: {tet}");
                return -tet - biztositottZseton;
            }
            else if (vesztett == true && splitLost == true && nyert == false ||
                osztoKartyaErtek > jatekosKartyaErtek && osztoKartyaErtek > jatekosSzetvalasztottKartyaErtek && vesztett == false && splitLost == false ||
                osztoKartyaErtek > jatekosKartyaErtek && splitLost == true ||
                osztoKartyaErtek > jatekosSzetvalasztottKartyaErtek && vesztett == true)
            {
                Console.WriteLine($"Vesztettél: {tet}");
                return -tet - biztositottZseton;
            }
            else
            {
                return tet;
            }
        }

        internal Osszeg DeserializeTheObject()
        {
            string filename = "MentesOsszeg.xml";
            if (!File.Exists(filename))
            {
                Osszeg nonexist = new Osszeg();
                var ser = new XmlSerializer(typeof(Osszeg));
                nonexist.nyeremeny = 0;
                nonexist.osszesNyeremeny = 0;
                nonexist.legnagyobbOsszeg = 0;
                using (var writer = new StreamWriter(filename))
                {
                    ser.Serialize(writer, nonexist);
                }
            }
            //Visszatér a deserializált objektummal
            Osszeg objectToDeserialize = new Osszeg();
            XmlSerializer xmlserializer = new XmlSerializer(objectToDeserialize.GetType());
            using (StreamReader reader = new StreamReader(filename))
            {
                return (Osszeg)xmlserializer.Deserialize(reader);
            }
        }

        public static void OsztoUtes(string[] paklik, ref bool nyert, ref bool isPlaying, ref int kartyak, ref int osztoKartyaErtek, string[] jatekosKartyai, string[] osztoKartyai, ref int i)
        {
            if (convertToInt(paklik, kartyak) == 11 && osztoKartyaErtek + convertToInt(paklik, kartyak) > 21)
            {
                //Növeljük az osztó kártyáit
                i++;
                osztoKartyai[i] = "A - One";
                osztoKartyaErtek += 1;
                Console.WriteLine($"Az osztó megütötte! Az osztó {paklik[kartyak]} lapot kapott, jelenleg {osztoKartyaErtek} van neki");
                kartyak--;
            }
            else
            {
                i++;
                osztoKartyai[i] = paklik[kartyak];
                osztoKartyaErtek += convertToInt(paklik, kartyak);
                Console.WriteLine($"Az osztó megütötte! Az osztó {paklik[kartyak]} lapot kapott, jelenleg {osztoKartyaErtek} van neki");
                kartyak--;
            }
            if (osztoKartyaErtek > 21)
            {
                int index = checkHaVanAsz(jatekosKartyai);
                if (osztoKartyai[index] == "A")
                {
                    osztoKartyai[index] = "A - One";
                    osztoKartyaErtek -= 10;
                    Console.WriteLine($"Az osztónál volt Ász, az értéke 1-re lett állítva, az osztónál jelenleg {osztoKartyaErtek} van");
                }
                else
                {
                    nyert = true;
                    isPlaying = false;
                }
            }
        }

        private static void KartyaOsztas(string[] paklik, ref int kartyak, ref int osztoKartyaErtek, ref int jatekosKartyaErtek, string[] jatekosKartyai, string[] osztoKartyai, ref int i)
        {
            for (; i < 2; i++)
            {
                //Kioszt 2db kártyát a játékosnak
                jatekosKartyai[i] = paklik[kartyak];
                Console.WriteLine($"A {paklik[kartyak]} kártyát kaptad");
                if (jatekosKartyaErtek + convertToInt(paklik, kartyak) > 21)
                {
                    jatekosKartyai[0] = "A - One";
                    jatekosKartyaErtek -= 10;
                }
                jatekosKartyaErtek += convertToInt(paklik, kartyak);
                kartyak--;
                //Kioszt 2db kártyát az osztónak
                osztoKartyai[i] = paklik[kartyak];
                if (osztoKartyaErtek + convertToInt(paklik, kartyak) > 21)
                {
                    osztoKartyai[1] = "A - One";
                    osztoKartyaErtek -= 10;
                }
                osztoKartyaErtek += convertToInt(paklik, kartyak);
                kartyak--;
            }
        }

        private static void SikeresenBiztositott(ref double tet, ref bool biztositas, ref bool vesztett, ref bool nyert, ref bool isPlaying, ref double biztositottZseton, int osztoKartyaErtek, int jatekosKartyaErtek)
        {
            Console.WriteLine($"Sikeres biztosítás, értéke : {biztositottZseton}\n\nNézzük az osztó kártyáit");
            if (osztoKartyaErtek == 21)
            {
                Console.WriteLine("Az osztónak BlackJack-je van!");
                isPlaying = false;
                if (jatekosKartyaErtek != 21)
                {
                    vesztett = true;
                    biztositas = false;
                    tet = tet - (biztositottZseton * 2);
                    biztositottZseton = 0;
                    if (tet == 0)
                    {
                        nyert = true;
                    }
                }
                else
                {
                    nyert = true;
                    tet = biztositottZseton;
                    biztositottZseton = 0;
                }
            }
            else
            {
                Console.WriteLine($"Az osztónak nincs BlackJack-je, a játék folytatódik tovább..." +
                    $"\nVesztettél {biztositottZseton} (Biztosított zsetonod)");
                biztositas = false;
            }
        }


    }

}