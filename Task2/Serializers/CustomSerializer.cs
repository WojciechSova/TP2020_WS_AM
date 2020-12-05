using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace Serializers
{
    class CustomSerializer : IFormatter
    {
        private Type Type;

        public SerializationBinder Binder { get; set; }
        public StreamingContext Context { get; set; }
        public ISurrogateSelector SurrogateSelector { get; set; }

        public CustomSerializer(Type type)
        {
            this.Type = type;
        }

        public void Serialize(Stream serializationStream, Object graph)
        {
            List<PropertyInfo> properties = Type.GetProperties().ToList();
            StreamWriter streamWriter = new StreamWriter(serializationStream);
            streamWriter.WriteLine(Type.Name);
            foreach(PropertyInfo property in properties)
            {
                streamWriter.WriteLine(String.Format("{0}:{1}", property.Name, property.GetValue(graph)));
            }

            streamWriter.Flush();
        }

        public object Deserialize(Stream serializationStream)
        {
            Object obj = Activator.CreateInstance(Type);

            using (StreamReader sr = new StreamReader(serializationStream))
            {
                string typeName = sr.ReadLine();
                string contents = sr.ReadToEnd();
                List<String> pairs = contents.Split(new string[] { "\n", "\r\n"}, StringSplitOptions.RemoveEmptyEntries).ToList();
                string key, value;

                foreach(string pair in pairs)
                {
                    string[] keyValue = pair.Split(':');
                    key = keyValue[0];
                    value = keyValue[1];

                    PropertyInfo propertyInfo = Type.GetProperty(key);
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(obj, value, null);
                    }
                }
            }

            return obj;
        }
    }
}
