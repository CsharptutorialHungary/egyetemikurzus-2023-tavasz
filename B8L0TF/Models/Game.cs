using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B8L0TF.Models
{
    internal record Game(string Id, string Username, int Result)
    {
        public override string ToString()
        {
            return "Jatekos nev: " + $"{Username}" + " Eredmenye: " + $"{Result}";
        }
    }

}
