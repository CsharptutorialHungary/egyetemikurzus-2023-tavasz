using System.Text.Json;
using Database.Models;

namespace D9MXN2.DataAccess
{
    public static class Serialization
    {
        static JsonSerializerOptions serializerOptions = new()
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        public static string Serialize(this Person model)
        {
            return JsonSerializer.Serialize<Person>(model, serializerOptions);
        }

        public static Person Deserialize(this Person model, string data)
        {
            try
            {
                var deserialized = JsonSerializer.Deserialize<Person>(data, serializerOptions);
                if (deserialized is null)
                {
                    throw new JsonException("Json with null value is not accepted");
                }

                return deserialized;
            }
            catch (JsonException)
            {
                Console.WriteLine("[Error] Inctorrect json");
                throw;
            }
        }
    }
}