using BookApp.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options) { }
        
        public DbSet<Book> Books { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
