using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_EfCore_Authorization.Model
{
    internal class User
    {
        public int Id { get; set; }
        public string? Email {  get; set; }
        public string? HashedPassword { get; set; }
        public string? SaltForHash { get; set; }

       
    }
}
