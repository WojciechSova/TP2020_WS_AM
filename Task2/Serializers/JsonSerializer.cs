using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace Serializers
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
            using FileStream _file = new FileStream(filePath, FileMode.Create);
            string serialized = JsonConvert.SerializeObject(obj, Formatting.Indented, Settings);
            _file.Write(Encoding.UTF8.GetBytes(serialized));
            _file.Flush();
        }

        public static T Deserialize<T>(string filePath)
        {
            if (File.Exists(filePath))
            {
                using FileStream _file = new FileStream(filePath, FileMode.Open);
                byte[] readedBytes = new byte[_file.Length];
                _file.Read(readedBytes);
                return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(readedBytes), Settings);
            }
            return default;
        }
    }
}
