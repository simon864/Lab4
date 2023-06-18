/**********************************
* Группа: ПИ-221                  *
* Автор: Богулянов Семен          *
* Название: Стандартный ввод вывод*
* Дата: 17.06.2023                *
**********************************/


using System;
using System.IO;

namespace Lab4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("что вы хотите сделать:\n1 - редактировать файл\n2 - индексация файлов в определенной директории");
            int Choice = Convert.ToInt32(Console.ReadLine());
            if (Choice == 1)
            {
                Console.WriteLine("Введите путь к файлу");
                TextFile MyFile = new TextFile();
                MyFile.Path = Console.ReadLine();
                MyFile.Content = File.ReadAllText(MyFile.Path);
                bool Working = true;
                while (Working)
                {
                    Console.WriteLine("выберите действие:\n1 - добавить символы\n2 - стереть символы\n3 - откатиться назад\n4 - показать текст\n5 - выход");
                    string ThisChoice = Console.ReadLine();
                    Caretaker ct = new Caretaker();
                    switch (ThisChoice)
                    {
                        case "1":
                            ct.SaveState(MyFile);
                            Console.WriteLine("какую строчку добавить?");
                            string Appending = Console.ReadLine();
                            File.AppendAllText(MyFile.Path, Appending);
                            MyFile.Content = File.ReadAllText(MyFile.Path);

                            break;
                        case "2":
                            ct.SaveState(MyFile);
                            string ReservePath = "";
                            string DeathPath = "";
                            for (int Position = 0; Position < MyFile.Path.Length - 4; ++Position)
                            {
                                ReservePath += MyFile.Path[Position];
                                DeathPath += MyFile.Path[Position];
                            }
                            ReservePath += "reserve.txt";
                            DeathPath += "deth.txt";
                            Console.WriteLine("сколько символов стереть?");
                            int CountOfDeleting = Convert.ToInt32(Console.ReadLine());
                            FileStream MomFile = new FileStream(MyFile.Path, FileMode.OpenOrCreate);
                            FileStream SonFile = new FileStream(ReservePath, FileMode.OpenOrCreate);
                            MomFile.CopyTo(SonFile, (int)MomFile.Length - CountOfDeleting);
                            MomFile.Flush();
                            MomFile.Close();
                            SonFile.Flush();
                            SonFile.Close();
                            File.Replace(MyFile.Path, ReservePath, DeathPath);
                            File.Delete(DeathPath);
                            File.Move(ReservePath, MyFile.Path);
                            MyFile.Content = File.ReadAllText(MyFile.Path);

                            break;
                        case "3":

                            ct.RestoreState(MyFile);
                            FileStream SecondFile = new FileStream(MyFile.Path + ".txt", FileMode.OpenOrCreate);
                            SecondFile.Flush();
                            SecondFile.Close();
                            File.AppendAllText(MyFile.Path + ".txt", MyFile.Content);
                            File.Replace(MyFile.Path, MyFile.Path + ".txt", "1");
                            File.Replace(MyFile.Path + ".txt", MyFile.Path, "1");
                            break;
                        case "4":
                            Console.Clear();
                            Console.WriteLine(File.ReadAllText(MyFile.Path));
                            Console.WriteLine("\n\n\nнажмите любую клавишу, чтобы продолжить");
                            Console.ReadKey();
                            break;
                        case "5":
                            Working = false;
                            break;
                        default:
                            Console.WriteLine("неверный выбор");
                            break;
                    }
                }
            }
            else if (Choice == 2)
            {
                bool OneMoreWord = true;
                Console.WriteLine("Введите путь к каталогу с файлами");
                string MyPath = Console.ReadLine();
                Console.WriteLine("вводите ключевые слова. Для остановки введите end");
                string[] MyKeyWords = new string[10];
                string NextWord;
                int Turn = 0;
                while (OneMoreWord)
                {
                    NextWord = Console.ReadLine();
                    if (NextWord == "end")
                    {
                        OneMoreWord = false;
                    }
                    else
                    {
                        MyKeyWords[Turn] = NextWord;
                    }
                    Turn += 1;
                }
                Search.Searching(MyPath, MyKeyWords);
            }
            else
            {
                Console.WriteLine("неверный выбор");
            }
            Console.ReadKey();
        }
    }
}