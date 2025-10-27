```csharp
using BookManagementSystem.DTOs;
using BookManagementSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

/// <summary>
/// Returns a list of books with pagination support.
/// </summary>
/// <param name="page">Page number to display (default 1).</param>
/// <param name="pageSize">Number of books per page (default 10).</param>
/// <returns>List of books for the requested page.</returns>
    // GET: api/books?page=1&pageSize=10
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var books = await _bookService.GetAllAsync(page, pageSize);
        return Ok(books);
    }

/// <summary>
/// Finds a book by its ID.
/// </summary>
/// <param name="id">The book ID.</param>
/// <returns>The book details if found, otherwise 404 Not Found.</returns>
    // GET: api/books/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _bookService.GetByIdAsync(id);
        if (book == null)
            return NotFound(new { message = $"Book with ID {id} was not found." });

        return Ok(book);
    }

/// <summary>
    /// Creates a new book.
    /// </summary>
    /// <param name="dto">Book details for creation.</param>
    /// <returns>The created book with a unique ID.</returns>
    // POST: api/books
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdBook = await _bookService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook);
    }

/// <summary>
    /// Updates an existing book by ID.
    /// </summary>
    /// <param name="id">The book ID to update.</param>
    /// <param name="dto">Update details.</param>
    /// <returns>204 No Content if successful, 404 if the book was not found.</returns>
    // PUT: api/books/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateBookDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _bookService.UpdateAsync(id, dto);
        if (!updated)
            return NotFound();

        return NoContent();
    }

/// <summary>
    /// Deletes a book by ID.
    /// </summary>
    /// <param name="id">The book ID to delete.</param>
    /// <returns>204 No Content if successful, 404 if not found.</returns>
    // DELETE: api/books/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _bookService.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }

/// <summary>
    /// Searches for books by title and author.
    /// </summary>
    /// <param name="title">Book title (optional).</param>
    /// <param name="author">Author name (optional).</param>
    /// <returns>List of books matching the criteria.</returns>
    // GET: api/books/search?title=some&author=some
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? title, [FromQuery] string? author)
    {
        var results = await _bookService.SearchAsync(title, author);
        return Ok(results);
    }
}
```
