namespace BookManagementSystem.DTOs;

public class UpdateBookDto
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime PublicationDate { get; set; }
    public decimal Price { get; set; }
}
