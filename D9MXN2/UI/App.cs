using System;
using D9MXN2.DataAccess.Actions;
using Database.Models;

namespace D9MXN2.UI;

public class App : CommandScreen
{
    protected override Dictionary<string, Delegate> Commands { get; set; } = new() {
        {"login", () => ScreenRender(new LoginScreen())},
        {"register", () => ScreenRender(new RegisterScreen())},
        {"statistics", () => Statistics.PrintStatistics()},
        {"help", () => Help()},
        {"exit", () => Environment.Exit(0)}
    };

    static void Help()
    {
        Console.WriteLine("This is an awesome note handling application with user handling.");
        Console.WriteLine();
        Console.WriteLine("Commands:");
        Console.WriteLine("login: You can login here if you are registered.");
        Console.WriteLine("register: You can register here.");
        Console.WriteLine("statistics: Print statistics about the users.");
        Console.WriteLine("exit: closes the application.");
        Console.WriteLine();
        Console.WriteLine("Press anything to proceed.");
        Console.ReadKey();
        Console.Clear();
    }

    static void ScreenRender(BaseScreen screen)
    {
        screen.Render();
    }
}

public class HomeScreen : CommandScreen
{
    const string LOGOUT_COMMAND = "logout";
    string _username;
    ActionHandler<Note> _note_action_handler = new();

    protected override Dictionary<string, Delegate> Commands { get; set; } = new() {
        {LOGOUT_COMMAND, () => Console.WriteLine("Goodbye")},
        {"exit", () => Environment.Exit(0)}
    };

    protected override bool SuccessfulCommandInvoke(string command)
    {
        if (command == LOGOUT_COMMAND) { return false; } // this will break the render loop

        return base.SuccessfulCommandInvoke(command);
    }

    public HomeScreen(string username)
    {
        this._username = username;

        this.Commands.Add("notes", this.PrintNotes);
        this.Commands.Add("add", this.AddNote);
        this.Commands.Add("delete", this.DeleteNote);
        this.Commands.Add("save", this.SaveNotes);
        this.Commands.Add("dump", this.DumpUser);
        this.Commands.Add("load", this.LoadUser);
    }

    void PrintNotes()
    {
        Console.Clear();

        _note_action_handler.PrintAllNotes(this._username);

        Console.WriteLine();
        Console.Write("Press any key to proceed...");
        Console.ReadKey();
    }

    async Task<bool> SaveNotes()
    {
        await _note_action_handler.Save(this._username);

        return true;
    }

    void AddNote()
    {
        Console.WriteLine("Write a note (max length is 200)!");
        string note = GetInputWithMsg("");

        if (note.Length > 200)
        {
            Console.WriteLine("[Error]: Note was too long, it got discarded");
            return;
        }

        _note_action_handler.Add(new Note() { Value = note });
        Console.WriteLine();
    }

    string GetFilePath()
    {
        const string CANCEL_COMMAND = "cancel";

        while (true)
        {
            Console.WriteLine($"If you want to cancel this operation, type '{CANCEL_COMMAND}'");
            string user_given_file_path = GetInputWithMsg("Please give the wanted file path");

            if (user_given_file_path == CANCEL_COMMAND) { return string.Empty; }

            if (!System.IO.File.Exists(user_given_file_path))
            {
                Console.WriteLine($"*{user_given_file_path}* does not exist.");
                continue;
            }

            return user_given_file_path;
        }
    }

    async Task<bool> DumpUser()
    {
        string file_path = GetFilePath();
        if (file_path == string.Empty) { return false; }

        await _note_action_handler.DumpPersonTo(file_path, _username);
        return true;
    }

    void LoadUser() {
        string file_path = GetFilePath();
        if (file_path == string.Empty) { return;}

        _note_action_handler.LoadPersonFrom(file_path, _username);
    }

    void DeleteNote() {
        _note_action_handler.DeleteNote(_username);
    }
}