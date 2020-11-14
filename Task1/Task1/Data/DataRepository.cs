using System;
using System.Collections.Generic;
using System.Linq;

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
                    throw new ArgumentException("Cannot remove borrowed book.");
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
            return DataContext.BookEvents;
        }

        public IEnumerable<BookState> GetAllBookState()
        {
            return DataContext.BookStatesList;
        }

        public IEnumerable<Reader> GetAllReaders()
        {
            return DataContext.ReadersList;
        }

        public Book GetBook(int id)
        {
            throw new NotImplementedException();
        }

        public BookEvent GetBookEvent(int id)
        {
            if(DataContext.BookEvents.ElementAtOrDefault(id) != null)
            {
                return DataContext.BookEvents[id];
            }
            throw new KeyNotFoundException("Event id: " + id + " does not exist");
        }

        public BookState GetBookState(int id)
        {
            if (DataContext.BookStatesList.ElementAtOrDefault(id) != null)
            {
                return DataContext.BookStatesList[id];
            }
            throw new KeyNotFoundException("State id: " + id + " does not exist");
        }

        public Reader GetReader(int id)
        {
            if (DataContext.ReadersList.ElementAtOrDefault(id) != null)
            {
                return DataContext.ReadersList[id];
            }
            throw new KeyNotFoundException("Reader id: " + id + " does not exist");
        }

        public void UpdateBook(int id, string isbn, string author, string title, string description)
        {
            if (DataContext.BookSet.ContainsKey(id)
            {
                DataContext.BookSet[id] = new Book(isbn, author, title, description);
                return;
            }
            throw new KeyNotFoundException("Boook id: " + id + " does not exist");
        }

        public void UpdateBookEvent(int id, Reader reader, BookState bookState, DateTime dateTime)
        {
            if (DataContext.BookEvents.ElementAtOrDefault(id) != null)
            {
                DataContext.BookEvents[id].Reader = reader;
                DataContext.BookEvents[id].BookState = bookState;
                DataContext.BookEvents[id].EventTime = dateTime;
                return;
            }
            throw new KeyNotFoundException("Event id: " + id + " does not exist");
        }

        public void UpdateBookState(int id, Book book, bool available, DateTime buyingDate)
        {
            if (DataContext.BookStatesList.ElementAtOrDefault(id) != null)
            {
                DataContext.BookStatesList[id].Book = book;
                DataContext.BookStatesList[id].Available = available;
                DataContext.BookStatesList[id].BuyingDate = buyingDate;
                return;
            }
            throw new KeyNotFoundException("State id: " + id + " does not exist");
        }

        public void UpdateReader(int id, string name, string surname, long personalID)
        {
            if (DataContext.ReadersList.ElementAtOrDefault(id) != null)
            {
                DataContext.ReadersList[id].Name = name;
                DataContext.ReadersList[id].Surname = surname;
                DataContext.ReadersList[id].PersonalID = personalID;
                return;
            }
            throw new KeyNotFoundException("Reader id: " + id + " does not exist");
        }
    }
}
