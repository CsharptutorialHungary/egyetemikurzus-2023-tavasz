using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Database;
internal class PeopleDesignTimeContextFactory : IDesignTimeDbContextFactory<PeopleContext>
{
    const string DB_PATH = "../data.db";

    // https://learn.microsoft.com/en-gb/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli
    public PeopleContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PeopleContext>();
        optionsBuilder.UseSqlite($"Data Source={DB_PATH}");

        return new PeopleContext(optionsBuilder.Options);
    }
}
