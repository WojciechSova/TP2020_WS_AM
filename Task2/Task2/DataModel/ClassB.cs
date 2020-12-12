using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Task2.DataModel
{
    class ClassB : ISerializable
    {
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public double Amount { get; set; }
        public ClassA ClassA { get; set; }
        public ClassC ClassC { get; set; }

        public ClassB(string name, DateTime dateTime, double amount, ClassA classA, ClassC classC)
        {
            Name = name;
            DateTime = dateTime;
            Amount = amount;
            ClassA = classA;
            ClassC = classC;
        }

        public ClassB(string name, DateTime dateTime, double amount)
        {
            Name = name;
            DateTime = dateTime;
            Amount = amount;
        }

        public ClassB(SerializationInfo serializationInfo)
        {
            Name = serializationInfo.GetString("Name");
            DateTime = serializationInfo.GetDateTime("DateTime");
            Amount = serializationInfo.GetDouble("Amount");
            ClassA = (ClassA)serializationInfo.GetValue("ClassA", typeof(ClassA));
            ClassC = (ClassC)serializationInfo.GetValue("ClassC", typeof(ClassC));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("DateTime", DateTime);
            info.AddValue("Amount", Amount);
            info.AddValue("ClassA", ClassA);
            info.AddValue("ClassC", ClassC);
        }
    }
}
