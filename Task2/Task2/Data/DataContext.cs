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

        public int BookEventsLength { get; set; }

        public DataContext()
        {
            ReadersList = new List<Reader>();
            BookSet = new Dictionary<int, Book>();
            BookEvents = new ObservableCollection<BookEvent>();
            BookStatesList = new List<BookState>();
        }

        public void countEvents()
        {
            BookEventsLength = BookEvents.Count;
        }

        public DataContext(SerializationInfo serializationInfo)
        {
            ObservableCollection<BookEvent> events = new ObservableCollection<BookEvent>();

            BookEventsLength = serializationInfo.GetInt32("BookEventsLength");

            for (int i = 0; i < BookEventsLength; i++)
            {
                if (serializationInfo.GetString("Type_" + i.ToString()).Equals("Return"))
                {
                    events.Add(new BookReturn(
                        serializationInfo.GetString("BookEventId_" + i.ToString()),
                    new Reader(
                        serializationInfo.GetString("ReaderId_" + i.ToString()), serializationInfo.GetString("Name_" + i.ToString()),
                        serializationInfo.GetString("Surname_" + i.ToString()), serializationInfo.GetString("PersonalId_" + i.ToString())),
                        new BookState(
                            serializationInfo.GetString("BookStateId_" + i.ToString()),
                            new Book(
                                serializationInfo.GetString("BookId_" + i.ToString()),
                                serializationInfo.GetString("Isbn_" + i.ToString()), serializationInfo.GetString("Author_" + i.ToString()),
                            serializationInfo.GetString("Title_" + i.ToString()), serializationInfo.GetString("Description_" + i.ToString())),
                        serializationInfo.GetBoolean("Available_" + i.ToString()), serializationInfo.GetDateTime("BuyingDate_" + i.ToString())),
                        serializationInfo.GetDateTime("EventTime_" + i.ToString())));
                }
                else
                {
                    events.Add(new BookRent(
                        serializationInfo.GetString("BookEventId_" + i.ToString()),
                        new Reader(
                            serializationInfo.GetString("ReaderId_" + i.ToString()), serializationInfo.GetString("Name_" + i.ToString()),
                            serializationInfo.GetString("Surname_" + i.ToString()), serializationInfo.GetString("PersonalId_" + i.ToString())),
                        new BookState(
                            serializationInfo.GetString("BookStateId_" + i.ToString()),
                            new Book(
                                serializationInfo.GetString("BookId_" + i.ToString()),
                                serializationInfo.GetString("Isbn_" + i.ToString()), serializationInfo.GetString("Author_" + i.ToString()),
                                serializationInfo.GetString("Title_" + i.ToString()), serializationInfo.GetString("Description_" + i.ToString())),
                        serializationInfo.GetBoolean("Available_" + i.ToString()), serializationInfo.GetDateTime("BuyingDate_" + i.ToString())),
                        serializationInfo.GetDateTime("EventTime_" + i.ToString())));
                }
            }
            BookEvents = events;
            
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            int index = 0;
            info.AddValue("BookEventsLength_", BookEventsLength);
            foreach (BookEvent ev in BookEvents)
            {
                if (typeof(BookRent) == ev.GetType())
                {
                    info.AddValue("Type_" + index.ToString() + "_" , "Rent");
                }
                else
                {
                    info.AddValue("Type_" +index.ToString() + "_", "Return");
                }
                ev.GetObjectData(info, context, index);
                index++;
            }
        }
    }
}
