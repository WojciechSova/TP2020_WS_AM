using System;
using Task2.Data;

namespace Tests.DataFiller
{
    public class ConstantFiller : IDataFiller
    {
        public void Fill(DataContext dataContext)
        {
            Random random = new Random();
            DateTime start = new DateTime(1945, 10, 19);
            int range = (DateTime.Today - start).Days - 3652;

            for (int i = 0; i < 7; i++)
            {
                string tmp = i.ToString();
                dataContext.BookSet.Add(i, new Book("123-456-" + tmp, "Author Wojciech" + tmp, "Catchy Title " + tmp, "Interesting book " + tmp));
                dataContext.ReadersList.Add(new Reader("Arciech" + tmp, "Sodaj" + tmp, 1000000 + i));
                dataContext.BookStatesList.Add(new BookState(dataContext.BookSet[i], true, start.AddDays(random.Next(range))));
            }

            dataContext.BookEvents.Add(new BookRent(dataContext.ReadersList[3], dataContext.BookStatesList[0], new DateTime(2017, 11, 14)));
            dataContext.BookEvents.Add(new BookRent(dataContext.ReadersList[3], dataContext.BookStatesList[2], new DateTime(2018, 2, 10)));
            dataContext.BookEvents.Add(new BookRent(dataContext.ReadersList[5], dataContext.BookStatesList[6], new DateTime(2015, 4, 24)));
            dataContext.BookEvents.Add(new BookRent(dataContext.ReadersList[6], dataContext.BookStatesList[1], new DateTime(2019, 9, 1)));
            dataContext.BookEvents.Add(new BookRent(dataContext.ReadersList[0], dataContext.BookStatesList[3], new DateTime(2016, 12, 28)));

            dataContext.BookEvents.Add(new BookReturn(dataContext.ReadersList[5], dataContext.BookStatesList[6], new DateTime(2015, 8, 16)));
            dataContext.BookEvents.Add(new BookReturn(dataContext.ReadersList[0], dataContext.BookStatesList[3], new DateTime(2017, 1, 30)));
        }
    }
}
