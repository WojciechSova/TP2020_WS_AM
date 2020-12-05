using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serializers;
using System;
using System.Collections.Generic;
using System.Text;
using Task2.Data;
using Tests.DataFiller;

namespace Tests.Serializers
{
    [TestClass]
    public class JsonSerializerTests
    {
        private readonly String filePath = "jsonFile.json";

        [TestMethod]
        public void JsonSerializeTest()
        {
            DataContext dataContext = new DataContext();
            IDataFiller dataFiller = new ConstantFiller();
            IDataRepository dataRepository;
            dataRepository = new Task2.Data.DataRepository(dataFiller, dataContext);
            JsonSerializer.Serialize(dataContext, filePath);

            DataContext deserialContext = JsonSerializer.Deserialize<DataContext>(filePath);

            Assert.AreEqual(dataContext.BookSet[1], deserialContext.BookSet[1]);
            Assert.AreEqual(dataContext.ReadersList[1], deserialContext.ReadersList[1]);
            Assert.AreEqual(dataContext.BookStatesList[1], deserialContext.BookStatesList[1]);
            Assert.AreEqual(dataContext.BookEvents[1], deserialContext.BookEvents[1]);
        }
    }
}
