﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Task2.DataModel
{
    public class Bookshelf : ISerializable
    {
        public List<Book> Books { get; set; }
        public BookGenres[] BookGenres { get; set; }

        [JsonConstructor]
        public Bookshelf(List<Book> books, BookGenres[] bookGenres)
        {
            Books = books;
            BookGenres = bookGenres;
        }

        public Bookshelf(SerializationInfo serializationInfo, StreamingContext context)
        {
            int BooksNumber = serializationInfo.GetInt32("BooksNumber");
            int GenresNumber = serializationInfo.GetInt32("GenresNumber");

            Books = new List<Book>();
            List<BookGenres> BookGenresTmp = new List<BookGenres>();

            for (int i = 0; i < BooksNumber; i++)
            {
                Books.Add((Book) serializationInfo.GetValue("Book" + i.ToString(), typeof(Book)));
            }

            for (int i = 0; i < GenresNumber; i++)
            {
                BookGenresTmp.Add((BookGenres) serializationInfo.GetValue("BookGenres" + i.ToString(), typeof(BookGenres)));
            }
            BookGenres = BookGenresTmp.ToArray();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("BooksNumber", Books.Count);
            info.AddValue("GenresNumber", BookGenres.Length);

            for (int i = 0; i < Books.Count; i++)
            {
                info.AddValue("Book" + i.ToString(), Books[i]);

            }
            for (int i = 0; i < BookGenres.Length; i++)
            {
                info.AddValue("BookGenres" + i.ToString(), BookGenres[i]);
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Bookshelf bookshelf &&
                   EqualityComparer<List<Book>>.Default.Equals(Books, bookshelf.Books) &&
                   EqualityComparer<BookGenres[]>.Default.Equals(BookGenres, bookshelf.BookGenres);
        }
    }
}
