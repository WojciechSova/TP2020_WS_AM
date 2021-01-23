using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    public class Service : IService
    {
        private IDataContext<Data.CreditCard> DataContext { get; set; }

        public Service ()
        {
            DataContext = new DataContext();
        }

        public Service(IDataContext<Data.CreditCard> dataContext)
        {
            DataContext = dataContext;
        }

        public void AddCreditCard(ICreditCard creditCard)
        {
            Data.CreditCard card = MoveToData(creditCard);
            card.ModifiedDate = DateTime.UtcNow;
            DataContext.AddItem(card);
        }

        public void DeleteCreditCard(int id)
        {
            DataContext.DeleteItem(id);
        }

        public IEnumerable<ICreditCard> GetAllCreditCards()
        {
             return DataContext.GetAll().Select(card => new CreditCard(card));
        }

        public ICreditCard GetCreditCard(int id)
        {
            return new CreditCard(DataContext.GetItem(id));
        }

        public void UpdateCreditCard(int id, ICreditCard creditCard)
        {
            Data.CreditCard card = MoveToData(creditCard);
            card.ModifiedDate = DateTime.UtcNow;
            DataContext.UpdateItem(id, card);
        }

        private static Data.CreditCard MoveToData(ICreditCard creditCard)
        {
            return new Data.CreditCard
            {
                CardNumber = creditCard.CardNumber,
                CardType = creditCard.CardType,
                ExpMonth = creditCard.ExpMonth,
                ExpYear = creditCard.ExpYear
            };
        }
    }
}
