using System;
using System.ComponentModel.DataAnnotations;

namespace BookManagementSystem.DTOs
{
    // ייצוג של ספר להעברה בין שכבות (Data Transfer Object) עם ולידציית נתונים.
    public class BookDto
    {
        // מזהה ייחודי של הספר
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; }
        public decimal Price { get; set; }
    }
}
