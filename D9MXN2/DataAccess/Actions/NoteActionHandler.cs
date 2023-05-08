using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<bool> Save(string username)
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

    public async Task DumpUserTo(string file_path, string username)
    {
        using (var db = SqliteDatabaseFactory<PeopleContext>.Create())
        {
            Person person = db.People
                .Where(p => p.Username == username)
                .Include(p => p.Notes)
                .Single();
            
            using (StreamWriter sw = new(file_path))
            {
                await sw.WriteAsync(person.Serialize());
            }
        }
    }
}