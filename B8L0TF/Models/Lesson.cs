using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B8L0TF.Models
{
    public class Lesson
    {
        public string text;
        public int difficulty;
        public int result;

        public Lesson(int difficulty)
        {
            this.difficulty = difficulty;
            text = string.Empty;
        }
    }
}
