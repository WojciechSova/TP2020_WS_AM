using System;

namespace Task1.Data
{
    public abstract class BookEvent
    {
        public Reader Reader { get; set; }
        public Book Book { get; set; }
        public DateTime EventTime { get; }

        
        protected BookEvent(Reader reader, Book book, DateTime dateTime)
        {
            Reader = reader;
            Book = book;
            EventTime = dateTime;
        }

        protected BookEvent(Reader reader, Book book) : this(reader, book, DateTime.Now)
        {

        }


    }
}