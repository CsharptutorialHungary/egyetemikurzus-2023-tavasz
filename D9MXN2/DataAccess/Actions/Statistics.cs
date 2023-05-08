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
            Console.Clear();

            foreach (var person in db.People.Include(p => p.Notes))
            {
                _container.Add(person);
            }

            int people_with_notes_count = _container.Count(p => p.Notes.Count > 0);
            
            if (people_with_notes_count == 0)
            {
                Console.WriteLine("There is nobody with any notes....");
            }
            else
            {
                var ordered_people_with_index = _container
                    .AsParallel()
                    .OrderByDescending(p => p.Notes.Count)
                    .Where(p => p.Notes.Count > 0)
                    .Zip(Enumerable.Range(0, people_with_notes_count));

                foreach ((Person elem, int index) in ordered_people_with_index)
                {
                    Console.WriteLine($"{index + 1}: {elem.Username} with {elem.Notes.Count} notes.");
                }
            }

            _container.Clear();
            Console.WriteLine();
            Console.Write("Press any key to proceed..");
            Console.ReadKey();
            Console.Clear();
        }
    }
}