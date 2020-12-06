using System;
using System.IO;
using Task2.Data;
using Tests.DataFiller;

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
            int option = Console.Read();
            switch (option)
            {
                case 1:
                    break;

            }

        }
    }
}
