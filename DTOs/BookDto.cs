namespace BookManagementSystem.DTOs;

// ייצוג של ספר להעברה בין שכבות (Data Transfer Object).
public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime PublicationDate { get; set; }  // תאריך הפרסום של הספר
    public decimal Price { get; set; }
}
