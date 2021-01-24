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
        public ICommand AddCard { get; set; }
        public ICommand RemoveCard { get; set; }
        public ICommand UpdateCard { get; set; }
        //public ICommand ShowAddDialog { get; set; }
        private ICommand _showAddCommand;
        public ICommand ShowAddDialog => _showAddCommand ?? (_showAddCommand = new RelayCommand(ShowAddDialogMethod));
        public IWindowResolver WindowResolver { get; set; }

        private CardModel currentCard;
        public MainViewModel()
        {
            cardModel = new CardModel();
            cardService = new CardService();
        }
        public MainViewModel(CardModel creditCard, CardService cardService)
        {
            AddCard = new RelayCommand(AddCreditCard);
            RemoveCard = new RelayCommand(RemoveCreditCard);
            UpdateCard = new RelayCommand(UpdateCreditCard);
            //ShowAddDialog = new RelayCommand(ShowAddDialogMethod);
            this.cardModel = creditCard;
            this.cardService = cardService;
        }

        private void ShowAddDialogMethod()
        {

            IOperationWindow dialog = WindowResolver.GetWindow();
            dialog.BindViewModel(this);
            dialog.Show();
            CreditCardList = GetCreditCards();
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

        public void AddCreditCard()
        {
            Task.Run(() =>
            {
                cardService.AddCard(cardModel);
            });
        }

        public void RemoveCreditCard()
        {
            Task.Run(() => cardService.DeleteCreditCard(currentCard.CreditCardID));
        }

        public void UpdateCreditCard()
        {
            Task.Run(() =>
            {
                cardService.UpdateCreditCard(currentCard.CreditCardID, cardModel);
            });
        }
    }
}