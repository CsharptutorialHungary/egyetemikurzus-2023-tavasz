using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _r5jxrm_;
using System.Xml.Serialization;

namespace _r5jxrm_
{
    public class Osszeg
    {
        //Az XML fájlom felépítése
        //Mindegyik kapott default értéket, hogy nehogy null-t adjon valamelyik helyen (pl amikor létrehozom az objektumot)
        public double nyeremeny { get; set; } = 0;
        public double legnagyobbOsszeg { get; set; } = 0;
        public double osszesNyeremeny { get; set; } = 0;
    } 
}
