using System;
using System.Collections.Generic;

namespace Task1.Data
{
    public class DataRepository : IDataRepository
    {
        private DataContext DataContext;

        public DataRepository()
        {
            DataContext = new DataContext();
        }

        public void AddBook(Book book)
        {
            if(DataContext.BookSet.ContainsKey(book.Isbn))
            {
                throw new ArgumentException("Book with provided isbn already exists");
            }

            
            DataContext.BookSet.Add(book.Isbn, book.Title);
        }

        public void AddBookState(BookState bookState)
        {
            DataContext.BookStatesList.Add(bookState);
        }

        public void AddEvent(BookEvent bookEvent)
        {
            DataContext.BookEvents.Add(bookEvent);
        }

        public void AddReader(Reader reader)
        {
            if (DataContext.ReadersList.Contains(reader))
            {
                throw new ArgumentException("Such person already exists.");
            }

            DataContext.ReadersList.Add(reader);
        }

        public void DeleteBook(Book book)
        {
            foreach (BookState state in DataContext.BookStatesList)
            {
                if (state.Book.Equals(book) && !state.Available)
                {
                    throw new ArgumentException("Cannot remove borrowed book.")
                }
            }
            foreach (string isbn in DataContext.BookSet.Keys)
            {
                if (isbn.Equals(book.Isbn))
                    DataContext.BookSet.Remove(isbn);
                break;
            }
        }

        public void DeleteBookEvent(BookEvent bookEvent)
        {
            if (DataContext.BookEvents.Contains(bookEvent))
            {
                DataContext.BookEvents.Remove(bookEvent);
            }
        }

        public void DeleteBookState(BookState bookState)
        {
            throw new NotImplementedException();
        }

        public void DeleteReader(Reader reader)
        {
            foreach (BookEvent b in DataContext.BookEvents)
            {
                if (b.Reader == reader)
                {
                    throw new ArgumentException("Cannot remove client who still has a borrowed book.");
                }
            }
            if (DataContext.ReadersList.Contains(reader))
            {
                DataContext.ReadersList.Remove(reader);
            }
        }

        public IEnumerable<Book> GetAllBook()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookEvent> GetAllBookEvent()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookState> GetAllBookState()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Reader> GetAllReaders()
        {
            throw new NotImplementedException();
        }

        public Book GetBook(int id)
        {
            throw new NotImplementedException();
        }

        public BookEvent GetBookEvent(int id)
        {
            throw new NotImplementedException();
        }

        public BookState GetBookState(int id)
        {
            throw new NotImplementedException();
        }

        public Reader GetReader(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateBook(int id, string isbn, string author, string title, string description)
        {
            throw new NotImplementedException();
        }

        public void UpdateBookEvent(int id, Reader reader, BookState bookState, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public void UpdateBookState(int id, Book book, bool available, DateTime buyingDate)
        {
            throw new NotImplementedException();
        }

        public void UpdateReader(int id, string name, string surname, long personalID)
        {
            throw new NotImplementedException();
        }
    }
}
