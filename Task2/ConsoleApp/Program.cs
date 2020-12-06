using System;
using System.Collections.Generic;
using System.IO;
using Task2.Data;
using Tests.DataFiller;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DataContext dataContext = new DataContext();
            IDataFiller dataFiller = new ConstantFiller();
            IDataRepository dataRepository;
            dataRepository = new DataRepository(dataFiller, dataContext);
            dataContext.countEvents();
            CustomFormatter customFormatter = new CustomFormatter();    


            Console.WriteLine("Wciśnij:");
            Console.WriteLine("1 - by zapisać stan programu w pliku JSON");
            Console.WriteLine("2 - by zapisać stan programu własną metodą");
            Console.WriteLine("3 - by wczytać stan programu z pliku JSON");
            Console.WriteLine("4 - by wczytać stan programu własną metodą");
            Console.WriteLine("5 - by wyświetlić wszystkie dane");
            Console.WriteLine("6 - by wyłączyć program");
            string option = "0";
            string filePath;
            while (option != "6")
            {
                option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        filePath = "..\\..\\..\\..\\TestResults\\jsonFile.json";
                        JsonSerializer.Serialize(dataContext, filePath);
                        break;
                    case "2":
                        filePath = "..\\..\\..\\..\\TestResults\\CustomSerialization.saved";
                        using (Stream stream = new FileStream(filePath, FileMode.Create))
                        {
                            customFormatter.Serialize(stream, dataContext);
                        }
                        break;
                    case "3":
                        filePath = "..\\..\\..\\..\\TestResults\\jsonFile.json";
                        dataContext = JsonSerializer.Deserialize<DataContext>(filePath);
                        break;
                    case "4":
                        filePath = "..\\..\\..\\..\\TestResults\\CustomSerialization.saved";
                        using (Stream stream = new FileStream(filePath, FileMode.Open))
                        {
                            dataContext = (DataContext)customFormatter.Deserialize(stream);
                        }
                        break;
                    case "5":
                        dataContext.ReadersList.ForEach(i => Console.WriteLine(i.ToString()));
                        foreach (KeyValuePair<int, Book> temp in dataContext.BookSet)
                        {
                            Console.WriteLine(temp.ToString());
                        }
                        dataContext.BookStatesList.ForEach(i => Console.WriteLine(i.ToString()));
                        foreach (var temp in dataContext.BookEvents)
                        {
                            Console.WriteLine(temp.ToString());
                        }
                        break;
                    default:
                        break;
                }
            }


        }
    }
}
