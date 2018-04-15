using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Ben.Tools.Helpers.FileSystem
{
    /// <summary>
    /// Peut tout désérializer, que ce soit des classes abstraites ou des interfaces.
    /// Déduit automatiquement le type enfant même en spécifiant un type parent :
    /// Deserialize<Mamiphere>(rawJson) désérializera en vrai le type du rawjson soit probablement un Elephant.
    /// </summary>
    public static class JsonSerializerHelper
    {
         
        public static readonly JsonSerializerSettings SerializeEverythingSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All // Permet de désérialiser les interfaces.
        };

        public static void ToFile<TDataType>(
            TDataType dataType,
            string path,
            bool indent = true)
        {
            File.WriteAllText(
                path,
                ToJson(dataType, indent));
        }

        public static string ToJson<TDataType>(
            TDataType dataType,
            bool indent = true) => 
        JsonConvert.SerializeObject(
            dataType,
            indent ? Formatting.Indented : Formatting.None,
            SerializeEverythingSettings);

        public static TDataType FromFile<TDataType>(string path) => ToType<TDataType>(File.ReadAllText(path));

        public static TDataType ToType<TDataType>(byte[] byteArray)
                where TDataType : class
        {
            using (var stream = new MemoryStream(byteArray))
            using (var reader = new StreamReader(stream, Encoding.UTF8))
                return JsonSerializer.Create()
                                     .Deserialize(reader, typeof(TDataType)) as TDataType;
        }

        public static TDataType ToType<TDataType>(string json) => 
            JsonConvert.DeserializeObject<TDataType>(json, SerializeEverythingSettings);

        
    }
}
