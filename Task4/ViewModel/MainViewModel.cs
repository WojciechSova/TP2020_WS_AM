using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Data;
using Model;
using ViewModel;
using ViewModel.Interface;

namespace ViewModel
{
    public class MainViewModel : ViewModelBase, IViewModel
    {
        private CardModel cardModel;
        private CardService cardService;
        private List<CardModel> cardList;
        private Boolean addMethod = true;
        public ICommand AddCard { get; set; }
        public ICommand RemoveCard { get; set; }
        public ICommand UpdateCard { get; set; }
        public ICommand ShowAddDialog { get; set; }
        public ICommand ShowUpdateDialog { get; set; }
        public ICommand OKCommand { get; set; }

        public IWindowResolver WindowResolver { get; set; }

        private CardModel currentCard;
        public MainViewModel()
        {
            cardModel = new CardModel();
            cardService = new CardService();
            InitCommands();
        }
        public MainViewModel(CardModel creditCard, CardService cardService)
        {
            cardModel = creditCard;
            this.cardService = cardService;
            InitCommands();
        }

        private void InitCommands()
        {
            RemoveCard = new RelayCommand(RemoveCreditCard);
            ShowAddDialog = new RelayCommand(ShowAddDialogMethod);
            ShowUpdateDialog = new RelayCommand(ShowUpdateDialogMethod);
            OKCommand = new RelayCommand(OkMethod);
        }

        private void ShowAddDialogMethod()
        {
            addMethod = true;
            IOperationWindow dialog = WindowResolver.GetWindow();
            dialog.BindViewModel(this);
            dialog.Show();
            CreditCardList = GetCreditCards();
        }

        private void ShowUpdateDialogMethod()
        {
            addMethod = false;
            cardModel = currentCard;
            IOperationWindow dialog = WindowResolver.GetWindow();
            dialog.BindViewModel(this);
            dialog.Show();
            CreditCardList = GetCreditCards();
        }

        private void OkMethod()
        {
            if (addMethod)
            {
                Task.Run(() => cardService.AddCard(cardModel));
            }
            else
            {
                Task.Run(() => cardService.UpdateCreditCard(cardModel.CreditCardID, cardModel));
            }
            CreditCardList = GetCreditCards();
            cardModel = new CardModel();
            CloseWindow?.Invoke();
        }

        public CardModel SelectedCreditCard
        {
            get => currentCard;
            set
            {
                currentCard = value;
                OnPropertyChanged("SelectedCreditCard");
            }
        }

        public List<CardModel> GetCreditCards()
        {
            if (cardList == null)
            {
                cardList = new List<CardModel>();
            }

            cardList.Clear();

            return cardList = cardService.GetAllCreditCards().ToList();
        }

        public List<CardModel> CreditCardList
        {
            get => GetCreditCards();
            set
            {
                cardList = value;
                OnPropertyChanged("CreditCardList");
            }
        }

        public int CreditCardID
        {
            get => cardModel.CreditCardID;
            set
            {
                cardModel.CreditCardID = value;
                OnPropertyChanged("CreditCardID");
            }
        }

        public string CardNumber
        {
            get => cardModel.CardNumber;
            set
            {
                cardModel.CardNumber = value;
                OnPropertyChanged("CardNumber");
            }
        }

        public string CardType
        {
            get => cardModel.CardType;
            set
            {
                cardModel.CardType = value;
                OnPropertyChanged("CardType");
            }
        }

        public byte ExpMonth
        {
            get => cardModel.ExpMonth;
            set
            {
                cardModel.ExpMonth = value;
                OnPropertyChanged("ExpMonth");
            }
        }

        public short ExpYear
        {
            get => cardModel.ExpYear;
            set
            {
                cardModel.ExpYear = value;
                OnPropertyChanged("ExpYear");
            }
        }

        public Action CloseWindow { get ; set; }


        public void RemoveCreditCard()
        {
            Task.Run(() => cardService.DeleteCreditCard(currentCard.CreditCardID));
        }
    }
}