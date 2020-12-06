using Newtonsoft.Json;
using System;

namespace Task2.Data
{
    public class BookRent : BookEvent
    {
        [JsonConstructor]
        public BookRent(Reader reader, BookState bookState, DateTime dateTime) : base(reader, bookState, dateTime)
        {
            bookState.Available = false;
        }

        public BookRent(Reader reader, BookState bookState) : base(reader, bookState)
        {
            bookState.Available = false;
        }

        public BookRent(string guid, Reader reader, BookState bookState, DateTime dateTime) : base(guid, reader, bookState, dateTime)
        {
            bookState.Available = false;
        }

        public override bool Equals(object obj)
        {
            return obj is BookRent && base.Equals(obj);
        }

        public override int GetHashCode()
        {
            int hashCode = 13120312;
            return hashCode * -1521134295 + base.GetHashCode();
        }

        public override string ToString()
        {
            return "Rent - " + base.ToString();
        }
    }
}
