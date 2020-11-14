using System;
using System.Collections.Generic;

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

        public override bool Equals(object obj)
        {
            return obj is Book book &&
                   Isbn == book.Isbn &&
                   Author == book.Author &&
                   Title == book.Title &&
                   Description == book.Description;
        }

        public override int GetHashCode()
        {
            int hashCode = 1976838697;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Isbn);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Author);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            return hashCode;
        }

        public override string ToString()
        {
            return "Book: " + Title + ", author: " + Author + "\n";
        }
    }
}