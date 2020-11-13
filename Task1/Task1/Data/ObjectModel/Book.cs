using System;

namespace Task1.Data
{
    public class Book
    {
        public string Isbn { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Book(string isbn, string author, string title, string description)
        {
            Isbn = isbn;
            Author = author;
            Title = title;
            Description = description;
        }

        public override string ToString()
        {
            return "Book: " + Title + ", author: " + Author;
        }

    }
}