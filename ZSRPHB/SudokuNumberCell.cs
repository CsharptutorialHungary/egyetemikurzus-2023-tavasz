using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public record class SudokuNumberCell
{
    public int x_coordinate_on_table { get; set; }
    public int y_coordinate_on_table { get; set; }
    public bool is_solved { get; set; }

    public int stored_number_value { get; set; }

    public override string ToString() => is_solved ? stored_number_value.ToString() : $"*";
}


