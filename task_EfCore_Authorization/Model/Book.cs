using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_EfCore_Authorization.Model
{
    internal class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }

        public int PageCount { get; set; }

        public override string ToString()
        {
            return $"({Id}) {Title}, by {Author}, {PageCount} pages";
        }

        public static Book[] AddBooksByDefault()
        {
            Book[] books = new Book[]
            {
                new Book {Title = "Master_i_Margarita", Author = "Mihail Bulgakov", PageCount = 550},
                new Book {Title = "Poviya", Author = "Panas Myrnyj", PageCount = 425},
                new Book {Title = "Bot", Author = "Maks Kidruk", PageCount = 780},
                new Book {Title = "Lord of Rings", Author = "Tolkien", PageCount = 1520},
                new Book {Title = "Kobzar", Author = "Shevchenko", PageCount = 350},
                new Book {Title = "Revizor", Author = "Gogol", PageCount = 280}
            };
            return books ;
        }
    }
}
