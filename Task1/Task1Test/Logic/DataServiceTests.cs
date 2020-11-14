using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task1.Data;
using Task1.Logic;
using Task1Test.DataFiller;
using System.Linq;
using System.Collections.Generic;

namespace Task1Test.Logic
{
    [TestClass]
    public class DataServiceTests
    {
        private IDataFiller dataFiller = new ConstantFiller();
        private DataContext dataContext = new DataContext();
        private IDataRepository dataRepository;
        private IDataService dataService;

        [TestMethod]
        public void RentBookTest()
        {
            IDataRepository dataRepository = new DataRepository(dataFiller, dataContext);
            dataService = new DataService(dataRepository);


        }
    }
}
