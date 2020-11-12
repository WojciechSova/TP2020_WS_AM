using System;

namespace Task1.Data
{
    class BookState
    {
        public bool Available { get; set; }
        public DateTime BuyingDate { get; }  

        public BookState(bool available, DateTime buyingDate)
        {
            Available = available;
            BuyingDate = buyingDate;
        }
    }
}