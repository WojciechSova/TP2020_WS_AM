namespace Logic
{
    public class CreditCard : ICreditCard
    {
        public CreditCard() { }

        public CreditCard(Data.CreditCard creditCard)
        {
            CreditCardID = creditCard.CreditCardID;
            CardNumber = creditCard.CardNumber;
            CardType = creditCard.CardType;
            ExpMonth = creditCard.ExpMonth;
            ExpYear = creditCard.ExpYear;
        }

        public int CreditCardID { get; set; }
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public byte ExpMonth { get; set; }
        public short ExpYear { get; set; }
    }
}
