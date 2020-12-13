using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Task2.Data;
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
            classA = new ClassA("claaasdassA", DateTime.Now, true);
            classB = new ClassB("cldsadsaassB", DateTime.Now, 777.2);
            classC = new ClassC("dsadsadsa", DateTime.Now);

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
            Assert.AreEqual(classA.DateTime, classADeserialized.DateTime);
            Assert.AreEqual(classA.Available, classADeserialized.Available);
            Assert.AreEqual(classA.ClassB.Name, classADeserialized.ClassB.Name);
            Assert.AreEqual(classA.ClassB.DateTime, classADeserialized.ClassB.DateTime);
            Assert.AreEqual(classA.ClassB.Amount, classADeserialized.ClassB.Amount);
            Assert.AreEqual(classA.ClassC.DateTime, classADeserialized.ClassC.DateTime);
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
