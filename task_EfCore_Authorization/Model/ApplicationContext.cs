using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_EfCore_Authorization.Model
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-7GU49OD\SQLEXPRESS; Database=UserAuthorization; Trusted_Connection=True; TrustServerCertificate=True;");
            optionsBuilder.LogTo(e => Debug.WriteLine(e), new[] { RelationalEventId.CommandExecuted });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(e => e.Email).IsRequired();
            modelBuilder.Entity<User>().Property(e=>e.HashedPassword).IsRequired();
            modelBuilder.Entity<User>().Property(e=>e.SaltForHash).IsRequired();
            modelBuilder.Entity<Book>().Property(e=>e.Title).IsRequired();

            modelBuilder.Entity<User>().HasAlternateKey(e => e.Email);

            modelBuilder.Entity<Book>().Property(e => e.Author).HasDefaultValue("no author");
            modelBuilder.Entity<Book>().Property(e => e.Author).HasDefaultValue(0);
        }
    }
}
