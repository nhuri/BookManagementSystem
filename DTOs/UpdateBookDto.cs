using System;
using System.ComponentModel.DataAnnotations;

namespace BookManagementSystem.DTOs
{
    // DTO for creating a new book with data validation
    public class CreateBookDto
    {
        // Book title, required, minimum length 1 and maximum 200 characters
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Title length must be between 1 and 200 characters")]
        public string Title { get; set; } = string.Empty;

        // Author name, required, minimum length 1 and maximum 100 characters
        [Required(ErrorMessage = "Author is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Author length must be between 1 and 100 characters")]
        public string Author { get; set; } = string.Empty;

        // Publication date of the book, required, must be a past date
        [Required(ErrorMessage = "PublicationDate is required")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(CreateBookDto), nameof(ValidatePublicationDate))]
        public DateTime PublicationDate { get; set; }

        // Book price, required, must be a positive number greater than 0
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }

        // Custom validation to ensure that the publication date is not in the future
        public static ValidationResult? ValidatePublicationDate(DateTime publicationDate, ValidationContext context)
        {
            if (publicationDate > DateTime.Now)
                return new ValidationResult("PublicationDate cannot be in the future");
            return ValidationResult.Success;
        }
    }
}
