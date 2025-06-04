using Microsoft.EntityFrameworkCore;

namespace BookManagement.API.Models
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options)
            : base(options) {}

        public DbSet<Book> Books { get; set; }
    }
}
