using BookManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagementSystem.Data
{
    /// DbContext עבור מערכת ניהול הספרים.
    /// אחראי על התקשורת עם מסד הנתונים.
    public class BookContext : DbContext
    {
        /// אתחול הקשר למסד הנתונים עם אפשרויות מוגדרות.
        /// <param name="options">אפשרויות קונפיגורציה ל-DbContext.</param>
        public BookContext(DbContextOptions<BookContext> options)
            : base(options)
        {
        }

        /// קבוצת ה - Books במסד הנתונים 
        /// מייצגת את הטבלה של הספרים.
        public DbSet<Book> Books { get; set; } = null!;
    }
}
