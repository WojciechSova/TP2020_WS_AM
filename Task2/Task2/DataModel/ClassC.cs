using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Task2.DataModel
{
    public class ClassC : ISerializable
    {
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public ClassA ClassA { get; set; }
        public ClassB ClassB { get; set; }

        [JsonConstructor]
        public ClassC(string name, DateTime dateTime, ClassA classA, ClassB classB)
        {
            Name = name;
            DateTime = dateTime;
            ClassA = classA;
            ClassB = classB;
        }

        public ClassC(string name, DateTime dateTime)
        {
            Name = name;
            DateTime = dateTime;
        }

        public ClassC()
        {
        }

        public ClassC(SerializationInfo serializationInfo, StreamingContext context)
        {
            Name = serializationInfo.GetString("Name");
            DateTime = serializationInfo.GetDateTime("DateTime");
          
            ClassA = (ClassA)serializationInfo.GetValue("ClassA", typeof(ClassA));
            ClassB = (ClassB)serializationInfo.GetValue("ClassB", typeof(ClassB));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("DateTime", DateTime);
            info.AddValue("ClassA", ClassA);
            info.AddValue("ClassB", ClassB);
        }
    }
}
