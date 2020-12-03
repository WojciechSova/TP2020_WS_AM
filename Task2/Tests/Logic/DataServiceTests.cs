using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task1.Data;
using Task1.Logic;
using Task1Test.DataFiller;
using System.Linq;
using System;

namespace Task1Test.Logic
{
    [TestClass]
    public class DataServiceTests
    {
        private IDataFiller dataFiller = new RandomFiller();
        private DataContext dataContext = new DataContext();
        private IDataService dataService;

        [TestInitialize()]
        public void SetUp()
        {
            IDataRepository dataRepository = new DataRepository(dataFiller, dataContext);
            dataService = new DataService(dataRepository);
        }

        #region Book
        [TestMethod]
        public void RentBookTest()
        {
            Reader reader1 = new Reader("Artur", "Xinski", 123456987);
            Book book1 = new Book("111-222-333", "Wojciech Sowa", "Life is life", "Amazing book");
            Book book2 = new Book("156-879-654", "John Tolkien", "LOTR", "Must have");
            BookState bookState1 = new BookState(book1, true, new System.DateTime(2011, 11, 11));
            BookState bookState2 = new BookState(book2, false, new System.DateTime(1998, 8, 7));


            int amountOfRentedBooks1 = dataService.GetAllReaderEvents(reader1).OfType<BookRent>().Count();

            dataService.RentBook(reader1, bookState1);

            Assert.ThrowsException<InvalidOperationException>(() => dataService.RentBook(reader1, bookState2));
            Assert.IsFalse(bookState1.Available);
            Assert.AreEqual(amountOfRentedBooks1 + 1, dataService.GetAllReaderEvents(reader1).OfType<BookRent>().Count());
        }

        [TestMethod]
        public void ReturnBookTest()
        {
            Reader reader1 = new Reader("Artur", "Xinski", 123456987);
            Book book1 = new Book("111-222-333", "Wojciech Sowa", "Life is life", "Amazing book");
            Book book2 = new Book("156-879-654", "John Tolkien", "LOTR", "Must have");
            Book book3 = new Book("111-555-999", "Roneald Reul", "RTOL", "Not necessarily");
            BookState bookState1 = new BookState(book1, true, new System.DateTime(2011, 11, 11));
            BookState bookState2 = new BookState(book2, true, new System.DateTime(1998, 8, 7));

            dataService.RentBook(reader1, bookState1);
            dataService.RentBook(reader1, bookState2);

            int amountOfReturnedBooks = dataService.GetAllReaderEvents(reader1).OfType<BookReturn>().Count();

            dataService.ReturnBook(reader1, bookState1);

            Assert.IsTrue(bookState1.Available);
            Assert.IsFalse(bookState2.Available);
            Assert.AreEqual(amountOfReturnedBooks + 1, dataService.GetAllReaderEvents(reader1).OfType<BookReturn>().Count());
            Assert.ThrowsException<InvalidOperationException>(() => dataService.ReturnBook(reader1, bookState1));

        }
        #endregion

        #region Getters
        [TestMethod]
        public void GetAllReaderEventsTest()
        {
            Reader reader1 = new Reader("Artur", "Xinski", 123456987);
            Book book1 = new Book("111-222-333", "Wojciech Sowa", "Life is life", "Amazing book");
            Book book2 = new Book("156-879-654", "John Tolkien", "LOTR", "Must have");
            BookState bookState1 = new BookState(book1, true, new System.DateTime(2011, 11, 11));
            BookState bookState2 = new BookState(book2, true, new System.DateTime(1998, 8, 7));

            dataService.RentBook(reader1, bookState1);
            dataService.RentBook(reader1, bookState2);

            Assert.AreEqual(2, dataService.GetAllReaderEvents(reader1).Count());
        }

        [TestMethod]
        public void GetAllBookEventsBetweenDatesTest()
        {
            Reader reader = new Reader("Artur", "Xinski", 123456987);
            Book book = new Book("111-222-333", "Wojciech Sowa", "Life is life", "Amazing book");
            BookState bookState1 = new BookState(book, true, new System.DateTime(2011, 11, 11));
            BookState bookState2 = new BookState(book, true, new System.DateTime(1998, 8, 7));

            DateTime firstDate = DateTime.Now;

            Assert.AreEqual(0, dataService.GetAllBookEventsBetweenDates(firstDate, DateTime.Now).Count());

            dataService.RentBook(reader, bookState1);
            Assert.AreEqual(1, dataService.GetAllBookEventsBetweenDates(firstDate, DateTime.Now).Count());

            dataService.RentBook(reader, bookState2);
            Assert.AreEqual(2, dataService.GetAllBookEventsBetweenDates(firstDate, DateTime.Now).Count());

            dataService.ReturnBook(reader, bookState1);
            Assert.AreEqual(3, dataService.GetAllBookEventsBetweenDates(firstDate, DateTime.Now).Count());
        }
        #endregion
    }
}
