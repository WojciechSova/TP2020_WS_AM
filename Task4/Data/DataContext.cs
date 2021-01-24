using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;

namespace Data
{
    public class DataContext : IDataContext<CreditCard>
    {
        DataBaseDataContext DataBaseDataContext;
        public DataContext()
        {
            DataBaseDataContext = new DataBaseDataContext();
        }
        public void AddItem(CreditCard item)
        {
            DataBaseDataContext.CreditCards.InsertOnSubmit(item);
            DataBaseDataContext.SubmitChanges(ConflictMode.ContinueOnConflict);
        }

        public void DeleteItem(int id)
        {
            CreditCard card = GetItem(id);
            DataBaseDataContext.CreditCards.DeleteOnSubmit(card);
            DataBaseDataContext.SubmitChanges(ConflictMode.ContinueOnConflict);
        }

        public IEnumerable<CreditCard> GetAll()
        {
            return DataBaseDataContext.CreditCards;
        }

        public CreditCard GetItem(int id)
        {
            return DataBaseDataContext.CreditCards.Single(c => c.CreditCardID == id);
        }

        public void UpdateItem(int id, CreditCard item)
        {
            CreditCard cc = GetItem(id);
            cc.CardNumber = item.CardNumber;
            cc.CardType = item.CardType;
            cc.ExpMonth = item.ExpMonth;
            cc.ExpYear = item.ExpYear;
            cc.ModifiedDate = DateTime.Now;
            DataBaseDataContext.SubmitChanges(ConflictMode.ContinueOnConflict);
        }
    }
}
