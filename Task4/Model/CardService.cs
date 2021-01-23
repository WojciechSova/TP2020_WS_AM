using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logic;

namespace Model
{
    public class CardService
    {
        private IService service;

        public CardService()
        {
            this.service = new Service();
        }

        public CardService(IService service)
        {
            this.service = service;
        }

        public CardModel GetCard(int id)
        {
            return (CardModel)service.GetCreditCard(id);
        }

        public IEnumerable<CardModel> GetAllCreditCards()
        {
            return service.GetAllCreditCards().Select(card => new CardModel(card));
        }

        public void AddCard(CardModel card)
        {
            service.AddCreditCard(card);
        }

        public void DeleteCreditCard(int id)
        {
            service.DeleteCreditCard(id);
        }

        public void UpdateCreditCard(int id, CardModel card)
        {
            service.UpdateCreditCard(id, card);
        }
    }
}
