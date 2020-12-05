using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Task2.Data
{
    public class DataContext
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
    }
}
