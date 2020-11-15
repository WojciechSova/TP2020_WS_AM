using System;
using System.Collections.Generic;
using System.Linq;
using Task1.Data;

namespace Task1.Logic
{
    public class DataService : IDataService
    {
        private IDataRepository IDataRepository;

        public DataService(IDataRepository iDataRepository)
        {
            IDataRepository = iDataRepository;
        }
        #region Adding
        public void AddBook(string Isbn, string Author, string Title, string Description)
        {
            Book book = new Book(Isbn, Author, Title, Description);
            IDataRepository.AddBook(book);
        }

        public void AddReader(string Name, string Surname, long PersonalId)
        {
            Reader reader = new Reader(Name, Surname, PersonalId);
            IDataRepository.AddReader(reader);
        }

        public void AddBookState(Book Book, bool Available, DateTime BuyingTime)
        {
            BookState bookState = new BookState(Book, Available, BuyingTime);
            IDataRepository.AddBookState(bookState);
        }
        #endregion

        #region Deleting
        public void DeleteBook(int id)
        {
            IDataRepository.DeleteBook(id);
        }

        public void DeleteReader(int index)
        {
            IDataRepository.DeleteReader(index);
        }

        public void DeleteBookState(BookState bookState)
        {
            IDataRepository.DeleteBookState(bookState);
        }
        #endregion

        #region Rent and Return
        public void RentBook(Reader reader, BookState bookState)
        {
            if (bookState.Available)
            {
                IDataRepository.AddEvent(new BookRent(reader, bookState, DateTime.Now));
                return;
            }
            throw new InvalidOperationException("Cannot rent this book");
        }

        public void ReturnBook(Reader reader, BookState bookState)
        {
            Reader reader1 = IDataRepository.GetAllBookEvent().ToList().Find(r => r.BookState == bookState).Reader;
            if (!bookState.Available && reader == reader1)
            {
                IDataRepository.AddEvent(new BookReturn(reader, bookState, DateTime.Now));
                return;
            }
            throw new InvalidOperationException("Cannot return this book");
        }
        #endregion

        #region Getters
        public IEnumerable<BookEvent> GetAllReaderEvents(Reader reader)
        {
            List<BookEvent> list = new List<BookEvent>();
            foreach (BookEvent be in IDataRepository.GetAllBookEvent())
            {
                if (be.Reader == reader)
                {
                    list.Add(be);
                }
            }
            return list;
        }

        public IEnumerable<BookEvent> GetAllBookEventsBetweenDates(DateTime start, DateTime end)
        {
            List<BookEvent> list = new List<BookEvent>();
            foreach (BookEvent e in IDataRepository.GetAllBookEvent())
            {
                if (e.EventTime.CompareTo(start) >= 0 && e.EventTime.CompareTo(end) <= 0)
                {
                    list.Add(e);
                }
            }
            return list;
        }
        #endregion

        #region Enumerating
        public IEnumerable<Book> GetAllBooks()
        {
            return IDataRepository.GetAllBook();
        }

        public IEnumerable<Reader> GetAllReaders()
        {
            return IDataRepository.GetAllReaders();
        }

        public IEnumerable<BookState> GetAllBookStates()
        {
            return IDataRepository.GetAllBookState();
        }

        public IEnumerable<BookEvent> GetAllBookEvents()
        {
            return IDataRepository.GetAllBookEvent();
        }
        #endregion

        
    }
}