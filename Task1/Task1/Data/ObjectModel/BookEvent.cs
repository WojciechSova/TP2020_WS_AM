using System;

namespace Task1.Data
{
    abstract class BookEvent
    {
        public Reader Reader { get; set; }
        public Book Book { get; set; }
        public DateTime DateTime { get; set; }

        
        protected BookEvent(Reader reader, BookEvent book, DateTime dateTime)
        {
            Reader = reader;
            Book = book;
            DateTime = dateTime;
        }
    }
}