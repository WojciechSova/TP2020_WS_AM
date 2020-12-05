using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Task2.Data;
using Tests.DataFiller;

namespace Tests.Data
{
    [TestClass]
    public class DataRepositoryTests
    {
        IDataFiller dataFiller = new ConstantFiller();
        DataContext dataContext= new DataContext();
        IDataRepository dataRepository;

        [TestInitialize()]
        public void SetUp()
        {
            dataRepository = new Task2.Data.DataRepository(dataFiller, dataContext);
        }

        #region Reader
        [TestMethod]
        public void AddReaderTest()
        {
            int listOfReadersSize = dataRepository.GetAllReaders().Count();
            dataRepository.AddReader(new Reader("Artur", "Xinski", 123456987));

            Assert.AreEqual(listOfReadersSize + 1, dataRepository.GetAllReaders().Count());
            Assert.AreEqual("Artur", dataRepository.GetReader(dataRepository.GetAllReaders().Count() - 1).Name);

        }

        [TestMethod]
        public void CannotAddTheSameReaderTest()
        {
            dataRepository.AddReader(new Reader("Artur", "Xinski", 123456987));

            Assert.ThrowsException<ArgumentException>(() => dataRepository.AddReader(new Reader("Artur", "Xinski", 123456987)));
        }

        [TestMethod]
        public void GetReaderTest()
        {
            for (int i = 0; i < dataContext.ReadersList.Count; i++)
            {
                Assert.AreEqual(dataContext.ReadersList[i].Surname, dataRepository.GetReader(i).Surname);
                Assert.AreEqual(dataContext.ReadersList[i].PersonalID, dataRepository.GetReader(i).PersonalID);
            }
        }

        [TestMethod]
        public void GetAllReadersTest()
        {
            Assert.AreEqual(dataContext.ReadersList.Count, dataRepository.GetAllReaders().Count());
        }

        [TestMethod]
        public void UpdateReaderTest()
        {
            dataRepository.UpdateReader(0, "UpdatedName", "UpdatedSurname", 7777777);

            Assert.AreEqual("UpdatedName", dataRepository.GetReader(0).Name);
            Assert.AreEqual(7777777, dataRepository.GetReader(0).PersonalID);
        }

        [TestMethod]
        public void NonExistentReaderTest()
        {
            Assert.ThrowsException<KeyNotFoundException>(() => dataRepository.UpdateReader(dataContext.ReadersList.Count() + 2, "Artur", "Xinski", 123456987));
            Assert.ThrowsException<KeyNotFoundException>(() => dataRepository.GetReader(dataContext.ReadersList.Count() + 2));
            Assert.ThrowsException<KeyNotFoundException>(() => dataRepository.DeleteReader(dataContext.ReadersList.Count() + 2));
        }

        [TestMethod]
        public void DeleteReaderTest()
        {
            Reader reader = new Reader("Artur", "Xinski", 123456987);
            dataRepository.AddReader(reader);

            int id = dataContext.ReadersList.Count();
            Assert.AreEqual(dataContext.ReadersList[id - 1], reader);


            dataRepository.DeleteReader(id - 1);
            Assert.IsFalse(dataContext.ReadersList.Contains(reader));
        }

        [TestMethod]
        public void CannotRemoveReaderWithBorrowedBook()
        {
            Reader reader = new Reader("Artur", "Xinski", 123456987);
            BookEvent rent = new BookRent(
                reader,
                new BookState(new Book("111-222-333", "Wojciech Sowa", "Life is life", "Amazing book"), true, DateTime.Now));
            dataRepository.AddReader(reader);
            dataRepository.AddEvent(rent);

            Assert.ThrowsException<InvalidOperationException>(() => dataRepository.DeleteReader(dataContext.ReadersList.Count - 1));
        }
        #endregion

        #region Book
        [TestMethod]
        public void AddBookTest()
        {
            int listOfBookSize = dataRepository.GetAllBook().Count();
            dataRepository.AddBook(new Book("111-222-333", "Wojciech Sowa", "Life is life", "Amazing book"));

            Assert.AreEqual(listOfBookSize + 1, dataRepository.GetAllBook().Count());
            Assert.AreEqual("Wojciech Sowa", dataRepository.GetBook(dataRepository.GetAllBook().Count() - 1).Author);
        }

        [TestMethod]
        public void CannotAddTheSameBookTest()
        {
            dataRepository.AddBook(new Book("111-222-333", "Wojciech Sowa", "Life is life", "Amazing book"));
            Assert.ThrowsException<ArgumentException>(() => dataRepository.AddBook(new Book("111-222-333", "Wojciech Sowa", "Life is life", "Amazing book")));
        }

        [TestMethod]
        public void GetBookTest()
        {
            int id = 0;
            foreach (Book b in dataRepository.GetAllBook())
            {
                Assert.AreEqual(dataContext.BookSet[id].Isbn, b.Isbn);
                Assert.AreEqual(dataContext.BookSet[id].Title, b.Title);
                id++;
            }
        }

        [TestMethod]
        public void GetAllBooksTest()
        {
            Assert.AreEqual(dataContext.BookSet.Count, dataRepository.GetAllBook().Count());
        }
        
        [TestMethod]
        public void UpdateBookTest()
        {
            dataRepository.AddBook(new Book("111-222-333", "Wojciech Sowa", "Life is life", "Amazing book"));
            Assert.AreEqual("111-222-333", dataRepository.GetAllBook().Last().Isbn);
            dataRepository.UpdateBook(dataContext.BookSet.Last().Key, "111-111-111", "Wojciech Sowa", "Life is life", "Amazing book");
            Assert.AreEqual("111-111-111", dataRepository.GetAllBook().Last().Isbn);
        }

        [TestMethod]
        public void NonExistentBookTest()
        {
            Assert.ThrowsException<KeyNotFoundException>(() => dataRepository.UpdateBook(dataContext.BookSet.Last().Key + 2, "111-1-11-1", "Sowa", "Life Is Life", "Amazing Book"));
            Assert.ThrowsException<KeyNotFoundException>(() => dataRepository.GetBook(dataContext.BookSet.Last().Key + 2));
            Assert.ThrowsException<KeyNotFoundException>(() => dataRepository.DeleteBook(dataContext.BookSet.Last().Key + 2));
        }

        [TestMethod]
        public void DeleteBookTest()
        {
            Book book = new Book("111-222-333", "Wojciech Sowa", "Life is life", "Amazing book");
            dataRepository.AddBook(book);

            int id = dataContext.BookSet.Last().Key;
            Assert.AreEqual(dataContext.BookSet[id], book);


            dataRepository.DeleteBook(id);
            Assert.IsFalse(dataContext.BookSet.ContainsValue(book));
        }

        [TestMethod]
        public void CannotRemoveBorrowedBookTest()
        {
            Book book = null;
            foreach (BookState bs in dataRepository.GetAllBookState())
            {
                if (!bs.Available)
                {
                    book = bs.Book;
                    break;
                }
            }
            if (book != null)
            {
                int id = dataContext.BookSet.FirstOrDefault(x => x.Value == book).Key;
                Assert.ThrowsException<InvalidOperationException>(() => dataRepository.DeleteBook(id));
            }
            else
            {
                Assert.Inconclusive("No borrowed book so cannot check if borrowed book can be deleted");
            }
        }
        #endregion

        #region BookState
        [TestMethod]
        public void AddBookStateTest()
        {
            DateTime dateTime = DateTime.Now; 
            int listOfBookStateSize = dataRepository.GetAllBookState().Count();
            dataRepository.AddBookState(new BookState(dataRepository.GetBook(0), true, dateTime));

            Assert.AreEqual(listOfBookStateSize + 1, dataRepository.GetAllBookState().Count());
            Assert.AreEqual(dateTime, dataRepository.GetBookState(dataRepository.GetAllBookState().Count() - 1).BuyingDate);
        }

        [TestMethod]
        public void GetBookStateTest()
        {
            int id = 0;
            foreach (BookState bs in dataRepository.GetAllBookState())
            {
                Assert.AreEqual(dataContext.BookStatesList[id].Book, bs.Book);
                Assert.AreEqual(dataContext.BookStatesList[id].BuyingDate, bs.BuyingDate);
                id++;
            }
        }

        [TestMethod]
        public void GetAllBookStateTest()
        {
            Assert.AreEqual(dataContext.BookStatesList.Count, dataRepository.GetAllBookState().Count());
        }

        [TestMethod]
        public void UpdateBookStateTest()
        {
            dataRepository.AddBookState(new BookState(dataRepository.GetBook(0), true, DateTime.Now));
            Assert.AreEqual(true, dataRepository.GetAllBookState().Last().Available);
            dataRepository.UpdateBookState(dataContext.BookStatesList.Count-1, dataRepository.GetBook(0), false, DateTime.Now);
            Assert.AreEqual(false, dataRepository.GetAllBookState().Last().Available);
        }
        
        [TestMethod]
        public void NonExistentBookStateTest()
        {
            Assert.ThrowsException<KeyNotFoundException>(() => dataRepository.UpdateBookState(dataContext.BookStatesList.Count + 2, dataRepository.GetBook(0), false, DateTime.Now));
            Assert.ThrowsException<KeyNotFoundException>(() => dataRepository.GetBookState(dataContext.BookStatesList.Count + 2));
            BookState bookState = new BookState(dataRepository.GetBook(0), true, DateTime.Now);
            Assert.ThrowsException<KeyNotFoundException>(() => dataRepository.DeleteBookState(bookState));
        }

        [TestMethod]
        public void DeleteBookStateTest()
        {
            BookState bookState = new BookState(dataRepository.GetBook(3), true, DateTime.Now);
            dataRepository.AddBookState(bookState);

            int id = dataContext.BookStatesList.Count - 1;
            Assert.AreEqual(dataContext.BookStatesList.ElementAt(id), bookState);


            dataRepository.DeleteBookState(bookState);
            Assert.IsFalse(dataContext.BookStatesList.Contains(bookState));
        }

        [TestMethod]
        public void CannotRemoveBookStateWhoseBookIsBorrowedTest()
        {
            BookState bookState = null;
            foreach (BookState bs in dataRepository.GetAllBookState())
            {
                if (!bs.Available)
                {
                    bookState = bs;
                    break;
                }
            }
            if (bookState != null)
            {
                Assert.ThrowsException<InvalidOperationException>(() => dataRepository.DeleteBookState(bookState));
            }
            else
            {
                Assert.Inconclusive("No state with borrowed book so cannot check if state with borrowed book can be deleted");
            }
        }
        #endregion

        #region BookEvent
        [TestMethod]
        public void AddEventTest()
        {
            int listOfBookEventsSize = dataRepository.GetAllBookEvent().Count();
            dataRepository.AddEvent(new BookRent(
                new Reader("Artur", "Xinski", 123456987), 
                new BookState(new Book("111-222-333", "Wojciech Sowa", "Life is life", "Amazing book"), false, new DateTime(1999, 12, 12))));

            Assert.AreEqual(listOfBookEventsSize + 1, dataRepository.GetAllBookEvent().Count());
            Assert.AreEqual("Wojciech Sowa", dataRepository.GetBookEvent(dataRepository.GetAllBookEvent().Count() - 1).BookState.Book.Author);

        }

        [TestMethod]
        public void GetBookEventTest()
        {
            for (int i = 0; i < dataContext.BookEvents.Count; i++)
            {
                Assert.IsTrue(dataContext.BookEvents[i].Equals(dataRepository.GetBookEvent(i)));
            }
        }

        [TestMethod]
        public void GetAllBookEventsTest()
        {
            Assert.AreEqual(dataContext.BookEvents.Count, dataRepository.GetAllBookEvent().Count());
        }

        [TestMethod]
        public void UpdateBookEventTest()
        {
            dataRepository.UpdateBookEvent(0, 
                new Reader("Artur", "Xinski", 123456987), 
                new BookState(new Book("111-222-333", "Wojciech Sowa", "Life is life", "Amazing book"), false, new DateTime(1999, 12, 12)), 
                new DateTime(2010, 7, 8));

            Assert.AreEqual(123456987, dataRepository.GetBookEvent(0).Reader.PersonalID);
            Assert.IsFalse(dataRepository.GetBookEvent(0).BookState.Available);
        }

        [TestMethod]
        public void NonExistentBookEventTest()
        {
            Assert.ThrowsException<KeyNotFoundException>(() => dataRepository.UpdateBookEvent(
                dataContext.BookEvents.Count() + 2,
                new Reader("Artur", "Xinski", 123456987),
                new BookState(new Book("111-222-333", "Wojciech Sowa", "Life is life", "Amazing book"), false, new DateTime(1999, 12, 12)),
                new DateTime(2010, 7, 8)));

            Assert.ThrowsException<KeyNotFoundException>(() => dataRepository.GetBookEvent(
                dataContext.BookEvents.Count() + 2));

            Assert.ThrowsException<KeyNotFoundException>(() => dataRepository.DeleteBookEvent(
                dataRepository.GetBookEvent(dataContext.BookEvents.Count() + 2)));
        }

        [TestMethod]
        public void DeleteBookEventTest()
        {
            int listOfBookEventSize = dataRepository.GetAllBookEvent().Count();

            dataRepository.DeleteBookEvent(dataContext.BookEvents[0]);

            Assert.AreEqual(listOfBookEventSize - 1, dataRepository.GetAllBookEvent().Count());
        }
        #endregion
    }
}
