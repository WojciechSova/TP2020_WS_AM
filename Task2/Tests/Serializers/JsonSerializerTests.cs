using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Task2.Data;
using Tests.DataFiller;

namespace Tests.Serializers
{
    [TestClass]
    public class JsonSerializerTests
    {
        [TestMethod]
        public void JsonSerializerTest()
        {
            String filePath = "..\\..\\..\\..\\TestResults\\jsonFile.json";
            DataContext dataContext = new DataContext();
            IDataFiller dataFiller = new ConstantFiller();
            _ = new DataRepository(dataFiller, dataContext);

            JsonSerializer.Serialize(dataContext, filePath);

            DataContext deserialContext = JsonSerializer.Deserialize<DataContext>(filePath);

            Assert.AreEqual(dataContext.BookSet[1], deserialContext.BookSet[1]);
            Assert.AreEqual(dataContext.BookSet.Count, deserialContext.BookSet.Count);
            Assert.AreEqual(dataContext.ReadersList[1], deserialContext.ReadersList[1]);
            Assert.AreEqual(dataContext.BookStatesList[1], deserialContext.BookStatesList[1]);
            Assert.AreEqual(dataContext.BookEvents[1], deserialContext.BookEvents[1]);
        }
    }
}
