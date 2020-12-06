using Newtonsoft.Json;
using System;

namespace Task2.Data
{
    public class BookReturn : BookEvent
    {
        [JsonConstructor]
        public BookReturn(Reader reader, BookState bookState) : base(reader, bookState)
        {
            bookState.Available = true;
        }

        public BookReturn(Reader reader, BookState bookState, DateTime dateTime) : base(reader, bookState, dateTime)
        {
            bookState.Available = true;
        }

        public BookReturn(String guid, Reader reader, BookState bookState, DateTime dateTime) : base(guid, reader, bookState, dateTime)
        {
            
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
