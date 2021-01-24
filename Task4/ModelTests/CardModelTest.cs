using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace ModelTests
{
    [TestClass]
    public class CardModelTest
    {
        private CardModel card;

        [TestInitialize]
        public void TestInitialize()
        {
            card = new CardModel
            {
                CreditCardID = 1,
                CardNumber = "123",
                CardType = "Visa",
                ExpMonth = 1,
                ExpYear = 2022
            };
        }

        [TestMethod]
        public void CreditCardModel()
        {
            Assert.IsNotNull(card);
            Assert.AreEqual(1, card.CreditCardID);
            Assert.AreEqual("123", card.CardNumber);
            Assert.AreEqual("Visa", card.CardType);
            Assert.AreEqual(1, card.ExpMonth);
            Assert.AreEqual(2022, card.ExpYear);
        }
    }
}
