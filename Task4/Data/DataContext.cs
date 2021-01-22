using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;

namespace Data
{
    class DataContext : IDataContext<CreditCard>
    {
        DataBaseDataContext DataBaseDataContext;
        public DataContext()
        {
            DataBaseDataContext = new DataBaseDataContext();
        }
        public void AddItem(CreditCard item)
        {
            DataBaseDataContext.CreditCards.InsertOnSubmit(item);
            DataBaseDataContext.SubmitChanges();
        }

        public void DeleteItem(CreditCard item)
        {
            //CreditCard card = DataBaseDataContext.get
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
