using Microsoft.EntityFrameworkCore;

namespace D9MXN2.DataAccess;


public static class SqliteDatabaseFactory<T> where T : DbContext
{
    const string DB_PATH = "data.db";

    public static DbContextOptionsBuilder<T> GetOptionsBuilder(string db_path)
    {
        DbContextOptionsBuilder<T> options_builder = new();
        options_builder.UseSqlite($"Data Source={db_path}");

        return options_builder;
    }

    public static T Create()
    {
        var options_builder = GetOptionsBuilder(DB_PATH);
        // this is slow
        T? result = (T?)Activator.CreateInstance(typeof(T), options_builder.Options);
        if(result == null) {
            throw new ArgumentNullException("Invalid generic type, got null after CreateInstance.");
        }

        return result;
    }
}