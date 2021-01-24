using System.Collections.Generic;

namespace Data
{
    public interface IDataContext<T>
    {
        T GetItem(int id);
        IEnumerable<T> GetAll();
        void AddItem(T item);
        void DeleteItem(int id);
        void UpdateItem(int id, T item);
    }
}
