using System;

namespace Task1.Data
{
    public class Book
    {
        public string Isbn { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }



        public override string ToString()
        {
            return "Book: " + Title + ", author: " + Author;
        }

    }
}