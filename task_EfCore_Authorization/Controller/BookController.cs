using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task_EfCore_Authorization.Model;

namespace task_EfCore_Authorization.Controller
{
    internal class BookController
    {
        private int pageCountPaginate = 5;
        public readonly ApplicationContext context;

        //public BookController(ApplicationContext context)
        //{
        //    this.context = context;
        //}

        // метод для задания начальных данных в БД
        public void EnsurePopulated()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                if (!context.Books.Any())
                {
                    context.Books.AddRange(Book.AddBooksByDefault());
                    context.SaveChanges();
                }
            }              
        }

        // добавление книги
        public void AddBook(string title, string author, int pages)
        {
            using(ApplicationContext context = new ApplicationContext())
            {
                context.Books.Add(new Book { Title = title, Author = author, PageCount = pages });
                context.SaveChanges();
            }
        }

        // вывод книги по id
        public Book GetBookById(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var currentBook = context.Books.FirstOrDefault(e => e.Id== id);
                if(currentBook==null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("No book with such id.");
                    Console.ForegroundColor = ConsoleColor.White;
                    return null;
                }
                return currentBook;
            }
        }

        // вывод всех книг в список
        public IEnumerable<Book> GetAllBooks()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var bookList = context.Books.ToList();
                return bookList;
            }
        }

        // вывод книг с определенной страницы (пагинация
        public IEnumerable<Book> GetAllBooksPaginate(int page = 1)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                // метод Skip пропускает начальное кол-во элементов
                // пропустить следующее: - размер страницы pageSize
                // умножить на текущую страницу page минус 1, и после этого
                // с помощью метода Take берем кол-во элементов, равных pageSize
                // здесь мы хотим отображать по 5 книг, поэтому pageSize = 5
                
                List<Book> list = new List<Book>();
                // проверка, если пагинация пойдет в минус
                try
                {
                    list = context.Books.Skip(pageCountPaginate * (page-1)).Take(pageCountPaginate).ToList();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                return list;
            }
                
        }
    }
}
