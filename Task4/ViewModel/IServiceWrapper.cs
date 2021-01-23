﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    interface IServiceWrapper
    {
        Model GetCard(int id);

        IEnumerable<Model> GetAllCreditCards();

        void AddCard(Model card);

        void DeleteCreditCard(int id);

        void UpdateCreditCard(int id, Model card);

    }
}