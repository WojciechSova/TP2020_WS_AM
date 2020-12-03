using System;

namespace Task1.Data
{
    public class BookReturn : BookEvent
    {
        public BookReturn(Reader reader, BookState bookState) : base(reader, bookState)
        {
            bookState.Available = true;
        }

        public BookReturn(Reader reader, BookState bookState, DateTime dateTime) : base(reader, bookState, dateTime)
        {
            bookState.Available = true;
        }

        public override bool Equals(object obj)
        {
            return obj is BookReturn && base.Equals(obj);
        }

        public override int GetHashCode()
        {
            int hashCode = 32190453;
            return hashCode * -1521134295 + base.GetHashCode();
        }

        public override string ToString()
        {
            return "Return - " + base.ToString();
        }
    }
}
