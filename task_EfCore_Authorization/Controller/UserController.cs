using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task_EfCore_Authorization.Model;
using task_EfCore_Authorization.Helpers;

namespace task_EfCore_Authorization.Controller
{
    internal class UserController
    {
        public readonly ApplicationContext context;

        //public UserController(ApplicationContext context)
        //{
        //    this.context=context;
        //}

        // Регистрация пользователя
        public bool Registration(string email, string password)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                if (context.Users.Any(e => e.Email.Equals(email)))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("User with the such Email already exist.");
                    Console.ForegroundColor = ConsoleColor.White;
                    return false;
                }

                string salt = SecurityHelper.GenerateSalt(12363);
                string hashedPassword = SecurityHelper.HashPassword(password, salt, 12363, 70);

                User user = new User { Email = email, HashedPassword = hashedPassword, SaltForHash = salt };
                context.Users.Add(user);
                context.SaveChanges();

                return true;
            }               
        }

        // Фвторизация пользователя
        public bool Authorization(string email, string password)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var currUser = context.Users.FirstOrDefault(e => e.Email.Equals(email));
                if (currUser != null)
                {
                    string hashedPassword = SecurityHelper.HashPassword
                        (password, currUser.SaltForHash, 12363, 70);

                    if (currUser.HashedPassword.Equals(hashedPassword))
                    {
                        return true;
                    }
                }
                return false;
            }               
        }
    }
}
