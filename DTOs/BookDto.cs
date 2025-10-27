using System;
using System.ComponentModel.DataAnnotations;

namespace BookManagementSystem.DTOs
{
  // Representation of a book for data transfer between layers (Data Transfer Object) with data validation.

    public class BookDto
    {
        // Unique identifier of the book
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; }
        public decimal Price { get; set; }
    }
}
