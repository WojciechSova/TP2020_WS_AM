using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
