using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Task2.Data;


namespace Tests.DataFiller
{
    [TestClass]
    public class DataFillerTests
    {
        DataContext dataContext = new DataContext();
        [TestMethod]
        public void ConstantFillerTest()
        {
            IDataFiller dataFiller = new ConstantFiller();
            IDataRepository dataRepository = new DataRepository(dataFiller, dataContext);


            Assert.AreEqual(7, dataContext.ReadersList.Count);
            Assert.AreEqual(7, dataContext.BookSet.Count);
            Assert.AreEqual(7, dataContext.BookStatesList.Count);
            Assert.AreEqual(7, dataContext.BookEvents.Count);
            Assert.AreEqual(5, dataContext.BookEvents.OfType<BookRent>().ToList().Count);

            Assert.AreEqual("Arciech1", dataContext.ReadersList[1].Name);
            Assert.AreEqual("Catchy Title 5", dataContext.BookSet[5].Title);
        }

        [TestMethod]
        public void RandomFilterTest()
        {
            IDataFiller dataFiller = new RandomFiller();
            IDataRepository dataRepository = new DataRepository(dataFiller, dataContext);


            Assert.AreEqual(7, dataContext.ReadersList.Count);
            Assert.AreEqual(7, dataContext.BookSet.Count);
            Assert.AreEqual(7, dataContext.BookStatesList.Count);
            Assert.AreEqual(5, dataContext.BookEvents.OfType<BookRent>().ToList().Count);
        }
    }
}
