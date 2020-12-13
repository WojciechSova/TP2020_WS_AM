using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using Task2.Data;
using Task2.DataModel;

namespace Tests.Serializers
{
    [TestClass]
    public class CustomSerializerTests
    {
        private readonly String filePath = "..\\..\\..\\..\\TestResults\\CustomSerialization.saved";
        ClassA classA;
        ClassB classB;
        ClassC classC;
        ClassA classADeserialized;
        ClassB classBDeserialized;
        ClassC classCDeserialized;
        CustomFormatter customFormatter = new CustomFormatter();
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

            Bookshelf bookshelf = new Bookshelf(
                new List<Book> {
                    new Book("123-456", 9.9), new Book("987-654", 1.1)},
                new BookGenres[] {
                    new BookGenres("Drama"), new BookGenres("Action"), new BookGenres("Poetry") }
                );

            
        }

        [TestMethod]
        public void TestGraphSerializationClassA()
        {
            using (FileStream fileStream = new FileStream("ClassAGraph.txt", FileMode.Create))
            {
                customFormatter.Serialize(fileStream, classA);
            }

        }

        [TestMethod]
        public void TestGraphSerializationClassC()
        {
            using (FileStream fileStream = new FileStream("ClassCGraph.txt", FileMode.Create))
            {
                customFormatter.Serialize(fileStream, classC);
            }

        }

        [TestMethod]
        public void TestGraphDeserializationClassA()
        {
            using (FileStream fileStream = new FileStream("ClassAGraph.txt", FileMode.Open))
            {
                classADeserialized = (ClassA) customFormatter.Deserialize(fileStream);
            }
            Assert.IsNotNull(classADeserialized);
            Assert.AreNotSame(classA, classADeserialized);

            Assert.AreEqual(classA.Name, classADeserialized.Name);
            Assert.AreEqual(classA.Available, classADeserialized.Available);
            Assert.AreEqual(classA.Number, classADeserialized.Number);
        }

        [TestMethod]
        public void TestGraphDeserializationClassC()
        {
            using (FileStream fileStream = new FileStream("ClassCGraph.txt", FileMode.Open))
            {
                classCDeserialized = (ClassC)customFormatter.Deserialize(fileStream);
            }
            Assert.IsNotNull(classCDeserialized);
            Assert.AreNotSame(classC, classCDeserialized);

            Assert.AreEqual(classC.Name, classCDeserialized.Name);
            Assert.AreEqual(classC.Number, classCDeserialized.Number);
        }
    }
}
