namespace BookManagement.API.Models
{
    // Represents a book entity in the database
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; } // Book publication date
        public decimal Price { get; set; }
    }
}
