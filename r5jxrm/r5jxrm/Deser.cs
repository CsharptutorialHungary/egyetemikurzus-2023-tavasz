using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace _r5jxrm_
{
    internal class Deser
    {
        internal Osszeg DeserializeTheObject()
        {
            Osszeg objectToDeserialize = new Osszeg();
            XmlSerializer xmlserializer = new XmlSerializer(objectToDeserialize.GetType());
            using (StreamReader reader = new StreamReader("MentesOsszeg.xml"))
            {
                return (Osszeg)xmlserializer.Deserialize(reader);
            }
        }

        public double max(double aktualis)
        {
            Osszeg osszeg = DeserializeTheObject();
            double eddigimax = osszeg.legnagyobbOsszeg;
            double maximum = Math.Max(aktualis, eddigimax);
            return maximum;
        }

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
