using System.ComponentModel.DataAnnotations;

namespace Database.Models;
public class Note : BaseModel
{
    [Required]
    [MaxLength(250)]
    public string Value { get; set; } = null!;
    // TODO datetime

    public override string ToString()
    {
        return this.Value;
    }
}