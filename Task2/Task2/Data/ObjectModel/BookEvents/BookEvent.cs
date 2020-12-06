using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Task2.Data
{
    public abstract class BookEvent
    {
        public Guid BookEventGuid { get; set; }
        public Reader Reader { get; set; }
        public BookState BookState { get; set; }
        public DateTime EventTime { get; set; }

        
        protected BookEvent(Reader reader, BookState bookState, DateTime dateTime)
        {
            BookEventGuid = Guid.NewGuid();
            Reader = reader;
            BookState = bookState;
            EventTime = dateTime;
        }

        protected BookEvent(Reader reader, BookState bookState) : this(reader, bookState, DateTime.Now)
        {
            BookEventGuid = Guid.NewGuid();
        }

        public override bool Equals(object obj)
        {
            return obj is BookEvent @event &&
                   EqualityComparer<Reader>.Default.Equals(Reader, @event.Reader) &&
                   EqualityComparer<BookState>.Default.Equals(BookState, @event.BookState) &&
                   EventTime == @event.EventTime;
        }

        public override int GetHashCode()
        {
            int hashCode = 2019163721;
            hashCode = hashCode * -1521134295 + EqualityComparer<Reader>.Default.GetHashCode(Reader);
            hashCode = hashCode * -1521134295 + EqualityComparer<BookState>.Default.GetHashCode(BookState);
            hashCode = hashCode * -1521134295 + EventTime.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return "Book event - \n" + Reader + BookState.Book + "Date: " + EventTime + "\n";
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context, int index)
        {
            info.AddValue("BookEventId_" + index.ToString(), BookEventGuid);
            Reader.GetObjectData(info, context, index);
            BookState.GetObjectData(info, context, index);
            info.AddValue("EventTime_" + index.ToString(), EventTime);
        }
    }
}