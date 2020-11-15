using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Task1.Data;
using Task1Test.DataFiller;

namespace Task1Test.Data.DataRepository
{
    [TestClass]
    public class DataRepositoryTests
    {
        IDataFiller dataFiller = new ConstantFiller();
        DataContext dataContext= new DataContext();
        IDataRepository dataRepository;

        #region Reader
        [TestMethod]
        public void AddReaderTest()
        {
            dataRepository = new Task1.Data.DataRepository(dataFiller, dataContext);

            int listOfReadersSize = dataRepository.GetAllReaders().Count();
            dataRepository.AddReader(new Reader("Artur", "Xinski", 123456987));

            Assert.AreEqual(listOfReadersSize + 1, dataRepository.GetAllReaders().Count());
            Assert.AreEqual("Artur", dataRepository.GetReader(dataRepository.GetAllReaders().Count() - 1).Name);

        }

        [TestMethod]
        public void CannotAddTheSameReaderTest()
        {
            dataRepository = new Task1.Data.DataRepository(dataFiller, dataContext);

            dataRepository.AddReader(new Reader("Artur", "Xinski", 123456987));

            Assert.ThrowsException<ArgumentException>(() => dataRepository.AddReader(new Reader("Artur", "Xinski", 123456987)));
        }

        [TestMethod]
        public void GetReaderTest()
        {
            dataRepository = new Task1.Data.DataRepository(dataFiller, dataContext);

            for (int i = 0; i < 7; i++)
            {
                Assert.AreEqual("Sodaj" + i.ToString(), dataRepository.GetReader(i).Surname);
                Assert.AreEqual(1000000 + i, dataRepository.GetReader(i).PersonalID);
            }
        }

        [TestMethod]
        public void GetAllReadersTest()
        {
            dataRepository = new Task1.Data.DataRepository(dataFiller, dataContext);

            Assert.AreEqual(dataContext.ReadersList.Count, dataRepository.GetAllReaders().Count());
        }

        [TestMethod]
        public void UpdateReaderTest()
        {
            dataRepository = new Task1.Data.DataRepository(dataFiller, dataContext);

            dataRepository.UpdateReader(5, "UpdatedName", "UpdatedSurname", 7777777);

            Assert.AreEqual("UpdatedName", dataRepository.GetReader(5).Name);
            Assert.AreEqual(7777777, dataRepository.GetReader(5).PersonalID);
        }

        [TestMethod]
        public void NonExistentReaderTest()
        {
            dataRepository = new Task1.Data.DataRepository(dataFiller, dataContext);

            Assert.ThrowsException<KeyNotFoundException>(() => dataRepository.UpdateReader(10, "Artur", "Xinski", 123456987));
            Assert.ThrowsException<KeyNotFoundException>(() => dataRepository.GetReader(10));
            Assert.ThrowsException<KeyNotFoundException>(() => dataRepository.DeleteReader(10));
        }

        [TestMethod]
        public void DeleteReaderTest()
        {
            dataRepository = new Task1.Data.DataRepository(dataFiller, dataContext);

            int listOfReadersSize = dataRepository.GetAllReaders().Count();

            dataRepository.DeleteReader(1);

            Assert.AreEqual(listOfReadersSize - 1, dataRepository.GetAllReaders().Count());
        }

        [TestMethod]
        public void CannotRemoveReaderWithBorrowedBook()
        {
            dataRepository = new Task1.Data.DataRepository(dataFiller, dataContext);

            Assert.ThrowsException<InvalidOperationException>(() => dataRepository.DeleteReader(5));
        }
        #endregion
    }
}
