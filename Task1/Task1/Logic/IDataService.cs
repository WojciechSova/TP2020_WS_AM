using System;
using System.Collections.Generic;
using System.Text;
using Task1.Data;

namespace Task1.Logic
{
    public interface IDataService
    {
        #region Adding
        void AddBook(string Isbn, string Author, string Title, string Description);
        void AddReader(string Name, string Surname, long PersonalId);
        void AddBookState(Book Book, bool Available, DateTime BuyingTime);
        #endregion

        #region Deleting
        void DeleteBook(int id);
        void DeleteReader(int index);
        void DeleteBookState(BookState bookState);
        #endregion

        #region Rent and Return
        void RentBook(Reader reader, BookState bookState);
        void ReturnBook(Reader reader, BookState bookState);
        #endregion

        #region Getters

        #endregion

        #region Enumerating
        IEnumerable<Book> GetAllBooks();
        IEnumerable<Reader> GetAllReaders();
        IEnumerable<BookState> GetAllBookStates();
        IEnumerable<BookEvent> GetAllBookEvents();
        #endregion

    }
}
