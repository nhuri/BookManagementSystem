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
/// מחזיר רשימת ספרים עם תמיכה בפגינציה.
/// </summary>
/// <param name="page">מספר עמוד לתצוגה (ברירת מחדל 1).</param>
/// <param name="pageSize">מספר ספרים בכל עמוד (ברירת מחדל 10).</param>
/// <returns>רשימת ספרים בעמוד המבוקש.</returns>
    // GET: api/books?page=1&pageSize=10
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var books = await _bookService.GetAllAsync(page, pageSize);
        return Ok(books);
    }

/// <summary>
/// מחפש ספר לפי מזהה.
/// </summary>
/// <param name="id">מזהה הספר.</param>
/// <returns>פרטי הספר אם נמצא, אחרת 404 Not Found.</returns>
    // GET: api/books/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _bookService.GetByIdAsync(id);
        if (book == null)
            return NotFound();

        return Ok(book);
    }

/// <summary>
    /// יוצר ספר חדש.
    /// </summary>
    /// <param name="dto">פרטי הספר ליצירה.</param>
    /// <returns>הספר שנוצר עם מזהה ייחודי.</returns>
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
    /// מעדכן ספר קיים לפי מזהה.
    /// </summary>
    /// <param name="id">מזהה הספר לעדכון.</param>
    /// <param name="dto">פרטי העדכון.</param>
    /// <returns>204 No Content אם הצליח, 404 אם לא נמצא הספר.</returns>
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
    /// מוחק ספר לפי מזהה.
    /// </summary>
    /// <param name="id">מזהה הספר למחיקה.</param>
    /// <returns>204 No Content אם הצליח, 404 אם לא נמצא.</returns>
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
    /// מחפש ספרים לפי שם וכותב.
    /// </summary>
    /// <param name="title">שם הספר (אופציונלי).</param>
    /// <param name="author">שם המחבר (אופציונלי).</param>
    /// <returns>רשימת ספרים התואמים לקריטריונים.</returns>
    // GET: api/books/search?title=some&author=some
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? title, [FromQuery] string? author)
    {
        var results = await _bookService.SearchAsync(title, author);
        return Ok(results);
    }
}