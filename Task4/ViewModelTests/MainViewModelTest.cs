using Model;
using LogicTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel;

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
                ExpMonth = 04,
                ExpYear = 20
            };
            mainViewModel = new MainViewModel(creditCardModel, service);
        }

        [TestMethod]
        public void ConstructorTest()
        {
            Assert.IsNotNull(mainViewModel.CardNumber);
            Assert.IsNotNull(mainViewModel.CreditCardID);
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
            mainViewModel.OkMethod();
            Assert.AreEqual(4, mainViewModel.CreditCardList.Count);
        }

        [TestMethod]
         public void DeleteCreditCardViewModelTest()
         {
            Assert.AreEqual(3, mainViewModel.CreditCardList.Count);
            mainViewModel.SelectedCreditCard = creditCardModel;
            mainViewModel.RemoveCreditCard();
            Assert.AreEqual(2, mainViewModel.CreditCardList.Count);
         }
    }
}
