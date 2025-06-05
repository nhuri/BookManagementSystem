namespace BookManagementSystem.DTOs;

// DTO לשם יצירת ספר חדש.
public class CreateBookDto
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime PublicationDate { get; set; }  // תאריך הפרסום של הספר 
    public decimal Price { get; set; }
}
