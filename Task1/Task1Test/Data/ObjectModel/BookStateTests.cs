using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Task1.Data;

namespace Task1Test.Data
{
    [TestClass]
    public class BookStateTests
    {
        [TestMethod]
        public void CreateBookStateTest()
        {
            BookState bookState1 = new BookState(new Book("123-456-789", "John Janowy", "Catchy Title", "Interesting Book"), true, new DateTime(1999, 12, 15));

            Assert.AreEqual(new DateTime(1999, 12, 15), bookState1.BuyingDate);
            Assert.IsTrue(bookState1.Available);
        }

        [TestMethod]
        public void SetterBookStateTest()
        {
            BookState bookState2 = new BookState(new Book("987-654-321", "Jack Jakubowy", "Boring Title", "Casual Book"), true, new DateTime(2011, 1, 5));

            bookState2.Available = false;
            bookState2.Book.Author = "NoName";

            Assert.IsFalse(bookState2.Available);
            Assert.AreEqual("NoName", bookState2.Book.Author);
        }

        [TestMethod]
        public void HashCodeBookStateTest()
        {
            BookState bookState1 = new BookState(new Book("123-456-789", "John Janowy", "Catchy Title", "Interesting Book"), true, new DateTime(1999, 12, 15));
            BookState bookState2 = new BookState(new Book("123-456-789", "John Janowy", "Catchy Title", "Interesting Book"), true, new DateTime(1999, 12, 15));
            BookState bookState3 = new BookState(new Book("987-654-321", "Jack Jakubowy", "Boring Title", "Casual Book"), true, new DateTime(2011, 1, 5));

            Assert.AreEqual(bookState1.GetHashCode(), bookState2.GetHashCode());
            Assert.AreNotEqual(bookState3.GetHashCode(), bookState1.GetHashCode());
        }

        [TestMethod]
        public void EqualsBookStateTest()
        {
            BookState bookState1 = new BookState(new Book("123-456-789", "John Janowy", "Catchy Title", "Interesting Book"), true, new DateTime(1999, 12, 15));
            BookState bookState2 = new BookState(new Book("123-456-789", "John Janowy", "Catchy Title", "Interesting Book"), true, new DateTime(1999, 12, 15));
            BookState bookState3 = new BookState(new Book("987-654-321", "Jack Jakubowy", "Boring Title", "Casual Book"), true, new DateTime(2011, 1, 5));

            Assert.IsTrue(bookState1.Equals(bookState2));
            Assert.IsFalse(bookState1.Equals(bookState3));
        }
    }
}
