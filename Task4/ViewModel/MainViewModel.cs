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
        private List<CardModel> cardList;
        public ICommand AddCard { get; set; }
        public ICommand RemoveCard { get; set; }
        public ICommand UpdateCard { get; set; }

        private CardModel currentCard;

        public MainViewModel(CardModel creditCard, CardService cardService)
        {
            AddCard = new RelayCommand(AddCreditCard);
            RemoveCard = new RelayCommand(RemoveCreditCard);
            UpdateCard = new RelayCommand(UpdateCreditCard);
            this.cardModel = creditCard;
            this.cardService = cardService;
        }

        public MainViewModel()
        {
            cardService = new CardService();
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
            //foreach (CardModel card in cardService.GetAllCreditCards().Select(card => new CardModel(card)))
            //{
            //    cardList.Add(card);
            //}

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