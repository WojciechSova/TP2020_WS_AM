using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Task2.Data
{
    public class DataContext : ISerializable
    {
        public List<Reader> ReadersList { get; set; }
        public Dictionary<int, Book> BookSet {get; set;}
        public ObservableCollection<BookEvent> BookEvents { get; set; }
        public List<BookState> BookStatesList { get; set; }

        public DataContext()
        {
            ReadersList = new List<Reader>();
            BookSet = new Dictionary<int, Book>();
            BookEvents = new ObservableCollection<BookEvent>();
            BookStatesList = new List<BookState>();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            int index = 0;
            foreach (BookEvent ev in BookEvents)
            {
               ev.GetObjectData(info, context, index);
               index++;
            }
        }
    }
}
