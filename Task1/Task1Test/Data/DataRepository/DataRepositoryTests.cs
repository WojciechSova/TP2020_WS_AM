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

        public GetReaderTest()
        {

        }
        #endregion
    }
}
