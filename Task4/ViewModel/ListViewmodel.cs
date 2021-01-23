//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Windows.Input;
//using Model;

//namespace ViewModel
//{
//    public class ListViewModel : ViewModelBase
//    {
//        private static ListViewModel _instance;

//        private static CardService cardService;

//        private ObservableCollection<MainViewModel> cardList;

//        private MainViewModel currentCard;

///*        private ICommand _showAddCommand;
//        private ICommand _showEditCommand;*/

//        public ListViewModel() : this(new CardService()) { }

//        public ListViewModel(CardService service)
//        {
//            cardService = service;
//            CreditCardList = GetCreditCards();
//        }

//        //public IWindowResolver WindowResolver { get; set; }

//        public ObservableCollection<MainViewModel> CreditCardList
//        {
//            get => GetCreditCards();
//            set
//            {
//                cardList = value;
//                OnPropertyChanged("CreditCardList");
//            }
//        }

//        public MainViewModel SelectedCreditCard
//        {
//            get => currentCard;
//            set
//            {
//                currentCard = value;
//                OnPropertyChanged("SelectedCreditCard");
//            }
//        }

//        /*public ICommand ShowAddCommand => _showAddCommand ?? (_showAddCommand = new RelayCommand(ShowAddDialog));
//        public ICommand ShowEditCommand => _showEditCommand ?? (_showEditCommand = new RelayCommand(ShowEditDialog));*/

//        public Action CloseWindow { get; set; }

//        public static ListViewModel Instance()
//        {
//            return _instance ?? (_instance = new ListViewModel(cardService));
//        }

//        public ObservableCollection<MainViewModel> GetCreditCards()
//        {
//            if (cardList == null)
//            {
//                cardList = new ObservableCollection<MainViewModel>();
//            }

//            cardList.Clear();
//            foreach (MainViewModel card in cardService.GetAllCreditCards().Select(card => new MainViewModel(card, cardService)))
//            {
//                cardList.Add(card);
//            }

//            return cardList;
//        }

//        /*private void ShowAddDialog()
//        {

//            IOperationWindow dialog = WindowResolver.GetWindow();
//            dialog.BindViewModel(card);
//            dialog.Show();
//            CreditCardViewModel.Container.CreditCardList = GetCreditCards();
//        }

//        private void ShowEditDialog()
//        {
//            _selectedCreditCard.Mode = Mode.Edit;

//            IOperationWindow dialog = WindowResolver.GetWindow();
//            dialog.BindViewModel(_selectedCreditCard);
//            dialog.Show();
//        }*/
//    }
//}
