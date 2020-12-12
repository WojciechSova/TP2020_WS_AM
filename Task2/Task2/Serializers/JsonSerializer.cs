using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace Task2.Data
{
    public class JsonSerializer
    {
        private readonly static JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
        };

        public static void Serialize(Object obj, string filePath)
        {
            FileStream file = new FileStream(filePath, FileMode.Create);
            string serialized = JsonConvert.SerializeObject(obj, Formatting.Indented, Settings);
            file.Write(Encoding.UTF8.GetBytes(serialized));
            file.Close();
        }

        public static T Deserialize<T>(string filePath)
        {
            if (File.Exists(filePath))
            {
                FileStream file = new FileStream(filePath, FileMode.Open);
                byte[] readedBytes = new byte[file.Length];
                file.Read(readedBytes);
                return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(readedBytes), Settings);
            }
            return default;
        }
    }
}
