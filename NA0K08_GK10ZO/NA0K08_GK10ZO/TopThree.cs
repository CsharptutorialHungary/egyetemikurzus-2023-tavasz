using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NA0K08_GK10ZO
{
    internal class TopThree
    {
        readonly private string first;
        readonly private string second;
        readonly private string third;

        public TopThree(string first, string second, string third)
        {
            this.first = first;
            this.second = second;
            this.third = third;
        }

        public string getFirst() { return first; }

        public string getSecond() { return second; }

        public string getThird() { return third; }

    }
}
