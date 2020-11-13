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
        void AddState(Book Book, bool Available, DateTime buyingTime);
        #endregion

        #region Deleting
        void DeleteBook(string Isbn);
        void DeleteReader(long PersonalId);
        void DeleteState(string isbn);
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
