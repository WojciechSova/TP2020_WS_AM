using System.Collections.Generic;

namespace Logic
{
    public interface IService
    {
        void AddCreditCard(ICreditCard creditCard);
        ICreditCard GetCreditCard(int id);
        IEnumerable<ICreditCard> GetAllCreditCards();
        void UpdateCreditCard(int id, ICreditCard creditCard);
        void DeleteCreditCard(int id);
    }
}
