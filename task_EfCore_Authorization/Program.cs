using task_EfCore_Authorization.Controller;
using task_EfCore_Authorization.Model;

namespace task_EfCore_Authorization
{
    internal class Program
    {
        static BookController bookController;
        static UserController userController;
        static void Main(string[] args)
        {
            bookController = new BookController();
            userController = new UserController();

            while (true)
            {
                Console.WriteLine("1.Register. \n2.Login. \n3.Exit.");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        {
                            Console.WriteLine("Enter userName:");
                            string userName = Console.ReadLine();
                            Console.WriteLine("Enter password:");
                            string password = Console.ReadLine();

                            if (userController.Registration(userName, password))
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Great, You was registered!");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Error.");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            break;
                        }
                    case "2":
                        {
                            Console.WriteLine("Enter userName:");
                            string userName = Console.ReadLine();
                            Console.WriteLine("Enter password:");
                            string password = Console.ReadLine();

                            if (userController.Authorization(userName, password))
                            {
                                // если авторизация прошла успешно, сразу переход на
                                // меню с книгами
                                ShowBookMenu();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Error.");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            break;
                        }
                    case "3":
                        {
                            return;
                        }
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Not valid input");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            }
        }

        // меню книг, для тех пользователей, кто прошел авторизацию
        static void ShowBookMenu()
        {
            // вызываем начальное заполнение
            bookController.EnsurePopulated();           

            while (true)
            {
                Console.WriteLine("1.Show all books." +
                    "\n2.Get book by id." +
                    "\n3.Log Out.");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        {
                            BookShow();
                            break;
                        }
                    case "2":
                        {
                            Console.WriteLine("Enter book id:");
                            int bookId = 0;
                            try
                            {
                                bookId = int.Parse(Console.ReadLine());
                            }
                            catch(Exception ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(ex.Message);
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            var currBook = bookController.GetBookById(bookId);
                            Console.WriteLine(currBook);
                            break;
                        }
                    case "3":
                        {
                            return;
                        }
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Not valid input");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                }
            }
        }

        // метод показа книг с пагинацией
        private static void BookShow(int page = 1)
        {
            Console.WriteLine($"Page number: {page}");

            // в GetBooks() передаем номер страницы, из которой хотим
            // получить книги
            var allBooks = bookController.GetAllBooksPaginate(page).ToList();
            foreach ( var book in allBooks )
            {
                Console.WriteLine(book.ToString());
            }
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("***Next page - press 1***");
            Console.ForegroundColor = ConsoleColor.White;
            
            if (page > 1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("***Previous page - press 2***");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Exit - press 3");
            Console.ForegroundColor = ConsoleColor.White;

            int input = int.Parse(Console.ReadLine());

            // если инпут >= 3 - просто выходим из текущего метода
            if (input >= 3)
            {
                return;
            }
            // иначе (если юзер нажал назад или вперед) - 
            // заново вызываем текущий метод (рекурсия), и говорим
            // page += - если инпут = 1 то возвращаем 1, иначе минус один
            BookShow(page += input == 1 ? 1 : -1);
        }
    }
}
