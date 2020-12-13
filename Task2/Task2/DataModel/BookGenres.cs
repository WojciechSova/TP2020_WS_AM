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

        public BookGenres (SerializationInfo serializationInfo)
        {
            Name = serializationInfo.GetString("Name");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BookGenres)obj);
        }

        protected bool Equals(BookGenres other)
        {
            return Name == other.Name;
        }
    }
}
