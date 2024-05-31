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
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection"));

        }

        public DbSet<Book> Books { get; set; }
    }
}
