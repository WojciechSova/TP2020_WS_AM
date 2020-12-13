using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Task2.DataModel
{
    [Serializable]
    public class ClassC : ISerializable
    {
        public string Name { get; set; }
        public long Number { get; set; }
        public ClassA ClassA { get; set; }
        public ClassB ClassB { get; set; }

        public ClassC(string name, long number, ClassA classA, ClassB classB)
        {
            Name = name;
            Number = number;
            ClassA = classA;
            ClassB = classB;
        }

        public ClassC(string name, long number)
        {
            Name = name;
            Number = number;
        }

        public ClassC()
        {
        }

        public ClassC(SerializationInfo serializationInfo, StreamingContext context)
        {
            Name = serializationInfo.GetString("Name");
            Number = serializationInfo.GetInt64("Number");
          
            ClassA = (ClassA)serializationInfo.GetValue("ClassA", typeof(ClassA));
            ClassB = (ClassB)serializationInfo.GetValue("ClassB", typeof(ClassB));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Number", Number);
            info.AddValue("ClassA", ClassA);
            info.AddValue("ClassB", ClassB);
        }
    }
}
