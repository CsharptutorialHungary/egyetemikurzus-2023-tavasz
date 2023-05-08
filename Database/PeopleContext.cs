using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database;
public class PeopleContext : DbContext
{
    public PeopleContext(DbContextOptions options) : base(options) { }

    public DbSet<Person> People { get; set; } = null!;
    public DbSet<Note> Notes { get; set; } = null!;
}