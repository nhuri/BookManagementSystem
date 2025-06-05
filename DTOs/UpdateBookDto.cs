namespace BookManagementSystem.DTOs;

//  DTO לעדכון של ספר
public class UpdateBookDto
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime PublicationDate { get; set; }  // הוצאה לאור של הספר
    public decimal Price { get; set; }
}
