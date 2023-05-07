using D9MXN2.DataAccess;
using Database;

using(var db = SqliteDatabaseFactory<PeopleContext>.Create()) {
    System.Console.WriteLine(db.People.Count());
}