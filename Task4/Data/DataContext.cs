using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    class DataContext : IDataContext<CreditCard>
    {

        public DataContext()
        {

        }
        public void AddItem(CreditCard item)
        {
            throw new NotImplementedException();
        }

        public void DeleteItem(CreditCard item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CreditCard> GetAll()
        {
            throw new NotImplementedException();
        }

        public CreditCard GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(CreditCard item)
        {
            throw new NotImplementedException();
        }
    }
}
