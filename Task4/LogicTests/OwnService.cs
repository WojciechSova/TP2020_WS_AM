using Logic;
using System.Collections.Generic;
using System.Linq;

namespace LogicTests
{
    public class OwnService : IService
    {
        private List<ICreditCard> cards;
        public OwnService()
        {
            CreditCard cardA = new CreditCard
            {
                CreditCardID = 666,
                CardNumber = "0000111199995555",
                CardType = "Master Card",
                ExpMonth = 4,
                ExpYear = 23
            };
            CreditCard cardB = new CreditCard
            {
                CreditCardID = 999,
                CardNumber = "9999888822224444",
                CardType = "WebBank",
                ExpMonth = 1,
                ExpYear = 21
            };
            CreditCard cardC = new CreditCard
            {
                CreditCardID = 2137,
                CardNumber = "0000222244447777",
                CardType = "WebBank",
                ExpMonth = 8,
                ExpYear = 20
            };
            cards = new List<ICreditCard>();
            cards.Add(cardA);
            cards.Add(cardB);
            cards.Add(cardC);
        }

        public void AddCreditCard(ICreditCard creditCard)
        {
            cards.Add(creditCard);
        }

        public void DeleteCreditCard(int id)
        {
            cards.Remove(GetCreditCard(id));
        }

        public IEnumerable<ICreditCard> GetAllCreditCards()
        {
            return cards;
        }

        public ICreditCard GetCreditCard(int id)
        {
            return cards.Single(cards => cards.CreditCardID == id);
        }

        public void UpdateCreditCard(int id, ICreditCard creditCard)
        {
            ICreditCard cc = GetCreditCard(id);
            cc.CardNumber = creditCard.CardNumber;
            cc.CardType = creditCard.CardType;
            cc.CreditCardID = creditCard.CreditCardID;
            cc.ExpMonth = creditCard.ExpMonth;
            cc.ExpYear = creditCard.ExpYear;
        }
    }
}
