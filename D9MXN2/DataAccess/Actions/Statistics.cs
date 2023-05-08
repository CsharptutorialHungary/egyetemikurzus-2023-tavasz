using System;
using System.Linq;
using D9MXN2.Models;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace D9MXN2.DataAccess.Actions;
public static class Statistics
{
    static PersonContainer<Person> _container = new();

    public static void PrintStatistics()
    {
        using (var db = SqliteDatabaseFactory<PeopleContext>.Create())
        {
            foreach (var person in db.People.Include(p => p.Notes))
            {
                _container.Add(person);
            }

            var ordered_people_with_index = _container
                .OrderBy(p => p.Notes.Count)
                .Zip(Enumerable.Range(0, _container.Count));

            foreach ((Person elem, int index) in ordered_people_with_index)
            {
                Console.WriteLine($"{index+1}: {elem.Username} with {elem.Notes.Count} notes.");
            }
        }
    }
}