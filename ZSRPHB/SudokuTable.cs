using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;


    internal static class SudokuTable
    {
        public const int Table_size = 9;

        private static readonly int _box_size = (int)Math.Sqrt(Table_size);

        private static readonly List<SudokuNumberCell> _sudoku_cells_to_solve = new();

        public static string[] _game_table = new[] { "Unable to solve the given table" };

        private static readonly SudokuNumberCell[,] _sudoku_cells = new SudokuNumberCell[Table_size, Table_size];

        public static void Input()
        {
            _game_table = new string[Table_size];

            for (int i = 0; i < Table_size; i++)
            {
                _game_table[i] = Console.ReadLine() ?? "";
            }
        }

        public static string[] Solve()
        {
            InitializeSudokuCells();

            return SolveNext(0) ? SolutionToText() : new[] { "Unable to solve the given table" };
        }

        private static bool SolveNext(int cell_index)
        {
            if (cell_index == _sudoku_cells_to_solve.Count)
            {
                return true;
            }

            var cell = _sudoku_cells_to_solve[cell_index];
            var markers = GetAllMarkersOfSudokuNumberCell(cell);

            cell.is_solved = true;

            foreach (var marker in markers)
            {
                cell.stored_number_value = marker;

                if (SolveNext(cell_index + 1))
                {
                    return true;
                }
            }

            cell.is_solved = false;
            return false;
        }

        private static List<int> GetAllMarkersOfSudokuNumberCell(SudokuNumberCell cell)
        {
            var row = GetCellRow(cell.x_coordinate_on_table);
            var col = GetCellColumn(cell.y_coordinate_on_table);
            var cell_box = GetSudokuCellBox(cell.x_coordinate_on_table, cell.y_coordinate_on_table);

            var solvedNumbers = row.Concat(col).Concat(cell_box)
                .Where(x => x.is_solved)
                .Select(x => x.stored_number_value)
                .ToHashSet();

            var markers = new List<int>();

            for (int possibleNumber = 1; possibleNumber <= Table_size; possibleNumber++)
            {
                if (!solvedNumbers.Contains(possibleNumber))
                {
                    markers.Add(possibleNumber);
                }
            }

            return markers;
        }

        private static IEnumerable<SudokuNumberCell> GetCellRow(int cell_index)
        {
            for (int i = 0; i < Table_size; i++)
            {
                yield return _sudoku_cells[cell_index, i];
            }
        }

        private static IEnumerable<SudokuNumberCell> GetCellColumn(int cell_index)
        {
            for (int i = 0; i < Table_size; i++)
            {
                yield return _sudoku_cells[i, cell_index];
            }
        }

        private static IEnumerable<SudokuNumberCell> GetSudokuCellBox(int x_coordinate, int y_coordinate)
        {
            var x_coordinate_box = x_coordinate / _box_size;
            var y_coordinate_box = y_coordinate / _box_size;

            for (int i = x_coordinate_box * _box_size; i < (x_coordinate_box + 1) * _box_size; i++)
            {
                for (int j = y_coordinate_box * _box_size; j < (y_coordinate_box + 1) * _box_size; j++)
                {
                    yield return _sudoku_cells[i, j];
                }
            }
        }

        private static void InitializeSudokuCells()
        {
            _sudoku_cells_to_solve.Clear();

            for (int i = 0; i < Table_size; i++)
            {
                for (int j = 0; j < Table_size; j++)
                {
                    _sudoku_cells[i, j] = new SudokuNumberCell
                    {
                        x_coordinate_on_table = i,
                        y_coordinate_on_table = j,
                        is_solved = _game_table[i][j] != '*',
                        stored_number_value = _game_table[i][j] - '0',
                    };

                    if (!(_sudoku_cells[i, j].is_solved))
                    {
                        _sudoku_cells_to_solve.Add(_sudoku_cells[i, j]);
                    }
                }
            }
        }

        private static string[] SolutionToText()
        {
            var textSolution = new string[13];
            var display = new char[13][];

            display[0] = "-------------".ToCharArray();
            display[1] = "|   |   |   |".ToCharArray();
            display[2] = "|   |   |   |".ToCharArray();
            display[3] = "|   |   |   |".ToCharArray();
            display[4] = "|---|---|---|".ToCharArray();
            display[5] = "|   |   |   |".ToCharArray();
            display[6] = "|   |   |   |".ToCharArray();
            display[7] = "|   |   |   |".ToCharArray();
            display[8] = "|---|---|---|".ToCharArray();
            display[9] = "|   |   |   |".ToCharArray();
            display[10] = "|   |   |   |".ToCharArray();
            display[11] = "|   |   |   |".ToCharArray();
            display[12] = "-------------".ToCharArray();

            var positions_x = (new[] { 1, 2, 3, 5, 6, 7, 9, 10, 11 } as IEnumerable<int>).GetEnumerator();
            var positions_y = (new[] { 1, 2, 3, 5, 6, 7, 9, 10, 11 } as IEnumerable<int>).GetEnumerator();

            for (int i = 0; positions_x.MoveNext(); i++)
            {
                foreach (var number in GetCellRow(i).Select(x => x.stored_number_value))
                {
                    positions_y.MoveNext();
                    display[positions_x.Current][positions_y.Current] = (char)('0' + number);
                }

                positions_y.Reset();
            }

            for (int i = 0; i < 13; i++)
            {
                textSolution[i] = new string(display[i]);
            }

            return textSolution;
        }

        public static void ReadSudokuTableFromFile()
        {
            _game_table = new string[Table_size];

            Console.WriteLine("Melyik fájlból szeretnél beolvasni? (az alapértelmezett fájl a sudoku.txt)");
            Console.WriteLine();

            try
            {
                string name = Console.ReadLine() ?? "sudoku.txt";

                string path = Path.Combine(AppContext.BaseDirectory, name);

                string[] readedSudoku = File.ReadAllLines(path);

                _game_table = readedSudoku;
            }

            catch (IOException ex)
            {
                Console.WriteLine("Hiba történt a fájl beolvasása során!");
            }

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

        }

        public static void WriteSudokuTableToFile()
        {
            Console.WriteLine("A fájl neve:");
            Console.WriteLine();

            string name = Console.ReadLine() ?? "sudoku.txt";

            string path = Path.Combine(AppContext.BaseDirectory, name);

            using (FileStream file = File.OpenWrite(path))
            {
                using (StreamWriter writer = new StreamWriter(file))
                {
                    try
                    {
                        for (int i = 0; i < Table_size; i++)
                        {
                            writer.WriteLine(_game_table[i]);
                        }
                    }

                    catch (IOException ex)
                    {
                        Console.WriteLine("Hiba történt a fájl írása során!");
                    }
                }
            }
        }

        public static void ReadChoice()
        {
            Console.WriteLine("Fájlból szeretnéd beolvasni a táblát? (igen)");
            string read_choice = Console.ReadLine() ?? "";
            Console.WriteLine();

            if (read_choice.Equals("igen"))
            {
                SudokuTable.ReadSudokuTableFromFile();
            }

            else
            {
                Console.WriteLine("Add meg a bemeneti sudoku táblát!");
                SudokuTable.Input();
            }
        }

        public static void WriteChoice()
        {
            Console.WriteLine("Szeretnéd elmenteni a megoldandó táblát? (igen)");
            string write_choice = Console.ReadLine() ?? "";
            Console.WriteLine();

            if (write_choice.Equals("igen"))
            {
                SudokuTable.WriteSudokuTableToFile();
            }
        }

        public static void ReadAndWriteSudokuCellsJSON()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            var cells = new string[Table_size, Table_size];

            Console.WriteLine("A megadott sudoku mezők jellemzői:");
            Console.WriteLine();

            for (int i = 0; i < Table_size; i++)
            {
                for (int j = 0; j < Table_size; j++)
                {
                    var json_cell = JsonSerializer.Serialize(_sudoku_cells[i, j], options);
                    cells[i, j] = json_cell;
                    Console.WriteLine(json_cell);
                }
            }

            string name = "sudoku_JSON.txt";

            string path = Path.Combine(AppContext.BaseDirectory, name);

            using (FileStream file = File.OpenWrite(path))
            {
                using (StreamWriter writer = new StreamWriter(file))
                {
                    try
                    {
                        for (int i = 0; i < Table_size; i++)
                        {
                            for (int j = 0; j < Table_size; j++)
                            {
                                int index = i + j;
                                writer.WriteLine(cells[i, j]);
                            }
                        }
                    }

                    catch (IOException ex)
                    {
                        Console.WriteLine("Hiba történt a fájl írása során!");
                    }
                }
            }

            SudokuNumberCell[,] readed_cells = new SudokuNumberCell[Table_size, Table_size];

            for (int i = 0; i < Table_size; i++)
            {
                for (int j = 0; j < Table_size; j++)
                {
                    SudokuNumberCell json_cell_readed = JsonSerializer.Deserialize<SudokuNumberCell>(cells[i, j], options);
                    readed_cells[i, j] = json_cell_readed;
                }
            }
        }
    }
