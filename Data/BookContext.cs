using BookManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagementSystem.Data
{
    /// <summary>
    /// DbContext for the book management system.
    /// Responsible for communication with the database.
    /// </summary>
    public class BookContext : DbContext
    {
        /// <summary>
        /// Initializes the database context with defined options.
        /// </summary>
        /// <param name="options">Configuration options for the DbContext.</param>
        public BookContext(DbContextOptions<BookContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// The Books set in the database.
        /// Represents the table of books.
        /// </summary>
        public DbSet<Book> Books { get; set; } = null!;
    }
}
