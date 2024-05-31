using BookApp.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookApp.Data
{
    public class DBContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DBContextClass(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Database=Book");

        }

        public DbSet<Book> Books { get; set; }
    }
}
