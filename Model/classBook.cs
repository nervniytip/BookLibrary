using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ConsoleApp2
{
    struct Book
    {
        public string BookName { get; set; }
        public string Category { get; set; }
        public string AutorName { get; set; }
        public string AutorSurname { get; set; }
        public string AutorMiddleName { get; set; }
        public bool FavoriteFlag { get; set; }


        public Book(string BookName, string Category, string AutorSurname, string AutorName, string AutorMiddleName = null, bool FavoriteFlag = false) :this()
        {
            this.BookName = BookName;
            this.Category = Category;
            this.AutorSurname = AutorSurname;
            this.AutorName = AutorName;
            this.AutorMiddleName = AutorMiddleName;
            this.FavoriteFlag = FavoriteFlag;
        }

        // допилить правильное отображение книги (типо чтобы начиналось с большой бувы если введено с маленькой)
        public override string ToString()
        {
            return string.Format($"Kнига {this.BookName} автор : {this.AutorSurname} {this.AutorName} {this.AutorMiddleName ?? ""}" +
                    $"( жанр : {this.Category} )" + (this.FavoriteFlag == true ? $" {(char)42}" :"" )); 
        }
    }
}
