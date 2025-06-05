using System;
using System.ComponentModel.DataAnnotations;

namespace BookManagementSystem.DTOs
{
    // DTO לשם יצירת ספר חדש עם ולידציית נתונים
    public class CreateBookDto
    {
        // כותרת הספר, חובה למלא, אורך מינימלי 1 ומקסימלי 200 תווים
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Title length must be between 1 and 200 characters")]
        public string Title { get; set; } = string.Empty;

        // שם המחבר, חובה למלא, אורך מינימלי 1 ומקסימלי 100 תווים
        [Required(ErrorMessage = "Author is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Author length must be between 1 and 100 characters")]
        public string Author { get; set; } = string.Empty;

        // תאריך הפרסום של הספר, חובה, חייב להיות תאריך בעבר
        [Required(ErrorMessage = "PublicationDate is required")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(CreateBookDto), nameof(ValidatePublicationDate))]
        public DateTime PublicationDate { get; set; }

        // מחיר הספר, חובה, חייב להיות מספר חיובי גדול מ-0
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }

        // ולידציה מותאמת אישית לוודא שתאריך הפרסום הוא לא בעתיד
        public static ValidationResult? ValidatePublicationDate(DateTime publicationDate, ValidationContext context)
        {
            if (publicationDate > DateTime.Now)
                return new ValidationResult("PublicationDate cannot be in the future");
            return ValidationResult.Success;
        }
    }
}
