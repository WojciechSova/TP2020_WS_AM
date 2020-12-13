using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Task2.DataModel
{
    public class Book : ISerializable
    {
        public string Isbn { get; set; }
        public double Ratings { get; set; }

        [JsonConstructor]
        public Book(string isbn, double ratings)
        {
            Isbn = isbn;
            Ratings = ratings;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Isbn", Isbn);
            info.AddValue("Ratings", Ratings);
        }

        public Book (SerializationInfo serializationInfo)
        {
            Isbn = serializationInfo.GetString("Isbn");
            Ratings = serializationInfo.GetDouble("Ratings");
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Book)obj);
        }

        protected bool Equals(Book other)
        {
            return Isbn == other.Isbn && Ratings == other.Ratings;
        }
    }
}
