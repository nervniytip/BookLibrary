using System;
using System.Text;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Linq;


namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Library Books";
            ConsoleKey KeyPress;
            List<Book> lib = new List<Book>();
            //Анимация!!!!!!магия!
            for (int i = 0; i <= 100; i += 10)
            {
                Console.Write($" {i}%... \r");
                System.Threading.Thread.Sleep(75);
            }
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Выберите операцию:" +
                                                    "\n-1 Добавить книгу. " +
                                                    "\n-2 Показать список книг. " +
                                                    "\n-3 Упорядочить книги по названию." +
                                                    "\n-4 Показать книги с меткой избранное(упор. по названию)."+
                                                    "\n-5 Удаление Книги." +
                                                    "\n-6 Редактирование книги."+
                                                    "\n-<ESC> для выхода из программы.");
                KeyPress = Console.ReadKey(true).Key;
                switch (KeyPress)
                {
                    case (ConsoleKey.D1):               //Add new book
                        Console.Clear();
                        Book book = new Book();
                        AddBook(CreateBook(book));
                        break;
                    case (ConsoleKey.D2):               // Simple Show
                        Console.Clear();
                        ShowBooks(lib);
                        break;
                    case (ConsoleKey.D3):               //  Order by name show
                        Console.Clear();
                        ShowBooksOrderByName(lib);
                        break;
                    case (ConsoleKey.D4):                // Favorite show
                        Console.Clear();
                        ShowFavoriteBooksOrderByName(lib);
                        break;
                    case (ConsoleKey.D5):                // Delete book
                        Console.WriteLine("Ввведите название удаляемой книги :");
                        string name = Console.ReadLine();
                        DeleteBook(lib, name);
                        break;
                    case (ConsoleKey.D6):               // Edint book
                        Console.Clear(); 
                        EditBook(lib);   
                        break;
                    case (ConsoleKey.Escape):           // Exit
                        Console.Clear();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Такой операции нету.");
                        break;
                }
            }
        }
        //---------------------------------------------------------------------------
        private static Book CreateBook(Book book)                               // Создание книги(екземпляра класса Book)
        {
            string name; string category; string autorname; string autorsurname; string autormiddlename;
            bool favor;
            favor = false;
            Start1: // Метка 1 <<<
            Console.WriteLine("Ввведите название книги :");
            name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.Clear();
                Console.WriteLine("Название книги не введено! или состоит из одних пробелов!");
                goto Start1;
            }
            book.BookName = name;
            Console.WriteLine("Введите жанр книги :");
            category = Console.ReadLine();
            book.Category = category;
            Start2: // Метка 2 <<<
            Console.WriteLine("Введите фамилию автора :");
            autorsurname = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(autorsurname))
            {
                Console.Clear();
                Console.WriteLine("Имя автора не введено! или состоит из одних пробелов!");
                goto Start2;
            }
            book.AutorSurname = autorsurname;
            Console.WriteLine("Введите имя автора :");
            autorname = Console.ReadLine();
            book.AutorName = autorname;
            Console.WriteLine("Введите отчество автора(если у него имеется) :");
            autormiddlename = Console.ReadLine();
            book.AutorMiddleName = autormiddlename;
            Console.WriteLine("Добавить книгу в избранное?   Y / N "); //favorit
            ConsoleKey KeyPress = Console.ReadKey(true).Key;
            if (KeyPress == ConsoleKey.Y)
            {
                favor = true;
            }
            book.FavoriteFlag = favor;
            return book;
        }

        private static void AddBook(Book book)                                  //D1 Добавление книги
        {
            Console.Clear();
            try
            {
                //LibraryController.CreateCatalog(); -<<<< пока не нужно читать в LibraryController
                LibraryController.WriteBook(book);
                Console.Write(book);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" добавлена!");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        private static void ShowBooks(List<Book> list)                          //D2 Показать список имеющихся книг.
        {
            Console.Clear();
            try
            {
                LibraryController.Read(list);
                if (list.Count != 0)
                {
                    int count = 1;
                    foreach (var el in list) Console.WriteLine($"{count++}. {el}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Список пуст!");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            list.Clear();
            Console.WriteLine();
        }

        private static void ShowBooksOrderByName(List<Book> list)               //D3 Показать книги упорядоченные по названию.
        {
            Console.Clear();
            try
            {
                LibraryController.Read(list);
                if (list.Count != 0)
                {
                    int count = 1;
                    var sortlist = list.OrderBy(b => b.BookName);
                    foreach (var el in sortlist) Console.WriteLine($"{count++}. {el}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Список пуст!");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            list.Clear();
            Console.WriteLine();
        }

        private static void ShowFavoriteBooksOrderByName(List<Book> list)
        {
            Console.Clear();
            try
            {
                LibraryController.Read(list);
                if (list.Count != 0)
                {
                    int count = 1;
                    var sortlist = list.Where(b=>b.FavoriteFlag == true).Select(b =>b).OrderBy(b => b.BookName);
                    foreach (var el in sortlist) Console.WriteLine($"{count++}. {el}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Список пуст!");
                    Console.ResetColor();
                }


            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            list.Clear();
            Console.WriteLine();
        }   //D4 Показать книги с метой Избранное  и сортировать по названию

        private static void DeleteBook(List<Book> list, string name)            //D5- удаление книги  по названию
        {
            Console.Clear();
            try
            {
                LibraryController.Read(list);
                Book bok = list.Where(b => string.Compare(b.BookName, name, true) == 0).FirstOrDefault();                
                if (bok.BookName != null)
                {
                    list.Remove(bok);
                    LibraryController.ReWriteBook(list);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Книга удалена!");
                    Console.ResetColor();
                    
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Такой книги не существует или название книги введено некореткyо.");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            list.Clear();
            Console.WriteLine();
        }

        private  static void EditBook(List<Book> list)                           //D6-  Редактирование книги по названию? или номеру(индексу в листе)?
        {
            int i = 1;
            int booknumber;
            Console.Clear();
            try
            {
                LibraryController.Read(list);
                if (list.Count != 0)
                {                   
                    foreach (var el in list) Console.WriteLine($"{i++}. {el}");
                    Console.WriteLine();
                    Console.WriteLine("Введите номер книги которую хотите отредактировать."); // сделать проверку на parse или не делать есть же try/catch
                    Int32.TryParse(Console.ReadLine(),out booknumber);
                    Book book = list[booknumber-1];
                    book = CreateBook(book);                  
                    list[booknumber-1] = book;
                    LibraryController.ReWriteBook(list); 
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Книга изменена!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Список пуст!");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            list.Clear();
            Console.WriteLine();
        }


    }
}
