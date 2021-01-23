using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using System.Linq;

namespace LogicTests
{
    [TestClass]
    public class ServiceTest
    {
        private ICreditCard creditCard;
        private IService service;

        [TestInitialize]
        public void Init()
        {
            creditCard = new CreditCard()
            {
                CreditCardID = 123456,
                CardNumber = "9876543210",
                CardType = "VISA",
                ExpMonth = 04,
                ExpYear = 20
            };
            service = new OwnService();
        }

        [TestMethod]
        public void AddGetAllCardTest()
        {
            Assert.AreEqual(3, service.GetAllCreditCards().Count());
            service.AddCreditCard(creditCard);
            Assert.AreEqual(4, service.GetAllCreditCards().Count());
            Assert.AreEqual(creditCard.CreditCardID, service.GetAllCreditCards().Last().CreditCardID);
        }

        [TestMethod]
        public void GetCardTest()
        {
            service.AddCreditCard(creditCard);
            Assert.AreEqual(creditCard.CardNumber, service.GetCreditCard(creditCard.CreditCardID).CardNumber);
        }

        [TestMethod]
        public void DeleteCardTest()
        {
            Assert.AreEqual(3, service.GetAllCreditCards().Count());
            service.DeleteCreditCard(service.GetAllCreditCards().First().CreditCardID);
            Assert.AreEqual(2, service.GetAllCreditCards().Count());
        }

        [TestMethod]
        public void UpdateCardTest()
        {
            service.UpdateCreditCard(service.GetAllCreditCards().First().CreditCardID, creditCard);
            Assert.AreEqual(creditCard.CardNumber, service.GetAllCreditCards().First().CardNumber);
            Assert.AreEqual(creditCard.CardType, service.GetAllCreditCards().First().CardType);
        }
    }
}
