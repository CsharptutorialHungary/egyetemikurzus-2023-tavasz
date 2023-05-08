using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace D9MXN2.DataAccess.Actions;
public class PersonActionHandler
{
    public static async Task DumpPersonTo(string file_path, string username)
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

    public static void LoadPersonFrom(string file_path, string username)
    {
        string file_content = System.IO.File.ReadAllText(file_path);

        using (var db = SqliteDatabaseFactory<PeopleContext>.Create())
        {
            Person loaded_person = new Person().Deserialize(file_content);

            if (loaded_person.Username != username)
            {
                throw new InvalidOperationException("Tried to change not one's own data..");
            }

            Person saved_person = db.People
                .Where(p => p.Username == loaded_person.Username)
                .Include(p => p.Notes)
                .Single();

            // Fixme: not the best...
            db.Remove(saved_person); // this is going to make rubbish in the db, but apparently its intended.

            db.Add(loaded_person);
            db.SaveChanges();
        }
    }
}