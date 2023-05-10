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
