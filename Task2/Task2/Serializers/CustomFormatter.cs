using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Task2.Serializers;

namespace Task2.Data
{
    public class CustomFormatter : Formatter
    {
        struct Data
        {
            public string className;
            public string name;
            public string value;
            public Data(string classnam, string nam, string valu)
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
        Dictionary<string, object> RefToObjects = new Dictionary<string, object>();

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

            using (StreamWriter writer = new StreamWriter(serializationStream, Encoding.UTF8, 32, true))
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
            List<string> ObjectsInString = new List<string>();
            List<Object> Objects = new List<object>();
            if (serializationStream != null)
            {
                using (StreamReader reader = new StreamReader(serializationStream, Encoding.UTF8, false, 32, true))
                {
                    string data = reader.ReadToEnd();
                    string[] objs = data.Split('$');
                    foreach (string obj in objs)
                    {
                        ObjectsInString.Add(obj);
                    }
                }

                foreach (string obj in ObjectsInString)
                {
                    string[] objectProperties = obj.Split('\n');
                    if (objectProperties[0] == "")
                    {
                        List<String> tmp = objectProperties.OfType<String>().ToList();
                        tmp.RemoveAt(0);
                        objectProperties = tmp.ToArray();
                    }

                    string[] objectAtrribute = objectProperties[0].Split("->");
                    if (objectAtrribute.Length != 3)
                        continue;

                    RefToObjects.Add(
                        objectAtrribute[2], FormatterServices.GetSafeUninitializedObject(CustomBinder.BindToType(objectAtrribute[0], objectAtrribute[1])));
                }

                foreach (string obj in ObjectsInString)
                {
                    string[] objectProperties = obj.Split('\n');
                    if (objectProperties[0] == "")
                    {
                        List<String> tmp = objectProperties.OfType<String>().ToList();
                        tmp.RemoveAt(0);
                        objectProperties = tmp.ToArray();
                    }

                    string[] objectAtrribute = objectProperties[0].Split("->");
                    if (objectAtrribute.Length != 3)
                        continue;

                    Type objType = CustomBinder.BindToType(objectAtrribute[0], objectAtrribute[1]);
                    SerializationInfo serializationInfo = new SerializationInfo(objType, new FormatterConverter());
                    StreamingContext streamingContext = new StreamingContext(StreamingContextStates.File);

                    for (int i = 1; i < objectProperties.Length; i++)
                    {
                        string[] objectAttributes = objectProperties[i].Split("->");
                        Type atrributeType = CustomBinder.BindToType(objectAtrribute[0], objectAttributes[0]);
                        if (atrributeType == null)
                        {
                            DeSerializeUnknownType(serializationInfo, Type.GetType(objectAttributes[0]), objectAttributes[1], objectAttributes[2]);
                        }
                        else
                        {
                            serializationInfo.AddValue(objectAttributes[1], RefToObjects[objectAttributes[2]], atrributeType);
                        }
                    }

                    Type[] constructorTypes = { serializationInfo.GetType(), streamingContext.GetType() };
                    object[] constructorArguments = { serializationInfo, streamingContext };
                    RefToObjects[objectAtrribute[2]].GetType().GetConstructor(constructorTypes).Invoke(RefToObjects[objectAtrribute[2]], constructorArguments);
                    Objects.Add(RefToObjects[objectAtrribute[2]]);
                }
            }
            return Objects[0];
        }

        private void DeSerializeUnknownType(SerializationInfo serializationInfo, Type type, string name, string val)
        {
            switch (type.ToString())
            {
                case "System.DateTime":
                    serializationInfo.AddValue(name, DateTime.Parse(val, null, DateTimeStyles.AssumeLocal));
                    break;
                case "System.String":
                    serializationInfo.AddValue(name, val);
                    break;
                case "System.Double":
                    serializationInfo.AddValue(name, Double.Parse(val));
                    break;
                case "System.Boolean":
                    serializationInfo.AddValue(name, Boolean.Parse(val));
                    break;
            }
        }



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
            Values.Add(new Data(val.GetType().ToString(), name, val.ToString()));
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
        public override SerializationBinder Binder { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override StreamingContext Context { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override ISurrogateSelector SurrogateSelector { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
