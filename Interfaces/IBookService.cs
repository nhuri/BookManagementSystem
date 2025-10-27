using BookManagementSystem.DTOs;

namespace BookManagementSystem.Interfaces;

/// <summary>
/// Interface for the book service.
/// Defines the available operations for books in the system.
/// </summary>
public interface IBookService
{
    /// <summary>
    /// Returns a list of books with pagination support.
    /// </summary>
    /// <param name="page">Page number (starting from 1).</param>
    /// <param name="pageSize">Number of books per page.</param>
    /// <returns>List of books according to the parameters.</returns>
    Task<IEnumerable<BookDto>> GetAllAsync(int page, int pageSize);

    /// <summary>
    /// Returns a book by its ID.
    /// </summary>
    /// <param name="id">Book ID.</param>
    /// <returns>The requested book if found, otherwise null.</returns>
    Task<BookDto?> GetByIdAsync(int id);

    /// <summary>
    /// Creates a new book.
    /// </summary>
    /// <param name="dto">Object containing the book details for creation.</param>
    /// <returns>The created book including its ID.</returns>
    Task<BookDto> CreateAsync(CreateBookDto dto);

    /// <summary>
    /// Updates an existing book by ID.
    /// </summary>
    /// <param name="id">Book ID to update.</param>
    /// <param name="dto">Update details.</param>
    /// <returns>True if the update succeeded, otherwise false.</returns>
    Task<bool> UpdateAsync(int id, UpdateBookDto dto);

    /// <summary>
    /// Deletes a book by ID.
    /// </summary>
    /// <param name="id">Book ID to delete.</param>
    /// <returns>True if the deletion succeeded, otherwise false.</returns>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Searches for books by title and/or author.
    /// </summary>
    /// <param name="title">Title to search for (optional).</param>
    /// <param name="author">Author name to search for (optional).</param>
    /// <returns>List of books matching the criteria.</returns>
    Task<IEnumerable<BookDto>> SearchAsync(string? title, string? author);
}
