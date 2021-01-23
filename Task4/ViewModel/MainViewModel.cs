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

namespace ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private CardModel cardModel;
        private CardService cardService;
        private ObservableCollection<CardModel> cardList;
        public ICommand AddCard { get; set; }
        public ICommand RemoveCard { get; set; }
        public ICommand UpdateCard { get; set; }
        public ICommand GetAllCards { get; set; }
        public ICommand GetDetails { get; set; }

        public MainViewModel(CardModel creditCard, CardService cardService)
        {
            AddCard = new RelayCommand(AddCreditCard);
            RemoveCard = new RelayCommand(RemoveCreditCard);
            UpdateCard = new RelayCommand(UpdateCreditCard);
            /*GetAllCards = new RelayCommand(() => Service = new ServiceWrapper());
            GetDetails = new RelayCommand(GetCardDetails);*/
            this.cardModel = creditCard;
            this.cardService = cardService;
        }

        public MainViewModel()
        {

        }

        public int CardId
        {
            get => cardModel.CreditCardID;
            set
            {
                cardModel.CreditCardID = value;
                OnPropertyChanged("CardId");
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

        public void AddCreditCard()
        {
            Task.Run(() =>
            {
                cardService.AddCard(cardModel);
            });
        }

        public void RemoveCreditCard()
        {
            Task.Run(() => cardService.DeleteCreditCard(cardModel.CreditCardID));

        }

        public void UpdateCreditCard()
        {
            Task.Run(() =>
            {
                cardService.UpdateCreditCard(cardModel.CreditCardID, cardModel);
            });
        }
    }
}