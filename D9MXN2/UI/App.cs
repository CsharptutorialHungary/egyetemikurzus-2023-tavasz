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
    NoteActionHandler<Note> _note_action_handler = new();
    
    protected override Dictionary<string, Delegate> Commands { get; set; } = new() {
        {LOGOUT_COMMAND, () => Console.WriteLine("Goodbye")},
        {"exit", () => Environment.Exit(0)}
    };

    protected override bool SuccessfulCommandInvoke(string command)
    {
        if (command == LOGOUT_COMMAND) { return false; } // this will break the render loop

        return base.SuccessfulCommandInvoke(command);
    }

    public HomeScreen(string username) {
        this._username = username;

        this.Commands.Add("notes", this.PrintNotes);
        this.Commands.Add("add", this.AddNote);
        this.Commands.Add("save", this.SaveNotes);

        // {"delete", () => System.Console.WriteLine()},
        // {"dump", () => System.Console.WriteLine()},
        // {"load", () => System.Console.WriteLine()},
    }


    void PrintNotes() {
        Console.Clear();
        
        _note_action_handler.PrintAllNotes(this._username);
        
        Console.WriteLine();
        Console.Write("Press any key to proceed...");
        Console.ReadKey();
    }

    async Task<bool> SaveNotes() {
        await _note_action_handler.Save(this._username);

        return true;
    }

    void AddNote() {
        Console.WriteLine("Write a note (max length is 200)!");
        string note = GetInputWithMsg("");

        if(note.Length > 200) {
            Console.WriteLine("[Error]: Note was too long, it got discarded");
            return;
        }

        _note_action_handler.Add(new Note() { Value = note });
        Console.WriteLine();
    }
}