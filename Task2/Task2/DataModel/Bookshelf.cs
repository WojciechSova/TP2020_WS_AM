using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Task2.DataModel
{
    class Bookshelf : ISerializable
    {
        private List<Book> Books { get; set; }
        private BookGenres[] BookGenres { get; set; }

        public Bookshelf(List<Book> books, BookGenres[] bookGenres)
        {
            Books = books;
            BookGenres = bookGenres;
        }

        public Bookshelf(SerializationInfo serializationInfo)
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
                info.AddValue("Boook" + i.ToString(), Books[i]);

            }
            for (int i = 0; i < BookGenres.Length; i++)
            {
                info.AddValue("BookGenres" + i.ToString(), BookGenres[i]);
            }
        }
    }
}
