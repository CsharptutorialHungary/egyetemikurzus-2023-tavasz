using System.ComponentModel.DataAnnotations;

namespace Database.Models;
public class Person : BaseModel
{
    [Required]
    [MaxLength(100)]
    public string Username {get; set;} = null!;

    [Required]
    [MaxLength(100)]
    public string Password {get; set;} = null!;

    public List<Note> Notes {get; set;} = new();
}