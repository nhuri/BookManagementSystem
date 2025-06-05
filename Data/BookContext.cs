using BookManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagementSystem.Data
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; } = null!;
    }
}
