using System;
using System.Collections.Generic;
using System.Text;

namespace Task1.Data
{
    public class BookRent : BookEvent
    {
        public BookRent(Reader reader, Book book, DateTime dateTime) : base(reader, book, dateTime)
        {
        }

        public BookRent(Reader reader, Book book) : base(reader, book)
        {
        }
    }
}
