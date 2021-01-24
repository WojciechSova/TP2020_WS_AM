using Model;
using LogicTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel;
using System.Threading;
using System.Linq;

namespace ViewModelTests
{
    [TestClass]
    public class MainViewModelTest
    {
        private MainViewModel mainViewModel;
        private CardModel creditCardModel;

        [TestInitialize]
        public void Init()
        {
            CardService service = new CardService(new OwnService());
            creditCardModel= new CardModel
            {
                CreditCardID = 123456,
                CardNumber = "98765432101234",
                CardType = "VISA",
                ExpMonth = 4,
                ExpYear = 20
            };
            mainViewModel = new MainViewModel(creditCardModel, service);
        }

        [TestMethod]
        public void ConstructorTest()
        {
            Assert.IsNotNull(mainViewModel.CardNumber);
            Assert.IsNotNull(mainViewModel.ExpMonth);
        }

        [TestMethod]
        public void CommandsExecutableTest()
        {
            Assert.IsTrue(mainViewModel.RemoveCard.CanExecute(null));
            Assert.IsTrue(mainViewModel.ShowAddDialog.CanExecute(null));
            Assert.IsTrue(mainViewModel.ShowUpdateDialog.CanExecute(null));
            Assert.IsTrue(mainViewModel.OKCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddCardTest()
        {
            Assert.AreEqual(3, mainViewModel.CreditCardList.Count);
            mainViewModel.OKCommand.Execute(null);
            Thread.Sleep(2000);
            Assert.AreEqual(4, mainViewModel.CreditCardList.Count);
        }

        [TestMethod]
         public void DeleteCreditCardViewModelTest()
         {
            AddCardTest();
            Assert.AreEqual(4, mainViewModel.CreditCardList.Count);
            mainViewModel.SelectedCreditCard = creditCardModel;
            mainViewModel.RemoveCard.Execute(null);
            Thread.Sleep(2000);
            Assert.AreEqual(3, mainViewModel.CreditCardList.Count);
         }

        [TestMethod]
        public void UpdateCardTest()
        {
            AddCardTest();
            Assert.AreEqual(4, mainViewModel.CreditCardList.Count);
            Assert.AreEqual(4, mainViewModel.CreditCardList.Last().ExpMonth);
            creditCardModel.ExpMonth = 6;
            mainViewModel.addMethod = false;
            mainViewModel.SelectedCreditCard = creditCardModel;
            mainViewModel.OKCommand.Execute(null);
            Thread.Sleep(2000);
            Assert.AreEqual(6, mainViewModel.CreditCardList.Last().ExpMonth);
            Assert.AreEqual(4, mainViewModel.CreditCardList.Count);
        }
    }
}
