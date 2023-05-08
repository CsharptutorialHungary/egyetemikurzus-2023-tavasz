using System.ComponentModel.DataAnnotations;

namespace Database.Models;
public class Person : BaseModel, IComparable<Person>
{
    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string Password { get; set; } = null!;

    public List<Note> Notes { get; set; } = new();


    public override string ToString()
    {
        return $"{Username}:{Password}"; // not safe
    }

    #region compare overrides
    public int CompareTo(Person? other)
    {
        if (other is null) return 1;

        if (this.Notes.Count < other.Notes.Count)
        {
            return -1;
        }
        else if (this.Notes.Count > other.Notes.Count)
        {
            return 1;
        }

        return 0;
    }

    public static bool operator >(Person a, Person b)
    {
        return a.Notes.Count > b.Notes.Count;
    }

    public static bool operator <(Person a, Person b)
    {
        return a.Notes.Count < b.Notes.Count;
    }
    #endregion
}