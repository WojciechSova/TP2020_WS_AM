using System;
using System.Collections.Generic;
using System.IO;
using Task2.Data;
using Task2.DataModel;
using Task2.Serializers;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ClassA classA = new ClassA("classA", 789654, true);
            ClassB classB = new ClassB("classB", 123456897, 777);
            ClassC classC = new ClassC("classC", 98765188);

            classA.ClassB = classB;
            classA.ClassC = classC;
            classB.ClassA = classA;
            classB.ClassC = classC;
            classC.ClassA = classA;
            classC.ClassB = classB;

            Bookshelf bookshelf = new Bookshelf(
                new List<Book> {
                    new Book("123-456", 9.9), new Book("987-654", 1.1)},
                new BookGenres[] {
                    new BookGenres("Drama"), new BookGenres("Action"), new BookGenres("Poetry") }
                );

            CustomFormatter customFormatter;    


            Console.WriteLine("Wciśnij:");
            Console.WriteLine("1 - by zapisać stan programu w pliku JSON - ClassA");
            Console.WriteLine("2 - by zapisać stan programu w pliku JSON - Bookshelf");
            Console.WriteLine("3 - by zapisać stan programu własną metodą - ClassA");
            Console.WriteLine("4 - by zapisać stan programu własną metodą - Bookshelf");
            Console.WriteLine("5 - by wczytać stan programu z pliku JSON - ClassA");
            Console.WriteLine("6 - by wczytać stan programu z pliku JSON - Bookshelf");
            Console.WriteLine("7 - by wczytać stan programu własną metodą - ClassA");
            Console.WriteLine("8 - by wczytać stan programu własną metodą - Bookshelf");
            Console.WriteLine("9 - by wyłączyć program");
            string option = "10";
            string filePath;
            while (option != "9")
            {
                customFormatter = new CustomFormatter();
                option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        filePath = "..\\..\\..\\..\\TestResults\\jsonFileClassA.json";
                        JsonSerializer.Serialize(classA, filePath);
                        break;
                    case "2":
                        filePath = "..\\..\\..\\..\\TestResults\\jsonFileBookshelf.json";
                        JsonSerializer.Serialize(bookshelf, filePath);
                        break;
                    case "3":
                        filePath = "..\\..\\..\\..\\TestResults\\CustomSerializationClassA.txt";
                        using (Stream stream = new FileStream(filePath, FileMode.Create))
                        {
                            customFormatter.Serialize(stream, classA);
                        }
                        break;
                    case "4":
                        filePath = "..\\..\\..\\..\\TestResults\\CustomSerializationBookshelf.txt";
                        using (Stream stream = new FileStream(filePath, FileMode.Create))
                        {
                            customFormatter.Serialize(stream, bookshelf);
                        }
                        break;
                    case "5":
                        filePath = "..\\..\\..\\..\\TestResults\\jsonFileClassA.json";
                        ClassA classADeserialized = JsonSerializer.Deserialize<ClassA>(filePath);
                        break;
                    case "6":
                        filePath = "..\\..\\..\\..\\TestResults\\jsonFileBookshelf.json";
                        Bookshelf bookshelfDeserialized = JsonSerializer.Deserialize<Bookshelf>(filePath);
                        break;
                    case "7":
                        filePath = "..\\..\\..\\..\\TestResults\\CustomSerializationClassA.txt";
                        using (Stream stream = new FileStream(filePath, FileMode.Open))
                        {
                            ClassA deserialized = (ClassA)customFormatter.Deserialize(stream);
                        }
                        break;
                    case "8":
                        filePath = "..\\..\\..\\..\\TestResults\\CustomSerializationBookshelf.txt";
                        using (Stream stream = new FileStream(filePath, FileMode.Open))
                        {
                            Bookshelf deserialized = (Bookshelf)customFormatter.Deserialize(stream);
                        }
                        break;
                    default:
                        break;
                }
            }


        }
    }
}
