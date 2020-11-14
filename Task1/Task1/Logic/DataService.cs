using System;
using System.Collections.Generic;
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

        public void DeleteBook(string Isbn)
        {
            throw new NotImplementedException();
        }

        public void DeleteReader(long PersonalId)
        {
            throw new NotImplementedException();
        }

        public void DeleteBookState(string isbn)
        {
            throw new NotImplementedException();
        }

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
    }
}