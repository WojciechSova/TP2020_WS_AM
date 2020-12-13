using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Linq;
using Task2.DataModel;

namespace Task2.Data
{
    public class CustomFormatter : Formatter
    {
        struct Data{
            public string className;
            public string name;
            public string value;
            public Data(string classnam, string nam,  string valu)
            {
                className = classnam;
                name = nam;
                value = valu;
            }
            public override string ToString()
            {
                return className + "->" + name + "->" + value;
            }
        }

        private List<Object> values = new List<DataHeader>();
        public override void Serialize(Stream serializationStream, object graph)
        {
            ISerializable serializable = (ISerializable)graph;
            SerializationInfo serializationInfo = new SerializationInfo(graph.GetType(), new FormatterConverter());
            StreamingContext streamingContext = new StreamingContext(StreamingContextStates.File);
            serializable.GetObjectData(serializationInfo, streamingContext);
            foreach (SerializationEntry item in serializationInfo)
            {
                this.WriteMember(item.Name, item.Value);
            }

            using (StreamWriter writer = new StreamWriter(serializationStream))
            {
                foreach (XElement element in values)
                {
                    writer.Write($"{element.Name} {element.Value}\n");
                }
            }
        }

        public override object Deserialize(Stream serializationStream)
        {
            DataContext dataContext;
            using (StreamReader reader = new StreamReader(serializationStream))
            {
                SerializationInfo serializationInfo = new SerializationInfo(typeof(DataContext), new FormatterConverter());
                string line = reader.ReadLine();

                while(line != null)
                {
                    string[] values = line.Split("_ ");
                    serializationInfo.AddValue(values[0], values[1]);
                    line = reader.ReadLine();
                }
                dataContext = new DataContext(serializationInfo);
            }

            return dataContext;
        }



        public override SerializationBinder Binder { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override StreamingContext Context { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override ISurrogateSelector SurrogateSelector { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        protected override void WriteArray(object obj, string name, Type memberType)
        {
            throw new NotImplementedException();
        }

        protected override void WriteBoolean(bool val, string name)
        {
            values.Add(new XElement(name, (object) val));
        }

        protected override void WriteByte(byte val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteChar(char val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteDateTime(DateTime val, string name)
        {
            values.Add(new XElement(name, val));
        }

        protected override void WriteDecimal(decimal val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteDouble(double val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteInt16(short val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteInt32(int val, string name)
        {
            values.Add(new XElement(name, val));
        }

        protected override void WriteInt64(long val, string name)
        {
            values.Add(new XElement(name, val));
        }

        protected override void WriteObjectRef(object obj, string name, Type memberType)
        {
            values.Add(new XElement(name, obj.ToString()));
        }

        protected override void WriteSByte(sbyte val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteSingle(float val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteTimeSpan(TimeSpan val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteUInt16(ushort val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteUInt32(uint val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteUInt64(ulong val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteValueType(object obj, string name, Type memberType)
        {
            values.Add(new XElement(name, obj.ToString()));
        }
    }

}
