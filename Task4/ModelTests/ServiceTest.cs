using LogicTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System.Linq;

namespace ModelTests
{
    [TestClass]
    public class ServiceTest
    {
        private CardModel card;
        private CardService service;

        [TestInitialize]
        public void TestInitialize()
        {
            service = new CardService(new OwnService());
            card = new CardModel
            {
                CreditCardID = 3,
                CardNumber = "12345",
                CardType = "Visa",
                ExpMonth = 1,
                ExpYear = 2022
            };
        }

        [TestMethod]
        public void AddCreditCard()
        {
            Assert.AreEqual(3, service.GetAllCreditCards().Count());
            service.AddCard(card);
            Assert.AreEqual(4, service.GetAllCreditCards().Count());
        }


        [TestMethod]
        public void GetCreditCardTest()
        {
            service.AddCard(card);
            Assert.AreEqual(card.CardNumber, service.GetCard(card.CreditCardID).CardNumber);
        }

        [TestMethod]
        public void GetAllCreditCardsTest()
        {
            Assert.AreEqual(3, service.GetAllCreditCards().Count());
        }

        [TestMethod]
        public void UpdateCreditCardTest()
        {
            Assert.AreEqual(3, service.GetAllCreditCards().Count());
            service.UpdateCreditCard(service.GetAllCreditCards().Last().CreditCardID, card);
            Assert.AreEqual(3, service.GetAllCreditCards().Count());
            Assert.AreEqual(card.CardNumber, service.GetAllCreditCards().Last().CardNumber);
        }

        [TestMethod]
        public void DeleteCreditCardTest()
        {
            Assert.AreEqual(3, service.GetAllCreditCards().Count());
            service.AddCard(card);
            Assert.AreEqual(4, service.GetAllCreditCards().Count());
            service.DeleteCreditCard(service.GetAllCreditCards().Last().CreditCardID);
            Assert.AreEqual(3, service.GetAllCreditCards().Count());
        }
    }
}
