using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task2.Data;

namespace Tests.Data
{
    [TestClass]
    public class ReaderTests
    {
        [TestMethod]
        public void CreateReaderTest()
        {
            Reader reader = new Reader("Artur", "Xinski", 10553311245);

            Assert.AreEqual("Xinski", reader.Surname);
            Assert.AreEqual(10553311245, reader.PersonalID);
        }

        [TestMethod]
        public void SetterReaderTest()
        {
            Reader reader = new Reader("Wojciech", "Ygrecki", 111155469);
            reader.Name = "Janusz";
            reader.Surname = "Kowalski";

            Assert.AreEqual("Janusz", reader.Name);
            Assert.AreEqual("Kowalski", reader.Surname);
        }

        [TestMethod]
        public void HashCodeReaderTest()
        {
            Reader reader1 = new Reader("Wojciech", "Ygrecki", 111155469);
            Reader reader2 = new Reader("Wojciech", "Ygrecki", 111155469);
            Reader reader3 = new Reader("Wojciech", "Ygrecki", 12345678);

            Assert.AreEqual(reader1.GetHashCode(), reader2.GetHashCode());
            Assert.AreNotEqual(reader3.GetHashCode(), reader2.GetHashCode());
        }

        [TestMethod]
        public void EqualsReaderTest()
        {
            Reader reader1 = new Reader("Wojciech", "Ygrecki", 111155469);
            Reader reader2 = new Reader("Wojciech", "Ygrecki", 111155469);
            Reader reader3 = new Reader("Wojciech", "Ygrecki", 12345678);

            Assert.IsTrue(reader1.Equals(reader2));
            Assert.IsFalse(reader1.Equals(reader3));
        }
    }
}
