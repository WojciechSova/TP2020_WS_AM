using System;
using System.Collections.Generic;
using System.Text;

namespace Task1.Data
{
    public class BookReturn : BookEvent
    {
        public BookReturn(Reader reader, Book book) : base(reader, book)
        {
        }

        public BookReturn(Reader reader, Book book, DateTime dateTime) : base(reader, book, dateTime)
        {
        }
    }
}
