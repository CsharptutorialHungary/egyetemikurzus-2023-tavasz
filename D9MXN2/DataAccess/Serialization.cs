using Database.Models;

namespace D9MXN2.DataAccess
{
    public static class Serialization
    {
        public static void Serialize(this BaseModel model) {

        }

        public static IEnumerable<BaseModel> Deserialize(this BaseModel model) {
            return Enumerable.Empty<BaseModel>();
        }
    }
}