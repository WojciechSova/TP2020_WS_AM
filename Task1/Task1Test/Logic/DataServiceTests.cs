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
        private IDataFiller dataFiller = new ConstantFiller();
        private DataContext dataContext = new DataContext();
        private IDataRepository dataRepository;
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

            dataService.RentBook(reader1, bookState2);
            dataService.RentBook(reader1, bookState2);

            int amountOfRentedBooks1 = dataService.GetAllReaderEvents(reader1).OfType<BookRent>().Count();

            dataService.ReturnBook(reader1, bookState1);

            Assert.AreEqual(amountOfRentedBooks1 - 1, dataService.GetAllReaderEvents(reader1).OfType<BookRent>().Count());
            //Assert.ThrowsException<InvalidOperationException>(() => dataService.ReturnBook(reader1, bookState1));

        }
        #endregion
    }
}
