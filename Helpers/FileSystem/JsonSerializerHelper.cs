using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Ben.Tools.Helpers.FileSystem
{
    public static class JsonSerializerHelper
    {
      public static readonly JsonSerializerSettings SerializeEverythingSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All // Permet de désérialiser les interfaces.
        };

        public static void SaveInFile<SaveType>(SaveType saveType, string path, bool indent = true) =>
            File.WriteAllText(path, ToJson(saveType, indent));

        public static string ToJson<SerializeType>(SerializeType serializeType, bool indent = true) => 
            JsonConvert.SerializeObject(
                serializeType,
                indent ? Formatting.Indented : Formatting.None,
                SerializeEverythingSettings);

        public static LoadedType LoadFromFile<LoadedType>(string path) => ToType<LoadedType>(File.ReadAllText(path));

        public static TypeToLoad ToType<TypeToLoad>(byte[] bytes)
                where TypeToLoad : class
        {
            using (var stream = new MemoryStream(bytes))
            using (var reader = new StreamReader(stream, Encoding.UTF8))
                return JsonSerializer.Create()
                                     .Deserialize(reader, typeof(TypeToLoad)) as TypeToLoad;
        }

        public static TDataType ToType<TDataType>(string json) => 
            JsonConvert.DeserializeObject<TDataType>(json, SerializeEverythingSettings);
    }
}
