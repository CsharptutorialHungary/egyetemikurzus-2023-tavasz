using D9MXN2.Models;
using Database;
using Database.Models;
using System.Linq;

namespace D9MXN2.DataAccess.Actions;

public class UserActions
{
    public static string Login(User user)
    {
        string error_msg = "";

        using (var db = SqliteDatabaseFactory<PeopleContext>.Create())
        {
            var user_with_correct_credentails = db.People
                .Where(p => p.Username == user.Username && p.Password == user.Password)
                .ToList();

            if(user_with_correct_credentails.Count() != 1) {
                error_msg = "User not found.";
            }
        }

        return error_msg;
    }

    public static string Register(User new_user)
    {
        string error_msg = "";

        using (var db = SqliteDatabaseFactory<PeopleContext>.Create())
        {
            var person_with_same_username = db.People.Where(p => p.Username == new_user.Username).ToList();

            if (person_with_same_username.Count() > 0)
            {
                error_msg = "Usernam taken!";
                return error_msg;
            }

            db.Add(
                new Person()
                {
                    Username = new_user.Username,
                    Password = new_user.Password
                }
            );
            db.SaveChanges();
        }

        return error_msg;
    }
}