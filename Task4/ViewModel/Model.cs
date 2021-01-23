using Logic;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    class Model
    {
        public Model(ICreditCard card)
        {
            CreditCardID = card.CreditCardID;
            CardNumber = card.CardNumber;
            CardType = card.CardType;
            ExpMonth = card.ExpMonth;
            ExpYear = card.ExpYear;
        }

        public Model(int creditCardID, string cardNumber, string cardType, byte expMonth, short expYear)
        {
            CreditCardID = creditCardID;
            CardNumber = cardNumber;
            CardType = cardType;
            ExpMonth = expMonth;
            ExpYear = expYear;
        }

        public int CreditCardID { get; set; }
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public byte ExpMonth { get; set; }
        public short ExpYear { get; set; }
    }
}
