using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Task2.DataModel
{
    [Serializable]
    [JsonObject]
    public class ClassB : ISerializable
    {
        public string Name { get; set; }
        public long Number { get; set; }
        public double Amount { get; set; }
        public ClassA ClassA { get; set; }
        public ClassC ClassC { get; set; }

        public ClassB(string name, long number, double amount, ClassA classA, ClassC classC)
        {
            Name = name;
            Number = number;
            Amount = amount;
            ClassA = classA;
            ClassC = classC;
        }

        public ClassB(string name, long number, double amount)
        {
            Name = name;
            Number = number;
            Amount = amount;
        }

        public ClassB()
        {
        }

        public ClassB(SerializationInfo serializationInfo, StreamingContext context)
        {
            Name = serializationInfo.GetString("Name");
            Number = serializationInfo.GetInt64("Number");
            Amount = serializationInfo.GetDouble("Amount");
            ClassA = (ClassA)serializationInfo.GetValue("ClassA", typeof(ClassA));
            ClassC = (ClassC)serializationInfo.GetValue("ClassC", typeof(ClassC));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Number", Number);
            info.AddValue("Amount", Amount);
            info.AddValue("ClassA", ClassA);
            info.AddValue("ClassC", ClassC);
        }
    }
}
