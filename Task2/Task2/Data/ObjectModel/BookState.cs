using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Task2.Data
{
    public class BookState : ISerializable
    {
        public Guid BookStateGuid { get; set; }
        public Book Book { get; set; }
        public bool Available { get; set; }
        public DateTime BuyingDate { get; set; }  

        public BookState(Book book, bool available, DateTime buyingDate)
        {
            this.BookStateGuid = Guid.NewGuid();
            Book = book;
            Available = available;
            BuyingDate = buyingDate;
        }

        public override bool Equals(object obj)
        {
            return obj is BookState state &&
                   EqualityComparer<Book>.Default.Equals(Book, state.Book) &&
                   Available == state.Available &&
                   BuyingDate == state.BuyingDate;
        }

        public override int GetHashCode()
        {
            int hashCode = 911311761;
            hashCode = hashCode * -1521134295 + EqualityComparer<Book>.Default.GetHashCode(Book);
            hashCode = hashCode * -1521134295 + Available.GetHashCode();
            hashCode = hashCode * -1521134295 + BuyingDate.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return Book + " is available: " + Available + " Buying date: " + BuyingDate + "\n";
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("BookStateId", BookStateGuid);
            Book.GetObjectData(info, context);
            info.AddValue("Available", Available);
            info.AddValue("BuyingDate", BuyingDate);
        }
    }
}