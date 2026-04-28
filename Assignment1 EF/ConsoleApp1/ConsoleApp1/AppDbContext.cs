using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
    public class AppDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=ReadMoreBookstore;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}