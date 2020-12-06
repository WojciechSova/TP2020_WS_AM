using System;

namespace ConsoleApp
{
    class Program
    {
        static String filePath = "..\\..\\..\\..\\TestResults\\CustomSerialization.saved";
        static void Main(string[] args)
        {
            DataContext dataContext = new DataContext();
            IDataFiller dataFiller = new ConstantFiller();
            IDataRepository dataRepository;
            dataRepository = new Task2.Data.DataRepository(dataFiller, dataContext);
            dataContext.countEvents();
            CustomFormatter customFormatter = new CustomFormatter();    

            using (Stream  stream = new FileStream(filePath, FileMode.Create))
            {
                customFormatter.Serialize(stream, dataContext);
            }

            DataContext dataContext2;

            using (Stream stream = new FileStream(filePath, FileMode.Open))
            {
                dataContext2 = (DataContext)customFormatter.Deserialize(stream);
            }

            Console.WriteLine("Wciśnij:");
            Console.WriteLine("1 - by zapisać stan programu w pliku JSON");
            Console.WriteLine("2 - by zapisać stan programu własną metodą");
            Console.WriteLine("3 - by wczytać stan programu z pliku JSON");
            Console.WriteLine("4 - by wczytać stan programu własną metodą");
            Console.WriteLine("5 - by wyświetlić wszystkie dane");
            Console.WriteLine("6 - by wyłączyć program");
            String option = "0";
            DataContext dataContext = new DataContext();
            DataRepository dataRepository = new DataRepository();
            String filePath;
            while (option != "6")
            {
                option = Console.ReadLine();
                Console.WriteLine(option);
                switch (option)
                {
                    case "1":
                        filePath = "..\\..\\..\\..\\TestResults\\jsonFile.json";
                        JsonSerializer.Serialize(dataContext, filePath);
                        break;
                    case "2":

                        break;
                    case "3":
                        filePath = "..\\..\\..\\..\\TestResults\\jsonFile.json";
                        Console.WriteLine("aaaa3");
                        dataContext = JsonSerializer.Deserialize<DataContext>(filePath);
                        dataRepository = new DataRepository(dataContext);
                        Console.WriteLine(dataRepository.GetAllReaders().ToString());
                        break;
                    case "4":
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
