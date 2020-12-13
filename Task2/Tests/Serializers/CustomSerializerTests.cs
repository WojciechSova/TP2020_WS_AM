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
        CustomFormatter customFormatter;
        [TestInitialize]
        public void TestInitialize()
        {
            ClassA classA = new ClassA("classA", DateTime.Now, true);
            ClassB classB = new ClassB("classB", DateTime.Now, 777);
            ClassC classC = new ClassC("classC", DateTime.Now);

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

            customFormatter = new CustomFormatter();
        }

        [TestMethod]
        public void TestGraphSerializationClass1()
        {
            using (FileStream fileStream = new FileStream("..\\..\\..\\..\\TestResults\\ClassAGraph.txt", FileMode.Create))
            {
                customFormatter.Serialize(fileStream, classA);
            }

        }
    }
}
