using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Library.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books => Set<Book>();
    }
}
