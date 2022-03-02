using BackupsExtra.Services;
using Newtonsoft.Json;

namespace BackupsExtra.Tools.SerializationClass
{
    public class Serializator
    {
        public static string Serialize(BackUpJobExtra backUpJobExtra)
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                Formatting = Formatting.Indented,
            };

            return JsonConvert.SerializeObject(backUpJobExtra, settings);
        }

        public static BackUpJobExtra Deserialize(string serializedObject)
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                Formatting = Formatting.Indented,
            };

            return JsonConvert.DeserializeObject(serializedObject, settings) is BackUpJobExtra service ? service : null;
        }
    }
}