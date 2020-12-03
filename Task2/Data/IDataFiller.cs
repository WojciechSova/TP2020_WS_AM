using System;
using System.Collections.Generic;
using System.Text;

namespace Task1.Data
{
    public interface IDataFiller
    {
        void Fill(DataContext dataContext);
    }
}
