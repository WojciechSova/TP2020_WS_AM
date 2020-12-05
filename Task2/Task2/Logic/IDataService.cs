using System;
using System.Collections.Generic;
using Task2.Data;

namespace Task2.Logic
{
    public interface IDataService
    {
        #region Adding
        void AddBook(string isbn, string author, string title, string description);
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
        IEnumerable<BookEvent> GetAllReaderEvents(Reader reader);
        IEnumerable<BookEvent> GetAllBookEventsBetweenDates(DateTime start, DateTime end);

        #endregion

        #region Enumerating
        IEnumerable<Book> GetAllBooks();
        IEnumerable<Reader> GetAllReaders();
        IEnumerable<BookState> GetAllBookStates();
        IEnumerable<BookEvent> GetAllBookEvents();
        #endregion

    }
}
