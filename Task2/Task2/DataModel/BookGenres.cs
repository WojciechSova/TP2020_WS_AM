using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Task2.DataModel
{
    public class BookGenres : ISerializable
    {
        string Name { get; set; }

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
    }
}
