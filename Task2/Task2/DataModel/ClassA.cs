using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Task2.DataModel
{
    public class ClassA : ISerializable
    {
        public string Name { get; set; }
        public long Number{ get; set; }
        public bool Available { get; set; }
        public ClassB ClassB { get; set; }
        public ClassC ClassC { get; set; }

        [JsonConstructor]
        public ClassA(string name, long number, bool available, ClassB classB, ClassC classC)
        {
            Name = name;
            Number = number;
            Available = available;
            ClassB = classB;
            ClassC = classC;
        }

        public ClassA(string name, long number, bool available)
        {
            Name = name;
            Number = number;
            Available = available;
        }

        public ClassA()
        {
        }

        public ClassA(SerializationInfo serializationInfo, StreamingContext context)
        {
            Name = serializationInfo.GetString("Name");
            Number = serializationInfo.GetInt64("Number");
            Available = serializationInfo.GetBoolean("Available");
            ClassB = (ClassB) serializationInfo.GetValue("ClassB", typeof(ClassB));
            ClassC = (ClassC) serializationInfo.GetValue("ClassC", typeof(ClassC));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Number", Number);
            info.AddValue("Available", Available);
            info.AddValue("ClassB", ClassB);
            info.AddValue("ClassC", ClassC);
        }
    }
}
