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

        public DataRepository(IDataFiller dataFiller, DataContext dataContext)
        {
            DataContext = dataContext;
            dataFiller.Fill(dataContext);
        }

        #region Reader
        public void AddReader(Reader reader)
        {
            if (DataContext.ReadersList.Contains(reader))
            {
                throw new ArgumentException("Such person already exists.");
            }

            DataContext.ReadersList.Add(reader);
        }

        public Reader GetReader(int id)
        {
            if (DataContext.ReadersList.ElementAtOrDefault(id) != null)
            {
                return DataContext.ReadersList[id];
            }
            throw new KeyNotFoundException("Reader id: " + id + " do not exist.");
        }
        public IEnumerable<Reader> GetAllReaders()
        {
            return DataContext.ReadersList;
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
            throw new KeyNotFoundException("Reader id: " + id + " do not exist.");
        }

        public void DeleteReader(int index)
        {
            if (DataContext.ReadersList.ElementAtOrDefault(index) == null)
            {
                throw new KeyNotFoundException("There's no reader at index: " + index);
            }
            Reader reader = DataContext.ReadersList[index];
            foreach (BookEvent b in DataContext.BookEvents)
            {
                if (b.Reader == reader)
                {
                    throw new InvalidOperationException("Cannot remove client who still has a borrowed book.");
                }
            }
            if (DataContext.ReadersList.Contains(reader))
            {
                DataContext.ReadersList.Remove(reader);
            }
        }
        #endregion

        #region Book
        public void AddBook(Book book)
        {
            if (DataContext.BookSet.ContainsValue(book))
            {
                throw new ArgumentException("Book already exists.");
            }


            DataContext.BookSet.Add(DataContext.BookSet.Count, book);
        }

        public Book GetBook(int id)
        {
            if (!DataContext.BookSet.ContainsKey(id))
            {
                throw new KeyNotFoundException("Book id: " + id + " does not exist.");
            }
            return DataContext.BookSet[id];
        }

        public IEnumerable<Book> GetAllBook()
        {
            return DataContext.BookSet.Values;
        }

        public void UpdateBook(int id, string isbn, string author, string title, string description)
        {
             if (DataContext.BookSet.ContainsKey(id))
            {
                DataContext.BookSet[id] = new Book(isbn, author, title, description);
                return;
            }
            throw new KeyNotFoundException("Book id: " + id + " does not exist.");
        }

        public void DeleteBook(int id)
        {
            if (!DataContext.BookSet.ContainsKey(id))
            {
                throw new KeyNotFoundException("Such book does not exist.");
            }
            Book book = DataContext.BookSet[id];
            foreach (BookState state in DataContext.BookStatesList)
            {
                if (state.Book.Equals(book) && !state.Available)
                {
                    throw new InvalidOperationException("Cannot remove borrowed book.");
                }
            }
            DataContext.BookSet.Remove(id);
        }

        #endregion

        #region BookState
        public void AddBookState(BookState bookState)
        {
            DataContext.BookStatesList.Add(bookState);
        }
        public BookState GetBookState(int id)
        {
            if (DataContext.BookStatesList.ElementAtOrDefault(id) != null)
            {
                return DataContext.BookStatesList[id];
            }
            throw new KeyNotFoundException("State id: " + id + " does not exist.");
        }
        public IEnumerable<BookState> GetAllBookState()
        {
            return DataContext.BookStatesList;
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
            throw new KeyNotFoundException("State id: " + id + " does not exist.");
        }
        public void DeleteBookState(BookState bookState)
        {
            if (DataContext.BookStatesList.Contains(bookState))
            {
                if (bookState.Available)
                {
                    DataContext.BookStatesList.Remove(bookState);
                }
                throw new InvalidOperationException("Cannot remove BookState whose book is unavailable.");
            }

        }
        #endregion

        #region BookEvent
        public void AddEvent(BookEvent bookEvent)
        {
            DataContext.BookEvents.Add(bookEvent);
        }
        public BookEvent GetBookEvent(int id)
        {
            if (DataContext.BookEvents.ElementAtOrDefault(id) != null)
            {
                return DataContext.BookEvents[id];
            }
            throw new KeyNotFoundException("Event id: " + id + " does not exist.");
        }
        public IEnumerable<BookEvent> GetAllBookEvent()
        {
            return DataContext.BookEvents;
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
            throw new KeyNotFoundException("Event id: " + id + " does not exist.");
        }
        public void DeleteBookEvent(BookEvent bookEvent)
        {
            if (!DataContext.BookEvents.Contains(bookEvent))
            {
                throw new KeyNotFoundException("BookEvent does not exist.");
            }
            DataContext.BookEvents.Remove(bookEvent);
        }
        #endregion
    }
}
