using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Task2.Data;
using Tests.DataFiller;

namespace Tests.Data
{
    [TestClass]
    public class BookEventTests
    {
        BookEvent bookEvent1;
        BookEvent bookEvent2;
        DataContext dataContext = new DataContext();
        [TestInitialize()]
        public void SetUp()
        {
            IDataFiller dataFiller = new ConstantFiller();
            IDataRepository dataRepository = new DataRepository(dataFiller, dataContext);
            bookEvent1 = new BookRent(dataContext.ReadersList[3], dataContext.BookStatesList[0], new DateTime(2017, 11, 14));
            bookEvent2 = new BookReturn(dataContext.ReadersList[3], dataContext.BookStatesList[0], new DateTime(2017, 11, 14));
        }

        [TestMethod]
        public void ConstructorBookEventTest()
        {
            Assert.AreEqual(dataContext.ReadersList[3], bookEvent1.Reader);
            Assert.AreEqual(dataContext.BookStatesList[0], bookEvent1.BookState);
            Assert.AreEqual(new DateTime(2017, 11, 14), bookEvent1.EventTime);
        }

        [TestMethod]
        public void SetterBookEventTest()
        {
            Reader reader = new Reader("Wojciech", "Sowa", 123456789);
            Assert.AreEqual(dataContext.ReadersList[3], bookEvent1.Reader);
            bookEvent1.Reader = reader;
            Assert.AreEqual(reader, bookEvent1.Reader);


            Assert.AreEqual(new DateTime(2017, 11, 14), bookEvent1.EventTime);
            bookEvent1.EventTime = new DateTime(2011, 12, 2);
            Assert.AreEqual(new DateTime(2011, 12, 2), bookEvent1.EventTime);
        }

        [TestMethod]
        public void EqualsBookEventTest()
        {
            Assert.IsFalse(bookEvent1.Equals(bookEvent2));
            Assert.AreNotEqual(bookEvent1, bookEvent2);

            bookEvent2 = new BookRent(dataContext.ReadersList[3], dataContext.BookStatesList[0], new DateTime(2017, 11, 14));
            Assert.IsTrue(bookEvent1.Equals(bookEvent2));
            Assert.AreEqual(bookEvent1, bookEvent2);
        }

        [TestMethod]
        public void HashCodeBookEventTest()
        {
            Assert.AreNotEqual(bookEvent1.GetHashCode(), bookEvent2.GetHashCode());

            bookEvent2 = new BookRent(dataContext.ReadersList[3], dataContext.BookStatesList[0], new DateTime(2017, 11, 14));

            Assert.AreEqual(bookEvent1.GetHashCode(), bookEvent2.GetHashCode());
        }
    }
}
