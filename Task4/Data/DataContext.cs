﻿using System;
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
            DataBaseDataContext.SubmitChanges(ConflictMode.ContinueOnConflict);
        }

        public void DeleteItem(CreditCard item)
        {
            CreditCard card = GetItem(item.CreditCardID);
            DataBaseDataContext.CreditCards.DeleteOnSubmit(item);
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
            cc.CreditCardID = item.CreditCardID;
            cc.ExpMonth = item.ExpMonth;
            cc.ExpYear = item.ExpYear;
            cc.ModifiedDate = DateTime.UtcNow;
            DataBaseDataContext.SubmitChanges(ConflictMode.ContinueOnConflict);
        }
    }
}
