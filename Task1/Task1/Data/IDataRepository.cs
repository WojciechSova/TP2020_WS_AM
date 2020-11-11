﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Task1.Data
{
    public interface IDataRepository
    {
        #region Reader
        void AddReader(Reader reader);
        Reader GetReader(int id);
        IEnumerable<Reader> GetAllReaders();
        void UpdateReader(int id, string name, string surname, long personalID);
        void DeleteReader(Reader reader);
        #endregion

        #region Book
        void AddBook(Book book);
        Book GetBook(int id);
        IEnumerable<Book> GetAllBook();
        void UpdateBook(int id, string isbn, string author, string title, string description);
        void DeleteBook(Book book);
        #endregion

        #region BookState
        void AddBookState(BookState bookState);
        BookState GetBookState(int id);
        IEnumerable<BookState> GetAllBookState();
        void UpdateBookState(int id, Book book, bool state);
        void DeleteBookState(BookState bookState);
        #endregion

        #region BookEvent
        void AddEvent(BookEvent bookEvent);
        BookEvent GetBookEvent(int id);
        IEnumerable<BookEvent> GetAllBookEvent();
        void UpdateBookEvent(int id, Reader reader, BookState bookState);
        void DeleteBookEvent(BookEvent bookEvent);
        #endregion
    }
}
