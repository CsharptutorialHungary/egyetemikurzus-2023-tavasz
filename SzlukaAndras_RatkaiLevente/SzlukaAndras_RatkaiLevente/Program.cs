// Alkalmazasfejlesztes C# alapokon a modern fejlesztesi iranyelvek bemutatasaval kurzus Kotelezo program
// 2023. majus
using System;
using System.Data.SqlClient;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace Nevter
{
    // Szerializaciohoz teszt class
    public class Emberke
    {
        [XmlAttribute("emberke")]
        public string emberke { get; set; }

        [XmlElement("Hiba")]
        public string Nev { get; set; }
    }

    // Generikushoz teszt class
    class Generikus<T> where T : struct
    {
        private T valtozo;

        // A publikus konstruktorok lehetnek generikusak? ==> Nem. ==> A konstruktor private.
        private Generikus() { }

        public static Generikus<T> Letrehoz(T parameter)
        {
            Generikus<T> vissza = new Generikus<T>();
            vissza.valtozo = parameter;
            return vissza;
        }

        public override string ToString()
        {
            return string.Format("valtozo tarolt tipusa: {0}, Erteke: {1}",
                                  valtozo.GetType().Name, valtozo);
        }
    }

    // Linq-hoz teszt class
    public class Felhasznalo
    {
        public string FelhNev { get; set; }
        public int Penz { get; set; }
    }

    // Linq-hoz teszt class meg mindig
    public static class UserExtensions
    {
        public static bool IsOkos(this Felhasznalo user)
        {
            if (user.Penz > 50)
                return true;
            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            // Szerializacio KEZDETE
            #region szerializacio
            try
            {
                using (var file = File.CreateText("valami.txt"))
                {
                    file.WriteLine("Hello File kezeles!");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            List<Emberke> list = new List<Emberke>()
            {
                new Emberke
                {
                    emberke = "Ezazember",
                    Nev = "Tesztnev"
                },
                new Emberke
                {
                    emberke = "Azazember",
                    Nev = "Masiktesztnev"
                },
                new Emberke
                {
                    emberke = "Amazazember",
                    Nev = "Harmadiktesztnev"
                },
            };

            try
            {
                string jsonEncoded = JsonSerializer.Serialize(list, new JsonSerializerOptions
                {
                    WriteIndented = true,
                });
                File.WriteAllText("jsonExample.json", jsonEncoded);

                string vissza = File.ReadAllText("jsonExample.json");
                List<Emberke> pVissza = JsonSerializer.Deserialize<List<Emberke>>(vissza);
            }
            catch (Exception ex)
                when (ex is IOException || ex is JsonException)
            {
                Console.WriteLine(ex.ToString());
            }

            XmlSerializer xs = new XmlSerializer(typeof(List<Emberke>));
            using (var f = File.Create("test.xml"))
            {
                xs.Serialize(f, list);
            }

            using (var f = File.OpenRead("test.xml"))
            {
                List<Emberke> vissza = xs.Deserialize(f) as List<Emberke>;
            }
            #endregion
            // Szerializacio VEGE

            // Linq KEZDETE
            #region Linq
            // Nem kell, hogy a tipusok megegyezzenek a szerializacios resszel,
            // de akar meg is egyezhetnek, tehat Emberke is lehetett volna itt a List-ben
            // tarolt adatok tipusa
            List<Felhasznalo> Users = new List<Felhasznalo>
            {
            new Felhasznalo
            {
                FelhNev = "Tobb_Felhasznalo_Nev_Nem_Jut_Eszembe",
                Penz = 120,
            },
            new Felhasznalo
            {
                FelhNev = "Jozsef",
                Penz = 30,
            },
            new Felhasznalo
            {
                FelhNev = "Levente",
                Penz = 69,
            },
            };

            var bandi = new Felhasznalo
            {
                FelhNev = "Bandi",
                Penz = 51,
            };

            bandi.IsOkos();

            //LINQ - Language integrated Query
            //lambda syntax
            var max = Users.Max(user => user.Penz);

            var Okosak = from user in Users
                         where user.IsOkos()
                         orderby user.Penz descending
                         select user.FelhNev;

            foreach (var user in Okosak.Take(2))
            {
                Console.WriteLine(user);
            }
            #endregion
            // Linq VEGE

            // Generikus KEZDETE
            #region generikus
            var teszt1 = Generikus<int>.Letrehoz(22);
            var teszt2 = Generikus<double>.Letrehoz(33.2);
            var teszt3 = Generikus<char>.Letrehoz('A');
            //az alabbi hibat fog dobni, mert a string osztaly!
            //Generikus<string> teszt4 = Generikus.Letrehoz("Teszt");
            #endregion
            // Generikus VEGE

            // Aszinkron resz KEZDETE
            #region aszinkron

            // A task magja egy egy async Action delegate.
            Task.Run(async () =>
            {
                Console.WriteLine("Async elott");
                await PrintCurrentTimeAsync();
                Console.WriteLine("Async utan");
            });
            #endregion
            // Aszinkron resz VEGE

            // ==============================================================

            Console.WriteLine("Getting Connection ...");
            // connection string 
            string connString = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;";

            // adatbazis kapcsolathoz peldany letrehozasa
            SqlConnection conn = new SqlConnection(connString);

            try
            {
                Console.WriteLine("Openning Connection ...");

                // kapcsolat nyitasa
                conn.Open();

                Console.WriteLine("Connection successful!");

                //create a new SQL Query using StringBuilder
                StringBuilder strBuilder = new StringBuilder();
                //strBuilder.Append("CREATE TABLE Demotabla ( Name VARCHAR(255), Email VARCHAR(255), Class VARCHAR(255)) ");
                //strBuilder.Append("INSERT INTO Demotabla (Name, Email, Class) VALUES ");
                //strBuilder.Append("(N'Harsh', N'harsh@gmail.com', N'Class X'), ");
                //strBuilder.Append("(N'Ronak', N'ronak@gmail.com', N'Class X') ");

                string sqlQuery = strBuilder.ToString();
                //using (SqlCommand command = new SqlCommand(sqlQuery, conn)) //pass SQL query created above and connection
                //{
                //    command.ExecuteNonQuery(); //execute the Query
                //    Console.WriteLine("Query Executed.");
                //}

                //strBuilder.Clear(); // strBuilder osszes appendalando stringtol "megtisztitasa", torlese

                // update-elo lekerdezes
                strBuilder.Append("UPDATE Demotabla SET Email = N'suri@gmail.com' WHERE Name = 'Surendra'");
                sqlQuery = strBuilder.ToString();
                using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                {
                    int rowsAffected = command.ExecuteNonQuery(); // lekerdezes vegrehajtasa es update-elt sorok szamanak kiiratasa
                    Console.WriteLine(rowsAffected + " row(s) updated");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            Console.Read();
        }
    }
}
