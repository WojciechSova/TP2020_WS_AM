using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Task2.Data  
{
    public class Book
    {

        public Guid BookGuid { get; set; }
        public string Isbn { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [JsonConstructor]
        public Book(string isbn, string author, string title, string description)
        {
            this.BookGuid = Guid.NewGuid();
            Isbn = isbn;
            Author = author;
            Title = title;
            Description = description;
        }

        public Book(string guid, string isbn, string author, string title, string description)
        {
            this.BookGuid = Guid.Parse(guid);
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
            return "Book: " + Title + ", Author: " + Author + "\n";
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context, int index)
        {
            info.AddValue("BookId_" + index.ToString() + "_", BookGuid);
            info.AddValue("Isbn_" + index.ToString() + "_", Isbn);
            info.AddValue("Title_" + index.ToString() + "_", Title);
            info.AddValue("Author_" + index.ToString() + "_", Author);
            info.AddValue("Description_" + index.ToString() + "_", Description);
        }
    }
}