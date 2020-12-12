using System;
using System.Collections.Generic;
using System.Text;

namespace Task2.DataModel
{
    class DataHeader
    {
        private String typeName;
        private String varName;
        private string value;

        public DataHeader(string typeName, string varName, string value)
        {
            this.typeName = typeName;
            this.varName = varName;
            this.value = value;
        }

        public override string ToString()
        {
            return typeName + "->" + varName + "->" + value;
        }
    }
}
