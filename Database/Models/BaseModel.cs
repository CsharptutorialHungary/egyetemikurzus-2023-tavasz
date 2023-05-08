using System.Text.Json.Serialization;

namespace Database.Models;

public class BaseModel {
    [JsonIgnore]
    public int Id{get; set;}
}