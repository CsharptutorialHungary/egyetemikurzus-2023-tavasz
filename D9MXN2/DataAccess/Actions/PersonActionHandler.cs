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
            Person saved_person = db.People
                .Where(p => p.Username == username)
                .Include(p => p.Notes)
                .Single();


            // Fixme: not the best...
            Person loaded_person = saved_person.Deserialize(file_content);
            db.Remove(saved_person.Notes);
            db.Remove(saved_person);

            db.Add(loaded_person);
            db.SaveChanges();
        }
    }
}