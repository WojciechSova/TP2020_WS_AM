using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;

namespace Task2.Serializers
{
    class CustomBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            Assembly assembly = Assembly.Load(assemblyName);
            return assembly.GetType(typeName);
        }

        public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            Assembly assembly = serializedType.Assembly;
            assemblyName = assembly.FullName;
            typeName = serializedType.FullName;
        }
    }
}
