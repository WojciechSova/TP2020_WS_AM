using System;
using Task1.Data;

namespace Task1
{
    public class DataService implements IDataService
    {
        private IDataRepository IDataRepository;

        public DataService(IDataRepository iDataRepository)
        {
            IDataRepository = iDataRepository;
        }



    }
}