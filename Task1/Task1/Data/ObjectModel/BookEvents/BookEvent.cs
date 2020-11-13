using System;

namespace Task1.Data
{
    public abstract class BookEvent
    {
        public Reader Reader { get; set; }
        public BookState BookState { get; set; }
        public DateTime EventTime { get; }

        
        protected BookEvent(Reader reader, BookState bookState, DateTime dateTime)
        {
            Reader = reader;
            BookState = bookState;
            EventTime = dateTime;
        }

        protected BookEvent(Reader reader, BookState bookState) : this(reader, bookState, DateTime.Now)
        {

        }


    }
}