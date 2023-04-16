using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _r5jxrm_;
using System.Xml.Serialization;

namespace _r5jxrm_
{
    public class Deser
    {
        //XML-ből Deserialize Object-be /talán igy helyes a megfogalmazás
        internal Osszeg DeserializeTheObject()
        {
            //Visszatér a deserializált objektummal
            Osszeg objectToDeserialize = new Osszeg();
            XmlSerializer xmlserializer = new XmlSerializer(objectToDeserialize.GetType());
            using (StreamReader reader = new StreamReader("MentesOsszeg.xml"))
            {
                return (Osszeg)xmlserializer.Deserialize(reader);
            }
        }

        //Megvizsgálja az eddigi legnagyobb nyereményt és az aktuális nyereményt
        //Visszadja a két érték közül a nagyobbat
        public double max(double aktualis)
        {
            Osszeg osszeg = DeserializeTheObject();
            double eddigimax = osszeg.legnagyobbOsszeg;
            double maximum = Math.Max(aktualis, eddigimax);
            return maximum;
        }

        //Összeadja az eddigi összes nyereményt és az aktuális nyereményt
        //Kell egy plusz lista, mert arra hívom meg a .Sum() függvényt
        //Visszatér a kettő összegével
        public double osszeadas(double aktualis)
        {
            Osszeg osszeg = DeserializeTheObject();
            double eddigiek = osszeg.osszesNyeremeny;
            List<double> szumos = new List<double> { osszeg.osszesNyeremeny, aktualis};
            double szum = szumos.Sum();
            return szum;
        }
    }
}
