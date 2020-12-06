using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Task2.Data;
using Tests.DataFiller;

namespace Tests.Serializers
{
    [TestClass]
    public class CustomSerializerTests
    {
        private readonly String filePath = "..\\..\\..\\..\\TestResults\\CustomSerialization.saved";

        [TestMethod]
        public void CustomSerializeTest()
        { 
            DataContext dataContext = new DataContext();
            IDataFiller dataFiller = new ConstantFiller();
            IDataRepository dataRepository;
            dataRepository = new Task2.Data.DataRepository(dataFiller, dataContext);
            CustomFormatter customFormatter = new CustomFormatter();
            dataContext.countEvents();

            using (Stream stream = new FileStream(filePath, FileMode.Create))
            {
                customFormatter.Serialize(stream, dataContext);
            }

            DataContext dataContext2;

            using (Stream stream = new FileStream(filePath, FileMode.Open))
            {
                dataContext2 = (DataContext)customFormatter.Deserialize(stream);
            }

            for (int i = 0; i < 6; i++)
            {
                Assert.AreEqual(dataContext.BookEvents[i], dataContext2.BookEvents[i]);
            }

        }
    }
}
