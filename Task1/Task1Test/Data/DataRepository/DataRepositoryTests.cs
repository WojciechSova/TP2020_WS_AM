﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Task1.Data;
using Task1Test.DataFiller;

namespace Task1Test.Data.DataRepository
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
            dataRepository = new Task1.Data.DataRepository(dataFiller, dataContext);
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
            for (int i = 0; i < 7; i++)
            {
                Assert.AreEqual("Sodaj" + i.ToString(), dataRepository.GetReader(i).Surname);
                Assert.AreEqual(1000000 + i, dataRepository.GetReader(i).PersonalID);
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
            dataRepository.UpdateReader(5, "UpdatedName", "UpdatedSurname", 7777777);

            Assert.AreEqual("UpdatedName", dataRepository.GetReader(5).Name);
            Assert.AreEqual(7777777, dataRepository.GetReader(5).PersonalID);
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
            int listOfReadersSize = dataRepository.GetAllReaders().Count();

            dataRepository.DeleteReader(1);

            Assert.AreEqual(listOfReadersSize - 1, dataRepository.GetAllReaders().Count());
        }

        [TestMethod]
        public void CannotRemoveReaderWithBorrowedBook()
        {
            Assert.ThrowsException<InvalidOperationException>(() => dataRepository.DeleteReader(5));
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
                Assert.AreEqual("123-456-" + id, b.Isbn);
                Assert.AreEqual("Catchy Title " + id, b.Title);
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
            Assert.ThrowsException<KeyNotFoundException>(() => dataRepository.UpdateBook(10, "111-1-11-1", "Sowa", "Life Is Life", "Amazing Book"));
            Assert.ThrowsException<KeyNotFoundException>(() => dataRepository.GetBook(10));
            Assert.ThrowsException<KeyNotFoundException>(() => dataRepository.DeleteBook(10));
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
            Assert.ThrowsException<InvalidOperationException>(() => dataRepository.DeleteBook(0));
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
            for (int i = 0; i < 7; i++)
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
