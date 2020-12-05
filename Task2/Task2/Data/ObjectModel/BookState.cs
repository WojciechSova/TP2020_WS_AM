using System;
using System.Collections.Generic;

namespace Task2.Data
{
    public class BookState
    {
        public Book Book { get; set; }
        public bool Available { get; set; }
        public DateTime BuyingDate { get; set; }  

        public BookState(Book book, bool available, DateTime buyingDate)
        {
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
    }
}