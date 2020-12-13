using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;
using Task2.DataModel;
using Task2.Serializers;

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

        ObjectIDGenerator IDGenerator = new ObjectIDGenerator();
        CustomBinder CustomBinder = new CustomBinder();
        List<Data> Values = new List<Data>();
        List<Object> SerializedObjects = new List<Object>();
        List<Object> AllObjects = new List<Object>();
        Dictionary<string, Object> RefToObjects = new Dictionary<string, object>();
        
        
        
        
        public override void Serialize(Stream serializationStream, object graph)
        {
            ISerializable serializable = (ISerializable)graph;
            SerializationInfo serializationInfo = new SerializationInfo(graph.GetType(), new FormatterConverter());
            StreamingContext streamingContext = new StreamingContext(StreamingContextStates.File);
            CustomBinder.BindToName(graph.GetType(), out string assemblyName, out string typeName);
            serializable.GetObjectData(serializationInfo, streamingContext);

            foreach (SerializationEntry item in serializationInfo)
            {
                this.WriteMember(item.Name, item.Value);
            }

            StringBuilder fileContent = new StringBuilder(
                assemblyName + "->" + typeName + "->" + IDGenerator.GetId(graph, out bool _).ToString());

            foreach (Data data in Values)
            {
                fileContent.Append("\n" + data.ToString());
            }
            fileContent.Append("$\n");

            using (StreamWriter writer = new StreamWriter(serializationStream, Encoding.UTF8))
            {
                writer.Write(fileContent.ToString());
            }

            fileContent.Clear();
            Values.Clear();
            AllObjects.Add(graph);
            foreach (Object obj in SerializedObjects)
            {
                if (!AllObjects.Contains(obj))
                {
                    this.Serialize(serializationStream, obj);
                }
            }
        }

        public override object Deserialize(Stream serializationStream)
        {
            /*            DataContext dataContext;
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

                        return dataContext;*/
            return default;
        }



        public override SerializationBinder Binder { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override StreamingContext Context { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override ISurrogateSelector SurrogateSelector { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        protected override void WriteArray(object obj, string name, Type memberType)
        {
            long id = IDGenerator.GetId(obj, out bool firstTime);
            if (firstTime)
            {
                SerializedObjects.Add(obj);
            }
            Values.Add(new Data(memberType.Name, name, id.ToString()));
        }

        protected override void WriteBoolean(bool val, string name)
        {
            Values.Add(new Data(val.GetType().ToString(), name, val.ToString()));
        }

        protected override void WriteDateTime(DateTime val, string name)
        {
            Values.Add(new Data(val.GetType().ToString(), name, val.ToLocalTime().ToString("o")));
        }

        protected override void WriteDouble(double val, string name)
        {
            Values.Add(new Data(val.GetType().ToString(), name, val.ToString().Replace(",", ".")));
        }

        protected override void WriteInt32(int val, string name)
        {
            Values.Add(new Data(val.GetType().ToString(), name, val.ToString()));
        }

        protected override void WriteInt64(long val, string name)
        {
            Values.Add(new Data(val.GetType().ToString(), name, val.ToString()));
        }

        protected void WriteString(object obj, string name)
        {
            Values.Add(new Data(obj.GetType().ToString(), name, (String)obj));
        }

        protected override void WriteObjectRef(object obj, string name, Type memberType)
        {
            if (memberType == typeof(String))
            {
                WriteString(obj, name);
                return;
            }

            long id = IDGenerator.GetId(obj, out bool firstTime);
            if (firstTime)
            {
                SerializedObjects.Add(obj);
            }

            Values.Add(new Data(memberType.FullName, name, id.ToString()));
        }

        #region NotImplemented

        protected override void WriteByte(byte val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteChar(char val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteDecimal(decimal val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteInt16(short val, string name)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        #endregion
    }

}
