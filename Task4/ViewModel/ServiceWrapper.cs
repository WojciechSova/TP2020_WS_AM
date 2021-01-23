using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel
{
    class ServiceWrapper : IServiceWrapper
    {
        private IService service;

        public ServiceWrapper()
        {
            this.service = new Service();
        }

        public ServiceWrapper(IService service)
        {
            this.service = service;
        }

        public Model GetCard(int id)
        {
            return (Model)service.GetCreditCard(id);
        }

        public IEnumerable<Model> GetAllCreditCards()
        {
            return service.GetAllCreditCards().Select(card => new Model(card));
        }

        public void AddCard(Model card)
        {
            service.AddCreditCard((ICreditCard)card);
        }

        public void DeleteCreditCard(int id)
        {
            service.DeleteCreditCard(id);
        }

        public void UpdateCreditCard(int id, Model card)
        {
            service.UpdateCreditCard(id, (ICreditCard)card);
        }
    }
}
