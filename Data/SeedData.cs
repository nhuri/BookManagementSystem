//using BookManagement.Models;
using BookManagement.API.Models;

namespace BookManagementSystem.Data
{
    /// <summary>
    /// Static class for initializing default data in the database.
    /// </summary>
    public static class SeedData
    {
        /// <summary>
        /// Initializes the database with sample books if it is empty.
        /// </summary>
        /// <param name="context">The database context (BookContext).</param>
        public static void Initialize(BookContext context)
        {
            // If there are already books in the database, no need to seed again.
            if (context.Books.Any())
                return;

            // List of sample books to be added to the database if it is empty
            var books = new List<Book>
            {
                new Book { Title = "Clean Code", Author = "Robert C. Martin", PublicationDate = new DateTime(2008, 8, 1), Price = 180 },
                new Book { Title = "The Pragmatic Programmer", Author = "Andrew Hunt", PublicationDate = new DateTime(1999, 10, 30), Price = 150 },
                new Book { Title = "C# in Depth", Author = "Jon Skeet", PublicationDate = new DateTime(2019, 4, 2), Price = 200 },
                new Book { Title = "Design Patterns", Author = "Erich Gamma", PublicationDate = new DateTime(1994, 11, 10), Price = 250 },
                new Book { Title = "Refactoring", Author = "Martin Fowler", PublicationDate = new DateTime(1999, 7, 8), Price = 175 },
                new Book { Title = "Code Complete", Author = "Steve McConnell", PublicationDate = new DateTime(2004, 6, 9), Price = 210 },
                new Book { Title = "Domain-Driven Design", Author = "Eric Evans", PublicationDate = new DateTime(2003, 8, 30), Price = 230 },
                new Book { Title = "Effective C#", Author = "Bill Wagner", PublicationDate = new DateTime(2017, 1, 1), Price = 165 },
                new Book { Title = "Pro ASP.NET Core", Author = "Adam Freeman", PublicationDate = new DateTime(2020, 5, 20), Price = 280 },
                new Book { Title = "You Don't Know JS", Author = "Kyle Simpson", PublicationDate = new DateTime(2015, 3, 15), Price = 90 },
                new Book { Title = "Soft Skills", Author = "John Sonmez", PublicationDate = new DateTime(2014, 12, 1), Price = 120 },
                new Book { Title = "Intro to Algorithms", Author = "Thomas H. Cormen", PublicationDate = new DateTime(2009, 9, 15), Price = 300 },
                new Book { Title = "Working with Legacy Code", Author = "Michael Feathers", PublicationDate = new DateTime(2004, 10, 1), Price = 195 },
                new Book { Title = "The Mythical Man-Month", Author = "Frederick Brooks", PublicationDate = new DateTime(1975, 1, 1), Price = 140 },
                new Book { Title = "Patterns of Enterprise Apps", Author = "Martin Fowler", PublicationDate = new DateTime(2002, 11, 15), Price = 220 }
            };

            // Add the books to the database and save changes
            context.Books.AddRange(books);
            context.SaveChanges();
        }
    }
}
