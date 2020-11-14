using System;

namespace Task1.Data
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
    }
}