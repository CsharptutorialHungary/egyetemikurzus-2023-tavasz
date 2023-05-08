using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace D9MXN2.DataAccess.Actions;
public class NoteActionHandler<T> : ICollection<T> where T : Note
{
    #region ICollection overrides
    List<T> _Values = new();
    public int Count => _Values.Count;

    public bool IsReadOnly => false;

    public void Add(T item)
    {
        _Values.Add(item);
    }

    public void Clear()
    {
        _Values.Clear();
    }

    public bool Contains(T item)
    {
        return _Values.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        _Values.CopyTo(array, arrayIndex);
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var elem in this._Values)
        {
            yield return elem;
        }
    }

    public bool Remove(T item)
    {
        return this._Values.Remove(item);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
    #endregion

    public async Task<bool> SaveNotes(string username)
    {
        using (var db = SqliteDatabaseFactory<PeopleContext>.Create())
        {
            try
            {
                var person = db.People.Single(p => p.Username == username);
                person.Notes.AddRange(this._Values);
            }
            catch (InvalidOperationException exc)
            {
                Console.WriteLine("[Error]: incorrect username to save");
                Console.WriteLine($"[Error]: {exc.Message}");
                Environment.Exit(-2);
            }

            await db.SaveChangesAsync();
            this._Values.Clear();

            return true;
        }
    }

    public void PrintAllNotes(string username)
    {
        using (var db = SqliteDatabaseFactory<PeopleContext>.Create())
        {
            var saved_notes = db.People
                .Where(p => p.Username == username)
                .Include(p => p.Notes)
                .Single();

            foreach ((var saved_note, int index) in saved_notes.Notes.Zip(Enumerable.Range(0, saved_notes.Notes.Count)))
            {
                Console.WriteLine($"{index}) {saved_note}");
            }

            foreach (var unsaved_note in this._Values)
            {
                Console.WriteLine($"!UNSAVED! {unsaved_note}");
            }
        }
    }

    int GetNoteIDToDelete(IEnumerable<Note> notes)
    {
        Console.WriteLine("Which note should be deleted");

        for (int try_count = 0; try_count < 3; ++try_count)
        {

            Console.Write("id: ");
            string user_answer = Console.ReadLine() ?? "";

            if (!int.TryParse(user_answer, out var id_to_delete))
            {
                Console.WriteLine("Not a valid int...");
                continue;
            }

            var has_the_given_id = notes.Select(n => n.Id).Contains(id_to_delete);
            if (!has_the_given_id)
            {
                Console.WriteLine("Provided id was not found...");
                continue;
            }

            return id_to_delete;
        }
        return -1;
    }

    public void DeleteNote(string username)
    {
        using (var db = SqliteDatabaseFactory<PeopleContext>.Create())
        {
            var user_notes = db.People
                .Where(p => p.Username == username)
                .Include(p => p.Notes)
                .Select(p => p.Notes)
                .Single();

            if (user_notes.Count == 0)
            {
                Console.WriteLine("You dont have notes yet...");
                return;
            }

            foreach (var user_note in user_notes)
            {
                System.Console.WriteLine($"{user_note.Id}: {user_note}");
            }

            int id_to_delete = GetNoteIDToDelete(user_notes);
            if (id_to_delete == -1)
            {
                Console.WriteLine("[Error]: Could not provide valid id...");
                return;
            }

            db.Remove(
                user_notes
                    .Where(n => n.Id == id_to_delete)
                    .Single()
            );

            db.SaveChanges();
        }
    }
}