namespace BookManagement.API.Models
{
    // מייצג ישות של ספר במסד הנתונים
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; } // תאריך הוצאה לאור של הספר
        public decimal Price { get; set; }
    }
}
