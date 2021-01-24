using System;
using Logic;

namespace Model
{
    public class CardModel : ICreditCard
    {
        public CardModel()
        {
            CardNumber = "";
            CardType = "";
        }

        public CardModel(ICreditCard card)
        {
            CreditCardID = card.CreditCardID;
            CardNumber = card.CardNumber;
            CardType = card.CardType;
            ExpMonth = card.ExpMonth;
            ExpYear = card.ExpYear;
        }
        public int CreditCardID { get; set; }
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public byte ExpMonth { get; set; }
        public short ExpYear { get; set; }
    }
}
