using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Task1.Data;

namespace Task1
{
    public class DataContext
    {
        List<Reader> ReadersList { get; set; }
        Dictionary<string, BookState> BookSet{get; set;}
        ObservableCollection<BookEvent> BookEvents { get; set; }
        List<BookState> BookStatesList { get; set; }

        public DataContext()
        {
            ReadersList = new List<Reader>();
            BookSet = new Dictionary<string, BookState>();
            BookEvents = new ObservableCollection<BookEvent>();
            BookStatesList = new List<BookState>();
        }
    }
}
