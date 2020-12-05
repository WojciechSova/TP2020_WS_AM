using System;
using System.Collections.Generic;
using Task2.Data;

namespace Tests.DataFiller
{
    class RandomFiller : IDataFiller
    {
        public void Fill(DataContext dataContext)
        {
            Random random = new Random();
            DateTime start = new DateTime(1945, 10, 19);
            int range = (DateTime.Today - start).Days - 3652;

            List<string> names = new List<string> {"Wojciech", "Artur", "Kaja", "Kamil", "Aleksandra", "Michal", "Patrycja", "Kinga", "Paulina", "Jakub"};
            List<string> surnames = new List<string> {"Sowa", "Madaj", "Orzechowski", "Nowak", "Kowalczyk", "Chlebek", "Godlewski", "Adamecki", "Bury", "Grabski"};
            List<string> authors = new List<string> { "William Shakespeare", "Agatha Christie", "Barbara Cartland", "Danielle Steel", "Harold Robbins", "Georges Simenon", 
                "Enid Blyton", "Sidney Sheldon", "J. K. Rowling", "Gilbert Patten"};
            List<string> titles = new List<string> { "The Hunger Games", "Harry Potter", "To Kill a Mockingbird", "Pride and Prejudice", "Twilight",
                "The Book Thief", "Animal Farm", "The Chronicles of Narnia", "The Giving Tree", "Wuthering Heights"};
            List<string> description = new List<string> { "Nice", "Amazing", "Good", "Shocking", "Breathtaking", "Surprising", "Exciting", 
                "Mind-blowing", "Sensational", "Extraordinary"};

            for (int i = 0; i < 7; i++)
            {
                dataContext.BookSet.Add(i, new Book(generateISBN(), authors[random.Next(authors.Count)], titles[random.Next(titles.Count)], 
                    description[random.Next(description.Count)]));
                dataContext.ReadersList.Add(new Reader(names[random.Next(names.Count)], surnames[random.Next(surnames.Count)], generatePersonalID()));
                dataContext.BookStatesList.Add(new BookState(dataContext.BookSet[i], true, start.AddDays(random.Next(range))));
            }

            for (int i = 0; i < 5; i++)
            {
                DateTime startRent = new DateTime(2010, 10, 19);
                int rangeRent = (DateTime.Today - startRent).Days;
                BookState bookState = dataContext.BookStatesList[random.Next(7)];
                while (!bookState.Available)
                {
                    bookState = dataContext.BookStatesList[random.Next(7)];
                }
                Reader reader = dataContext.ReadersList[random.Next(7)];
                DateTime dateRent = startRent.AddDays(random.Next(rangeRent));
                dataContext.BookEvents.Add(new BookRent(reader, bookState, dateRent));
                if (random.Next(2) == 1)
                {
                    int rangeReturn = (DateTime.Now - dateRent).Days;
                    dataContext.BookEvents.Add(new BookReturn(reader, bookState, dateRent.AddDays(random.Next(rangeReturn))));
                }
            }
        }

        private string generateISBN()
        {
            Random r = new Random();
            return r.Next(1000) + "-" + r.Next(1000) + "-" + r.Next(1000);
        }

        private int generatePersonalID()
        {
            Random r = new Random();
            return int.Parse("" + r.Next(10) + r.Next(10) + r.Next(10) + r.Next(10) + r.Next(10) + r.Next(10) + r.Next(10) 
                + r.Next(10) + r.Next(10));
        }
    }
}
