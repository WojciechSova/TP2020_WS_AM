using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Task2.DataModel
{
    public class ClassA : ISerializable
    {
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public bool Available { get; set; }
        public ClassB ClassB { get; set; }
        public ClassC ClassC { get; set; }

        [JsonConstructor]
        public ClassA(string name, DateTime dateTime, bool available, ClassB classB, ClassC classC)
        {
            Name = name;
            DateTime = dateTime;
            Available = available;
            ClassB = classB;
            ClassC = classC;
        }

        public ClassA(string name, DateTime dateTime, bool available)
        {
            Name = name;
            DateTime = dateTime;
            Available = available;
        }

        public ClassA()
        {
        }

        public ClassA(SerializationInfo serializationInfo, StreamingContext context)
        {
            Name = serializationInfo.GetString("Name");
            DateTime = serializationInfo.GetDateTime("DateTime");
            Available = serializationInfo.GetBoolean("Available");
            ClassB = (ClassB) serializationInfo.GetValue("ClassB", typeof(ClassB));
            ClassC = (ClassC) serializationInfo.GetValue("ClassC", typeof(ClassC));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("DateTime", DateTime);
            info.AddValue("Available", Available);
            info.AddValue("ClassB", ClassB);
            info.AddValue("ClassC", ClassC);
        }
    }
}
