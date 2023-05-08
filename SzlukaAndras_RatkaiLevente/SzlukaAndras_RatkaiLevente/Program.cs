using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

record class TicTacToeGame
{
    public int Id { get; init; }
    public string Player1 { get; init; }
    public string Player2 { get; init; }
    public string[,] Board { get; set; } = new string[3, 3];
}

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            var game = await LoadGameAsync("game.json");
            Console.WriteLine($"Loaded game with Id {game.Id}");

            game.Player1 = "Alice";
            game.Player2 = "Bob";

            var currentPlayer = game.Player1;
            var winner = "";
            while (winner == "")
            {
                Console.Clear();
                Console.WriteLine($"{currentPlayer}'s turn");
                PrintBoard(game.Board);

                Console.Write("Enter row (0-2): ");
                var row = int.Parse(Console.ReadLine());

                Console.Write("Enter column (0-2): ");
                var col = int.Parse(Console.ReadLine());

                if (game.Board[row, col] != null)
                {
                    Console.WriteLine("Invalid move. This cell is already taken.");
                    Console.ReadKey();
                    continue;
                }

                game.Board[row, col] = currentPlayer;
                winner = GetWinner(game.Board);

                if (currentPlayer == game.Player1)
                {
                    currentPlayer = game.Player2;
                }
                else
                {
                    currentPlayer = game.Player1;
                }
            }

            Console.Clear();
            PrintBoard(game.Board);
            Console.WriteLine($"The winner is {winner}!");

            await SaveGameAsync("game.json", game);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static async Task<TicTacToeGame> LoadGameAsync(string filePath)
    {
        using var fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
        return await JsonSerializer.DeserializeAsync<TicTacToeGame>(fileStream);
    }

    static async Task SaveGameAsync(string filePath, TicTacToeGame game)
    {
        using var fileStream = new FileStream(filePath, FileMode.Create);
        await JsonSerializer.SerializeAsync(fileStream, game);
    }

    static void PrintBoard(string[,] board)
    {
        Console.WriteLine("   0  1  2");
        for (int i = 0; i < 3; i++)
        {
            Console.Write($"{i} ");
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == null)
                {
                    Console.Write("  ");
                }
                else
                {
                    Console.Write($" {board[i, j]}");
                }
            }
            Console.WriteLine();
        }
    }
    private char? GetWinner()
    {
        // Check rows
        for (int row = 0; row < BoardSize; row++)
        {
            if (board[row, 0] != ' ' && board[row, 0] == board[row, 1] && board[row, 1] == board[row, 2])
            {
                return board[row, 0];
            }
        }

        // Check columns
        for (int col = 0; col < BoardSize; col++)
        {
            if (board[0, col] != ' ' && board[0, col] == board[1, col] && board[1, col] == board[2, col])
            {
                return board[0, col];
            }
        }
        private char? GetWinner()
        {
            // Check rows
            for (int row = 0; row < BoardSize; row++)
            {
                if (board[row, 0] != ' ' && board[row, 0] == board[row, 1] && board[row, 1] == board[row, 2])
                {
                    return board[row, 0];
                }
            }

            // Check columns
            for (int col = 0; col < BoardSize; col++)
            {
                if (board[0, col] != ' ' && board[0, col] == board[1, col] && board[1, col] == board[2, col])
                {
                    return board[0, col];
                }
            }

            // Check diagonal (top-left to bottom-right)
            if (board[0, 0] != ' ' && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            {
                return board[0, 0];
            }

            // Check diagonal (bottom-left to top-right)
            if (board[2, 0] != ' ' && board[2, 0] == board[1, 1] && board[1, 1] == board[0, 2])
            {
                return board[2, 0];
            }

            // If no winner, return null
            return null;
        }
        // Check diagonals
        if (board[0, 0] != null && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
        {
            return board[0, 0];
        }

        if (board[0, 2] != null && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
        {
            return board[0, 2];
        }

        // Check for tie
        bool isTie = true;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == null)
                {
                    isTie = false;
                    break;
                }
            }
            if (!isTie)
            {
                break;
            }
        }
        if (isTie)
        {
            return "Tie";
        }

        return "";
    }

    static async Task<List<TicTacToeGame>> LoadGamesAsync(string filePath)
    {
        using var fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
        return await JsonSerializer.DeserializeAsync<List<TicTacToeGame>>(fileStream) ?? new List<TicTacToeGame>();
    }

    static async Task SaveGamesAsync(string filePath, List<TicTacToeGame> games)
    {
        using var fileStream = new FileStream(filePath, FileMode.Create);
        await JsonSerializer.SerializeAsync(fileStream, games);
    }

    static async Task<int> CreateNewGameAsync(List<TicTacToeGame> games)
    {
        var newGame = new TicTacToeGame
        {
            Id = games.Any() ? games.Max(g => g.Id) + 1 : 1,
            Player1 = "Player 1",
            Player2 = "Player 2"
        };
        games.Add(newGame);
        await SaveGamesAsync("games.json", games);
        return newGame.Id;
    }

    static async Task<List<TicTacToeGame>> GetGamesAsync()
    {
        var games = await LoadGamesAsync("games.json");
        return games;
    }

    {
        return board[2, 0];
    }

// If no winner, return null
return null;
}
Console.Clear();
PrintBoard(game.Board);

if (winner == "")
{
    Console.WriteLine("It's a tie!");
}
else
{
    Console.WriteLine($"The winner is {winner}!");
}

await SaveGameAsync("game.json", game);