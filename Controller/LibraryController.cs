using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace ConsoleApp2
{
    static class LibraryController
    {
        //------------->для записи в каталоги User или Program File нужны права или нужно запускать от имени администратора!!!! <<<---------------

        //private const string path = "C:\\Users\\User\\AppData\\Local\\HomeLibrary\\base.dat";         // "C:\\Program Files (x86)\\HomeLibrary\\base.dat";

        //private const string checkcatalog = "C:\\Users\\User\\AppData\\Local\\HomeLibrary";          // "C:\\Program Files (x86)\\HomeLibrary";

        private const string basefile = "base.bin";

        /*internal static void CreateCatalog() // создание каталога
        {
            if (!Directory.Exists(checkcatalog))
            {
                Directory.CreateDirectory(checkcatalog);
            }
        }*/

        /// <summary>
        /// Добдавление(сохранение) книги(Book) в файл 
        /// </summary>
        /// <param name="book"></param>
        internal static void WriteBook(Book book) //Добавление книги
        {
            using (BinaryWriter bw = new BinaryWriter(File.Open(basefile, FileMode.Append)))
            {
                bw.Write(book.BookName);
                bw.Write(book.Category);
                bw.Write(book.AutorSurname);
                bw.Write(book.AutorName);
                bw.Write(book.AutorMiddleName);
                bw.Write(book.FavoriteFlag);
            }
        }
        /// <summary>
        /// Перезаписывание List Book  в файл
        /// </summary>
        /// <param name="list"></param>
        internal static void ReWriteBook(List<Book> list) // перезапись книг 
        {
            using (BinaryWriter bw = new BinaryWriter(File.Open(basefile, FileMode.Truncate)))
            {
                foreach (var b in list)
                {
                    bw.Write(b.BookName);
                    bw.Write(b.Category);
                    bw.Write(b.AutorSurname);
                    bw.Write(b.AutorName);
                    bw.Write(b.AutorMiddleName);
                    bw.Write(b.FavoriteFlag);
                }
            }
        }
        /// <summary>
        /// Чтение данных(книг) из базы в List Book
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static List<Book> Read(List<Book> list) // прочтение файла в List<Book>
        {
            using (BinaryReader br = new BinaryReader(File.Open(basefile, FileMode.Open)))
            {
                while (br.PeekChar() > -1)
                {
                    string name = br.ReadString();
                    string category = br.ReadString();
                    string autorsurname = br.ReadString();
                    string autorname = br.ReadString();
                    string autormiddlename = br.ReadString();
                    bool favorite = br.ReadBoolean();
                    list.Add(new Book(name, category, autorsurname, autorname, autormiddlename,favorite)); 
                }
            }
            return list;
        }        
    }
}