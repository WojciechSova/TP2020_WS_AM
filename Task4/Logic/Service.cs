﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    class Service : IService
    {
        private IDataContext<Data.CreditCard> DataContext;



        public void AddCreditCard(ICreditCard creditCard)
        {
            throw new NotImplementedException();
        }

        public void DeleteCreditCard(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ICreditCard> GetAllCreditCards()
        {
            throw new NotImplementedException();
        }

        public ICreditCard GetCreditCard(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateCreditCard(int id, ICreditCard creditCard)
        {
            throw new NotImplementedException();
        }
    }
}
