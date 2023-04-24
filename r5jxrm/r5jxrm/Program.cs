using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using _r5jxrm_;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;

namespace _r5jxrm_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Pakli deklarálása
            string[] pakli = new[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

            //Fontos változók deklarálása
            double zseton = 500;
            double tet = 0;

            bool isPlaying = false;
            bool quit = false;


            //Háttérszín beállítása
            var szines = new MyConsoleColor();
            Console.WriteLine("Milyen háttérszínt szeretnél? Add meg a nevét!");
            List<ConsoleColor> consoleColors = new List<ConsoleColor>();
            szines.kiiratas(consoleColors);
            /*szines.hozzaadas(consoleColors);
            foreach (var color in consoleColors)
            {
                Console.WriteLine($"A választható színek: {color}");
            }*/
            var beszin = Console.ReadLine();
            beszin = beszin.ToLower();
            szinek(beszin, consoleColors);

            Console.Clear();

            //Bevezető szöveg
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string cim = "BlackJack 2023 - Kolbert Márió\u00a9";
            Console.SetCursorPosition((Console.WindowWidth - cim.Length) / 2, (Console.CursorTop));
            Console.WriteLine(cim + "\n\n");

            Console.WriteLine("Mennyi zsetonod van?");
            try
            {
                zseton = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException e)
            {
                Console.WriteLine("Helytelen amit megadtál...");
                Console.WriteLine("Az alapértelmezett 500 zsetonnal kezdesz!");
            }
            
            double kezdoZseton = zseton;
            Console.WriteLine();



            //A játék while ciklusa
            while (!quit)
            {
                if (isPlaying)
                {
                    Console.WriteLine($"Zsetonjaid száma:  {zseton}\n");
                    tet = osztoKezdo(zseton, kezdoZseton);
                    zseton += jatek(pakli, tet, zseton);
                    //Várakozás a felhasználóra
                    tovabbLepesSzoveg();
                    // ------------- Console.Clear();
                    if (zseton > 0)
                    {
                        isPlaying = false;
                    }
                    else
                    {
                        quit = true;
                        Deser optional = new Deser();
                        double maximum = optional.max(zseton);
                        var mentes = new Osszeg();
                        var serializer = new XmlSerializer(typeof(Osszeg));
                        if (zseton >= maximum)
                        {
                            mentes.legnagyobbOsszeg = zseton;
                        }
                        else
                        {
                            mentes.legnagyobbOsszeg = maximum;
                        }
                        mentes.nyeremeny = zseton - kezdoZseton;
                        mentes.osszesNyeremeny = optional.osszeadas(zseton-kezdoZseton);
                        using (var writer = new StreamWriter("MentesOsszeg.xml"))
                        {
                            serializer.Serialize(writer, mentes);
                        }
                        Console.WriteLine();
                        Console.WriteLine("A jaték mentése elkészült...");
                        Console.WriteLine($"Nyereményed: {mentes.nyeremeny}");
                        Console.WriteLine($"Legnagyobb összeged eddig: {mentes.legnagyobbOsszeg}");
                        Console.WriteLine($"Eddigi összes nyereményed: {mentes.osszesNyeremeny}");
                        Console.WriteLine();
                        Console.WriteLine("Kifogytál a zsetonokból!\nKöszönjük a játékot!");
                        //Várakozás a felhasználóra
                        tovabbLepesSzovegClear();
                    }
                }
                else
                {
                    quit = valasztas(zseton);
                    if (quit)
                    {
                        //Zsetonok mentése, statisztika írása
                        Deser optional = new Deser();
                        double maximum = optional.max(zseton);
                        var mentes = new Osszeg();
                        var serializer = new XmlSerializer(typeof(Osszeg));
                        if (zseton>=maximum)
                        {
                            mentes.legnagyobbOsszeg = zseton;
                        }
                        else
                        {
                            mentes.legnagyobbOsszeg = maximum;
                        }
                        mentes.nyeremeny = zseton-kezdoZseton;
                        mentes.osszesNyeremeny = optional.osszeadas(zseton-kezdoZseton);
                        using (var writer = new StreamWriter("MentesOsszeg.xml"))
                        {
                            serializer.Serialize(writer, mentes);
                        }


                        Console.WriteLine("A jaték mentése elkészült...");
                        Console.WriteLine($"Nyereményed: {mentes.nyeremeny}");
                        Console.WriteLine($"Legnagyobb összeged eddig: {mentes.legnagyobbOsszeg}");
                        Console.WriteLine($"Eddigi összes nyereményed: {mentes.osszesNyeremeny}");
                        Console.WriteLine();
                        Console.WriteLine("Köszönjük a játékot!");
                        //Várakozás a felhasználóra
                        tovabbLepesSzovegClear();
                    }
                    isPlaying = true;
                }
            }
        }


        //Összegzés
        //Menü megjelenítése, választás
        //Visszatérése: Bool-lal tér vissza, hogy a játékból kiléptünk-e vagy sem
        static private bool valasztas(double penz)
        {
            //Kilépő változó deklarálása
            bool quit = false;
            //While ciklus változójának deklarálása
            bool NoOption = true;
            //While ciklus
            while (NoOption)
            {
                //Zsetonjaid kiírása
                Console.WriteLine($"Zsetonjaid száma:  {penz}\n");
                //Választás kiírása
                Console.WriteLine("Válassz:\n1.Tét\n2.Kilépés");
                //Választási változó deklarálása
                string valasztott = Console.ReadLine();
                //Választási lehetőségek
                switch (valasztott)
                {
                    case "1":
                        Console.Clear();
                        NoOption = false;
                        break;
                    case "2":
                        Console.Clear();
                        quit = true;
                        NoOption = false;
                        break;
                    default:
                        Console.WriteLine("Rossz lehetőséget választottál");
                        tovabbLepesSzovegClear();
                        break;
                }
            }
            return quit;
        }


        //Összegzés
        //Köszöntjük a játékost, megnézzük mennyit szeretne fogadni, megvizsgáljuk van-e elég zsetonja,
        //majd visszaadja a feltett zsetont a játékosnak
        static private double osztoKezdo(double ennyivan, double kezdoZseton)
        {
            //Deklarálunk egy változót, ami csekkolja van-e elég zsetonja a játékosnak
            bool vaneleg = false;
            //Deklarálunk egy tét változót is
            double tet = -1;
            Console.WriteLine($"Köszöntelek a kaszinómban, {ennyivan} zsetonod van.\nMennyit szeretnél feltenni?");
            //Bekérjük a tétet, és csekkoljuk, hogy jó-e a bemenet
            try
            {
                tet = Convert.ToDouble(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Error");
            }
            //Ameddig van elég pénze
            while (!vaneleg)
            {
                //Levonjuk a tétet
                ennyivan = ennyivan - tet;
                //Megnezzük rakott-e fel tétet (egészre kerekítve)
                if ((int)tet == 0)
                {
                    //Elköszönő szöveg
                    Console.WriteLine("Nem tettél fel tétet, tehát kiszállsz...\n");
                    //Zsetonok mentése, statisztika írása
                    Deser optional = new Deser();
                    double maximum = optional.max(ennyivan);
                    var mentes = new Osszeg();
                    var serializer = new XmlSerializer(typeof(Osszeg));
                    if (ennyivan >= maximum)
                    {
                        mentes.legnagyobbOsszeg = ennyivan;
                    }
                    else
                    {
                        mentes.legnagyobbOsszeg = maximum;
                    }
                    mentes.nyeremeny = ennyivan - kezdoZseton;
                    mentes.osszesNyeremeny = optional.osszeadas(ennyivan - kezdoZseton);
                    using (var writer = new StreamWriter("MentesOsszeg.xml"))
                    {
                        serializer.Serialize(writer, mentes);
                    }


                    Console.WriteLine("A jaték mentése elkészült...");
                    Console.WriteLine($"Nyereményed: {mentes.nyeremeny}");
                    Console.WriteLine($"Legnagyobb összeged eddig: {mentes.legnagyobbOsszeg}");
                    Console.WriteLine($"Eddigi összes nyereményed: {mentes.osszesNyeremeny}");
                    Console.WriteLine();
                    Console.WriteLine("Köszönjük a játékot!");
                    tovabbLepesSzovegClear();
                    Environment.Exit(0);
                }
                else if (tet < 0)
                {
                    //Visszaállítja a régi értéket
                    ennyivan = ennyivan + tet;
                    Console.WriteLine($"Hibás érték, kérlek adj meg újat!\nEnnyi zsetonod van: {ennyivan}");
                    //Ismét beolvassuk az értéket.
                    try
                    {
                        tet = Convert.ToDouble(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Error");
                    }
                    Console.Clear();
                }
                //Ha a tétrakás után több, mint 0, vagy pontosan 0, akkor elfogadjuk a tétrakást
                else if (ennyivan >= 0)
                {
                    Console.WriteLine($"A megtett tét {tet} zseton. Még {ennyivan} zsetonod maradt.");
                    vaneleg = true;
                    tovabbLepesSzovegClear();
                }
                //Egyéb esetben, ha a maradék zseton kevesebb, mint 0
                else
                {
                    //Visszaállítjuk az eredeti értéket
                    ennyivan = ennyivan + tet;
                    //Tájékoztatjuk, hogy nincs elég zsetonja
                    Console.WriteLine($"Nincs elég zsetonod, kérjük adj meg más tétet. Ennyi zsetonod van: {ennyivan}");
                    //Új tét kérése
                    try
                    {
                        tet = Convert.ToDouble(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Error");
                    }
                    Console.Clear();
                }
            }
            return tet;
        }


        //
        //A játékunk játszása
        //Paraméterben várja a paklit, a tétet, illetve a jelenlegi zsetonunk értékét
        //Visszatér a nyert vagy vesztett zseton mennyiségével

        static private double jatek(string[] paklik, double tet, double ennyivan)
        {
            //Deklarálunk fontos változókat
            bool errorSave = false;
            bool biztositas = false;
            bool elsoKor = true;
            bool splitLost = false;
            bool vesztett = false;
            bool nyert = false;
            bool isPlaying = true;
            bool jatekosKor = true;
            bool splitAvailable = false;
            bool splitting = false;
            bool hasSplit = false;

            string valasztas;

            double biztositottZseton = 0;
            int kartyak = 51;
            int osztoKartyaErtek = 0;
            int jatekosKartyaErtek = 0;
            int jatekosSzetvalasztottKartyaErtek = 0;
            string[] jatekosKartyai = new string[100];
            string[] jatekosSzetvalasztottKartyak = new string[100];
            string[] osztoKartyai = new string[100];

            Console.Clear();
            Console.WriteLine("Kezdődik a játék");
            Console.WriteLine("Kártyák megkeverése és kiosztása.");
            paklik = Keveres(paklik);
            tovabbLepesSzovegClear();
            int i = 0;
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

            //Osztó megmutatja a kártyáját
            Console.WriteLine($"Az osztó kártyái {osztoKartyai[0]} és *");
            //Ha az osztó kártyája ÁSZ, akkor biztosítsunk
            if (osztoKartyai[0] == "A")
            {
                biztositas = true;
            }
            //Engedélyezi a játékosnak, hogy biztosítási oldali fogadást tegyen, ha elérhető
            while (biztositas)
            {
                //Kérdés, hogy akarja-e vagy sem
                Console.WriteLine("Szeretnél biztosítani?\n1 - Igen\n2 - Nem");
                valasztas = Console.ReadLine();

                switch (valasztas)
                {
                    case "1":
                        while (biztositas)
                        {
                            Console.Clear();
                            Console.WriteLine($"Jelenleg {ennyivan - tet} zsetonod van, és eddig {tet} zsetont tettél fel." +
                                $"\nMennyit szeretnél biztosítani? Figyelem, maximum a téted felét tudod biztosítani!" +
                                $"\nA kilépéshez: 0");
                            try
                            {
                                biztositottZseton = Convert.ToDouble(Console.ReadLine());
                            }
                            catch
                            {
                                Console.WriteLine("\nError");
                                biztositottZseton = -1;
                            }
                            if (biztositottZseton > 0 && ennyivan - (biztositottZseton + tet) >= 0 && biztositottZseton <= tet / 2)
                            {
                                Console.WriteLine($"Sikeres biztosítás, értéke : {biztositottZseton}\n\nNézzük az osztó kártyáit");
                                tovabbLepesSzoveg();
                                if (osztoKartyaErtek == 21)
                                {
                                    Console.WriteLine("Az osztónak BlackJack-je van!");
                                    tovabbLepesSzovegClear();
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
                                    tovabbLepesSzovegClear();
                                    biztositas = false;
                                }

                            }
                            else if ((int)biztositottZseton == 0)
                            {
                                Console.WriteLine("Nem biztosítottál semmit.");
                                tovabbLepesSzovegClear();
                                biztositas = false;
                            }
                            else
                            {
                                Console.WriteLine("Hibás érték, nézd meg mégegyszer...");
                                tovabbLepesSzoveg();
                            }
                        }
                        break;
                    case "2":
                        Console.WriteLine("Nem biztosítottál semmit, a játék folytatódik");
                        tovabbLepesSzovegClear();
                        biztositas = false;
                        break;
                    default:
                        Console.WriteLine("Nem választottál semmit.");
                        tovabbLepesSzovegClear();
                        break;
                }
            }
            //Ha a játékosnak 21-e van, jelezzük a játékosnak és ugorjunk a játékban
            //Csekkoljuk, hogy döntetlen van-e
            if (jatekosKartyaErtek == 21)
            {
                Console.WriteLine("21-ed van elsőkézben!");
                tovabbLepesSzoveg();
                isPlaying = false;
                tet = tet * 1.5;
            }
            //Ha az osztónak 21-e van, és a játékos nem biztosított
            if (osztoKartyaErtek == 21 && nyert == false && vesztett == false)
            {
                Console.WriteLine("Az osztónak elsőkézben 21-e van!");
                tovabbLepesSzoveg();
                isPlaying = false;
            }
            //Ha a játékosnak 2 ugyanolyan lapja van, lehetséges a szétválasztás
            if (jatekosKartyai[0] == jatekosKartyai[1] || jatekosKartyai[1] == "A - One" && jatekosKartyai[0] == "A")
            {
                splitAvailable = true;
            }
            while (isPlaying)
            {
                if (jatekosKor)
                {
                    //Mutassuk meg, mennyije van a játékosnak, ha szétválaszt megmutatjuk az aktuális paklit
                    if (!splitting)
                    {
                        Console.WriteLine($"A kártyáid értéke: {jatekosKartyaErtek}");
                    }
                    else
                    {
                        Console.WriteLine($"A kártyáid értéke másik kézben: {jatekosSzetvalasztottKartyaErtek}");
                    }
                    //Megkérdezzük, hogy megüti-e vagy marad
                    Console.WriteLine("Melyiket szeretnéd?\n1 - Megütöm\n2 - Maradok");
                    if (elsoKor && splitAvailable)
                    {
                        Console.WriteLine("3 - Megütöm + Dupla tét\n4 - Megadom\n5 - Szétválasztom");
                    }
                    else if (elsoKor)
                    {
                        Console.WriteLine("3 - Megütöm + Dupla tét\n4 - Megadom");
                    }
                    valasztas = Console.ReadLine();
                    if (splitting)
                    {
                        switch (valasztas)
                        {
                            //Ha megüti
                            case "1":
                                //Ha a következő lap egy ász 11-es értékű, és a játékos másik paklija 21 fölé megy a hozzáadással, legyen egy értéke
                                if (convertToInt(paklik, kartyak) == 11 && jatekosSzetvalasztottKartyaErtek + convertToInt(paklik, kartyak) > 21)
                                {
                                    //Mozgatjuk a játékos tovább
                                    i++;
                                    //Player's other deck add ace with value of one
                                    jatekosSzetvalasztottKartyak[i] = "A - One";
                                    jatekosSzetvalasztottKartyaErtek += 1;
                                    Console.WriteLine($"Megütötted! {paklik[kartyak]} lapot kaptad, jelenleg {jatekosSzetvalasztottKartyaErtek} van");
                                    //Pakli aljára mozgatás
                                    kartyak--;
                                }
                                else
                                {
                                    i++;
                                    jatekosSzetvalasztottKartyak[i] = paklik[kartyak];
                                    jatekosSzetvalasztottKartyaErtek += convertToInt(paklik, kartyak);
                                    Console.WriteLine($"Megütötted! {paklik[kartyak]} lapot kaptad, jelenleg {jatekosSzetvalasztottKartyaErtek} van");
                                    kartyak--;
                                }
                                //Ha a szétválasztásnál több, mint 21
                                if (jatekosSzetvalasztottKartyaErtek > 21)
                                {
                                    //Megnézzük, hogy a játékos paklijában van-e "ÁSZ"
                                    int index = checkHaVanAsz(jatekosSzetvalasztottKartyak);
                                    //Ha van "ÁSZ", aminek 11 az értéke, akkor változtassuk meg 1-re
                                    if (jatekosSzetvalasztottKartyak[index] == "A")
                                    {
                                        //Átállítjuk a nevét
                                        jatekosSzetvalasztottKartyak[index] = "A - One";
                                        //És az értéket csökkentjük 10-zel
                                        jatekosSzetvalasztottKartyaErtek -= 10;
                                        Console.WriteLine($"A játékosnak volt Ásza, az értékét 1-re csökkentettük, a játékos jelenleg {jatekosSzetvalasztottKartyaErtek} van");
                                    }
                                    else
                                    {
                                        //
                                        splitLost = true;
                                        splitting = false;
                                        i = 0;
                                    }
                                }
                                tovabbLepesSzovegClear();
                                break;
                            //Ha marad
                            case "2":
                                Console.WriteLine($"Maradtál {jatekosSzetvalasztottKartyaErtek} értékkel");
                                tovabbLepesSzovegClear();
                                splitting = false;
                                i = 0;
                                break;
                            //Ha nem választ semmit
                            case "":
                                Console.WriteLine("Kérlek válassz!");
                                if (elsoKor)
                                {
                                    errorSave = true;
                                }
                                tovabbLepesSzovegClear();
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (valasztas)
                        {
                            //Ha megüti a játékos
                            case "1":
                                if (convertToInt(paklik, kartyak) == 11 && jatekosKartyaErtek + convertToInt(paklik, kartyak) > 21)
                                {
                                    //Továbbmegyünk a játékos pakliján
                                    i++;
                                    //Adunk egy Ász 1-es értéket a játékos paklijának
                                    jatekosKartyai[i] = "A - One";
                                    //Növeljük az értéket 1-el
                                    jatekosKartyaErtek += 1;
                                    Console.WriteLine($"Megütötted! {paklik[kartyak]} lapot kaptad, jelenleg {jatekosKartyaErtek} van");
                                    //Csökkentjük a paklit
                                    kartyak--;
                                }
                                //Mást üt?
                                else
                                {
                                    //Továbbmegyünk a játékos pakliján
                                    i++;
                                    //Hozzáadjuk a kártyát
                                    jatekosKartyai[i] = paklik[kartyak];
                                    //Hozzáadjuk az értékét
                                    jatekosKartyaErtek += convertToInt(paklik, kartyak);
                                    Console.WriteLine($"Megütötted! {paklik[kartyak]} lapot kaptad, jelenleg {jatekosKartyaErtek} van");
                                    //Csökkentjük a paklit
                                    kartyak--;
                                }
                                //Ha a játékos átlépte a 21-et
                                if (jatekosKartyaErtek > 21)
                                {
                                    //Megnézzük van-e Ász 11-es értékkel
                                    int index = checkHaVanAsz(jatekosKartyai);
                                    if (jatekosKartyai[index] == "A")
                                    {
                                        jatekosKartyai[index] = "A - One";
                                        jatekosKartyaErtek -= 10;
                                        Console.WriteLine($"Volt 11-es Ász, az értéke megváltozott 1-re, jelenleg {jatekosKartyaErtek} van");
                                    }
                                    else
                                    {
                                        vesztett = true;
                                        if (!hasSplit || hasSplit && splitLost)
                                        {
                                            isPlaying = false;
                                        }
                                        else
                                        {
                                            jatekosKor = false;
                                        }
                                    }
                                }
                                tovabbLepesSzovegClear();
                                break;
                            case "2":
                                Console.WriteLine($"Maradtál, jelenleg {jatekosKartyaErtek}");
                                tovabbLepesSzoveg();
                                jatekosKor = false;
                                break;
                            case "":
                                Console.WriteLine("Kérlek adj meg egy választást!");
                                if (elsoKor)
                                {
                                    errorSave = true;
                                }
                                tovabbLepesSzovegClear();
                                break;
                            default:
                                break;
                        }
                    }
                    if (elsoKor)
                    {
                        switch (valasztas)
                        {
                            case "3":
                                //Csekkoljuk, hogy van-e elég zsetonja duplázni
                                if (ennyivan - tet * 2 >= 0)
                                {
                                    if (convertToInt(paklik, kartyak) == 11 && jatekosKartyaErtek + convertToInt(paklik, kartyak) > 21)
                                    {
                                        i++;
                                        jatekosKartyai[i] = "A - One";
                                        jatekosKartyaErtek += 1;
                                        Console.WriteLine($"Megütötted! {paklik[kartyak]} lapot kaptad, jelenleg {jatekosKartyaErtek} van");
                                        kartyak--;
                                    }
                                    else
                                    {
                                        i++;
                                        jatekosKartyai[i] = paklik[kartyak];
                                        jatekosKartyaErtek += convertToInt(paklik, kartyak);
                                        Console.WriteLine($"Megütötted! {paklik[kartyak]} lapot kaptad, jelenleg {jatekosKartyaErtek} van");
                                        kartyak--;
                                    }
                                    if (jatekosKartyaErtek > 21)
                                    {
                                        int index = checkHaVanAsz(jatekosKartyai);
                                        if (jatekosKartyai[index] == "A")
                                        {
                                            jatekosKartyai[index] = "A - One";
                                            jatekosKartyaErtek -= 10;
                                            Console.WriteLine($"Volt 11-es Ász, az értéke megváltozott 1-re, jelenleg {jatekosKartyaErtek} van");
                                        }
                                        else
                                        {
                                            vesztett = true;
                                            isPlaying = false;
                                        }
                                    }
                                    tovabbLepesSzoveg();
                                    jatekosKor = false;
                                    tet = tet * 2;
                                }
                                else
                                {
                                    Console.WriteLine("Nincs elég zsetonod duplázni");
                                    errorSave = true;
                                    tovabbLepesSzovegClear();
                                }
                                break;
                            //Ha megadja
                            case "4":
                                tet = tet / 2;
                                Console.WriteLine("Megadtad! Csak a felét veszted el, a tétnek");
                                tovabbLepesSzovegClear();
                                vesztett = true;
                                isPlaying = false;
                                break;
                        }
                    }
                    //Ha a szétválasztás elérhető
                    if (splitAvailable)
                    {
                        switch (valasztas)
                        {
                            //Ha szétválaszt
                            case "5":
                                if (ennyivan - tet * 2 >= 0)
                                {
                                    splitting = true;
                                    hasSplit = true;
                                    if (jatekosKartyai[1] == "A - One" && jatekosKartyai[0] == "A")
                                    {
                                        jatekosKartyai[1] = "A";
                                        jatekosKartyaErtek += 10;
                                    }
                                    jatekosSzetvalasztottKartyak[0] = jatekosKartyai[1];
                                    jatekosKartyaErtek -= convertToInt(jatekosKartyai, 1);
                                    jatekosSzetvalasztottKartyaErtek += convertToInt(jatekosKartyai, 1);
                                    jatekosKartyai[1] = "";
                                    tet = tet * 2;
                                    i = 0;
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.WriteLine("Nincs elég zsetonod szétválasztani!");
                                    errorSave = true;
                                    tovabbLepesSzovegClear();
                                }
                                break;
                        }
                    }
                    if (splitAvailable || elsoKor)
                    {
                        if (!errorSave)
                        {
                            elsoKor = false;
                            splitAvailable = false;
                        }
                        else
                        {
                            errorSave = false;
                        }
                    }
                }
                else
                {
                    //1től számolunk az osztónál
                    i = 1;
                    //Ha az osztó 16 felett van, akkor marad
                    if (osztoKartyaErtek > 16)
                    {
                        Console.WriteLine($"Az osztó marad {osztoKartyaErtek} értékkel");
                        tovabbLepesSzoveg();
                        isPlaying = false;
                    }
                    else
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
                        tovabbLepesSzoveg();
                    }
                }
            }

            if (hasSplit)
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
            else
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

        static private void tovabbLepesSzovegClear()
        {
            Console.WriteLine("Nyomj gombot a folytatáshoz!");
            Console.ReadKey();
            Console.Clear();
        }

        static private void tovabbLepesSzoveg()
        {
            Console.WriteLine("Nyomj gombot a folytatáshoz!");
            Console.ReadKey();
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
                    Console.WriteLine("Hibás szín! Adj meg létező színt a listából!");
                    var beszin2 = Console.ReadLine();
                    beszin2 = beszin2.ToLower();
                    szinek(beszin2, szineklista);
                    break;
            }
        }
    }
}
