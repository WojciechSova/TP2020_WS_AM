using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Task2.DataModel;
using Task2.Serializers;

namespace Tests.Serializers
{
    [TestClass]
    public class JsonSerializerTests
    {
        ClassA classA;
        ClassB classB;
        ClassC classC;
        ClassA classADeserialized;
        Bookshelf bookshelf;
        Bookshelf bookshelfDeserialized;

        [TestInitialize]
        public void TestInitialize()
        {
            classA = new ClassA("claaasdassA", 2684132, true);
            classB = new ClassB("cldsadsaassB", 13216549, 777.2);
            classC = new ClassC("dsadsadsa", 1354987);

            classA.ClassB = classB;
            classA.ClassC = classC;
            classB.ClassA = classA;
            classB.ClassC = classC;
            classC.ClassA = classA;
            classC.ClassB = classB;

            bookshelf = new Bookshelf(
                new List<Book> {
                    new Book("123-456", 9.9), new Book("987-654", 1.1)},
                new BookGenres[] {
                    new BookGenres("Drama"), new BookGenres("Action"), new BookGenres("Poetry") }
                );
        }

        [TestMethod]
        public void JsonSerializerClassATest()
        {
            String filePath = "..\\..\\..\\..\\TestResults\\jsonFileClassA.json";

            JsonSerializer.Serialize(classA, filePath);

            classADeserialized = JsonSerializer.Deserialize<ClassA>(filePath);

            Assert.IsNotNull(classADeserialized);
            Assert.AreNotSame(classA, classADeserialized);

            Assert.AreEqual(classA.Name, classADeserialized.Name);
            Assert.AreEqual(classA.Number, classADeserialized.Number);
            Assert.AreEqual(classA.Available, classADeserialized.Available);
            Assert.AreEqual(classA.ClassB.Name, classADeserialized.ClassB.Name);
            Assert.AreEqual(classA.ClassB.Number, classADeserialized.ClassB.Number);
            Assert.AreEqual(classA.ClassB.Amount, classADeserialized.ClassB.Amount);
            Assert.AreEqual(classA.ClassC.Number, classADeserialized.ClassC.Number);
            Assert.AreEqual(classA.ClassC.Name, classADeserialized.ClassC.Name);
        }

        [TestMethod]
        public void JsonSerializerBookshelfTest()
        {
            String filePath = "..\\..\\..\\..\\TestResults\\jsonFileBookshelf.json";

            JsonSerializer.Serialize(bookshelf, filePath);

            bookshelfDeserialized = JsonSerializer.Deserialize<Bookshelf>(filePath);

            Assert.IsNotNull(bookshelfDeserialized);
            Assert.AreNotSame(bookshelf, bookshelfDeserialized);

            CollectionAssert.AreEqual(bookshelf.Books, bookshelfDeserialized.Books);
            CollectionAssert.AreEqual(bookshelf.BookGenres, bookshelfDeserialized.BookGenres);
        }



    }
}
