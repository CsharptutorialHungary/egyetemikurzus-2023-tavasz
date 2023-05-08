using System.Text.Json;
using Database.Models;

namespace D9MXN2.DataAccess
{
    public static class Serialization
    {
        public static string Serialize(this Person model)
        {
            return JsonSerializer.Serialize<Person>(model);
        }

        public static IEnumerable<Person> Deserialize(this Person model, string data)
        {
            try {
                var deserialized =  JsonSerializer.Deserialize<List<Person>>(data);

                return deserialized ?? Enumerable.Empty<Person>();
            } catch(JsonException) {
                Console.WriteLine("[Error] Inctorrect json");
                throw;
            }
        }
    }
}