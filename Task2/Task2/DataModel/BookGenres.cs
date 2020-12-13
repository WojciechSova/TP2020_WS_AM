using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Task2.DataModel
{
    public class BookGenres : ISerializable
    {
        public string Name { get; set; }

        [JsonConstructor]
        public BookGenres(string name)
        {
            Name = name;
        }

        public BookGenres (SerializationInfo serializationInfo, StreamingContext context)
        {
            Name = serializationInfo.GetString("Name");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
        }

        public override bool Equals(object obj)
        {
            return obj is BookGenres genres &&
                   Name == genres.Name;
        }
    }
}
