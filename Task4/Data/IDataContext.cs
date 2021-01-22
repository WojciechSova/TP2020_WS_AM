using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IDataContext<T>
    {
        T GetItem(int id);
        IEnumerable<T> GetAll();
        void AddItem(T item);
        void DeleteItem(T item);
        void UpdateItem(T item);
    }
}
