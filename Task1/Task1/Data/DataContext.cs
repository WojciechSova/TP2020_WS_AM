using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Task1.Data
{
    public class DataContext
    {
        public List<Reader> ReadersList { get; set; }
        public Dictionary<string, string> BookSet{get; set;}
        public ObservableCollection<BookEvent> BookEvents { get; set; }
        public List<BookState> BookStatesList { get; set; }

        public DataContext()
        {
            ReadersList = new List<Reader>();
            BookSet = new Dictionary<string, string>();
            BookEvents = new ObservableCollection<BookEvent>();
            BookStatesList = new List<BookState>();
        }
    }
}
