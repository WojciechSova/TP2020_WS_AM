using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Data;
using Model;
using ViewModel;

namespace ViewModel
{
    class MainViewModel : ViewModelBase
    {
        private IServiceWrapper service;
        private Model cardCategory;
        private ObservableCollection<Model> cardCategories;
        private Model cardCategoryInfo;
        private ObservableCollection<Model> cardCategoriesInfo;
        public string CardNumber { get; set; }
        public ICommand AddCard { get; set; }
        public ICommand RemoveCard { get; set; }
        public ICommand UpdateCard { get; set; }
        public ICommand GetAllCards { get; set; }
        public ICommand GetDetails { get; set; }

        public MainViewModel()
        {
            Service = new ServiceWrapper();
            AddCard = new RelayCommand(AddCreditCard);
            RemoveCard = new RelayCommand(RemoveCreditCard);
            UpdateCard = new RelayCommand(UpdateCreditCard);
            GetAllCards = new RelayCommand(() => Service = new ServiceWrapper());
            GetDetails = new RelayCommand(GetCardDetails);
        }



        public IServiceWrapper Service
        {
            get { return service; }
            set
            {
                service = value;

                Task.Run(() =>
                {
                    cardCategories = new ObservableCollection<Model>(value.GetAllCreditCards());
                });

            }
        }

        public ObservableCollection<Model> CardCategories
        {
            get { return cardCategories; }
            set
            {
                cardCategories = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Model> CardCategoriesInfo
        {
            get { return cardCategoriesInfo; }
            set
            {
                cardCategoriesInfo = value;
                RaisePropertyChanged();
            }
        }

        public Model CardCategoryInfo
        {
            get
            {
                return cardCategoryInfo;
            }
            set
            {
                cardCategoryInfo = value;
                RaisePropertyChanged();
            }
        }

        public Model CardCategory
        {
            get
            {
                return cardCategory;
            }
            set
            {
                cardCategory = value;
                RaisePropertyChanged();
            }
        }

        public void AddCreditCard()
        {
            Model productCategory = new Model(99, "11111111111", "Visa", 1, 2021);
            Task.Run(() =>
            {
                service.AddCard(productCategory);
            });
        }

        public void RemoveCreditCard()
        {
            Task.Run(() => service.DeleteCreditCard(cardCategory.CreditCardID));

        }

        public void UpdateCreditCard()
        {
            Task.Run(() =>
            {
                service.UpdateCreditCard(cardCategory.CreditCardID, CardCategory);
            });
        }

        public void GetCardDetails()
        {
            Task.Run(() =>
            {
                CardCategoriesInfo = new ObservableCollection<Model>();
                CardCategoriesInfo.Add(service.GetCard(cardCategory.CreditCardID));
                CardCategoryInfo = service.GetCard(cardCategory.CreditCardID);
            });
        }
    }
}
