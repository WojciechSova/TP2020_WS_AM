using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task2.Data;

namespace Tests.Data
{
    [TestClass]
    public class BookTests
    {
        Book book1;
        Book book2;
        [TestInitialize()]
        public void SetUp()
        {
            book1 = new Book("111-222-333", "Wojciech Sowa", "Life is life", "Amazing book");
            book2 = new Book("111-222-333", "Wojciech Sowa", "Life is life", "Amazing book");
        }

        [TestMethod]
        public void ConstructorBookTest()
        {
            Assert.AreEqual("111-222-333", book1.Isbn);
            Assert.AreEqual("Wojciech Sowa", book1.Author);
            Assert.AreEqual("Life is life", book1.Title);
            Assert.AreEqual("Amazing book", book1.Description);            
        }

        [TestMethod]
        public void SetterBookTest()
        {
            Assert.AreEqual("Wojciech Sowa", book1.Author);
            book1.Author = "Artur Madaj";
            Assert.AreEqual("Artur Madaj", book1.Author);


            Assert.AreEqual("Amazing book", book1.Description);
            book1.Description = "Good book";
            Assert.AreEqual("Good book", book1.Description);
        }

        [TestMethod]
        public void EqualsBookTest()
        {
            Assert.IsTrue(book1.Equals(book2));
            Assert.AreEqual(book1, book2);

            book1.Author = "Artur Madaj";

            Assert.IsFalse(book1.Equals(book2));
            Assert.AreNotEqual(book1, book2);
        }

        [TestMethod]
        public void HashCodeBookTest()
        {
            Assert.AreEqual(book1.GetHashCode(), book2.GetHashCode());

            book1.Author = "Artur Madaj";

            Assert.AreNotEqual(book1.GetHashCode(), book2.GetHashCode());
        }
    }
}
