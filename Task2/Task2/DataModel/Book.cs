using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Task2.DataModel
{
    class Book : ISerializable
    {
        private string Isbn { get; set; }
        private double Ratings { get; set; }

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
    }
}
