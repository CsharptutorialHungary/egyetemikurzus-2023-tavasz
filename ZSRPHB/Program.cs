
Task task_for_info = Task.Run(() =>
{
    Console.WriteLine("Ezzel az alkalmazással  megadhatsz egy sudoku táblát reprezentáló stringet, amiben az üres pozíciókat a * karakter jelöli.");
    Console.WriteLine("A táblát soronként tagolva (9x9) kell megadni");
    Console.WriteLine();
});

Task task_for_sudoku = Task.Run(() =>
{
    SudokuTable.ReadChoice();
    Console.WriteLine();
    var solution = string.Join("\n", SudokuTable.Solve());

    Console.WriteLine();

    Console.WriteLine("Megoldás:");
    Console.WriteLine(solution);
    Console.WriteLine();
    SudokuTable.ReadAndWriteSudokuCellsJSON();
    Console.WriteLine();
    SudokuTable.WriteChoice();
});

task_for_sudoku.Wait();
