using System;
using System.Collections.Generic;
using System.Text;

namespace Task1.Data
{
    public class BookReturn : BookEvent
    {
        public BookReturn(Reader reader, BookState bookState) : base(reader, bookState)
        {
        }

        public BookReturn(Reader reader, BookState bookState, DateTime dateTime) : base(reader, bookState, dateTime)
        {
        }

        public override string ToString()
        {
            return "Return - " + base.ToString();
        }
    }
}
