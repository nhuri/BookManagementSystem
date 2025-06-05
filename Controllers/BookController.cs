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

    // GET: api/books?page=1&pageSize=10
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var books = await _bookService.GetAllAsync(page, pageSize);
        return Ok(books);
    }

    // GET: api/books/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _bookService.GetByIdAsync(id);
        if (book == null)
            return NotFound();

        return Ok(book);
    }

    // POST: api/books
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdBook = await _bookService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook);
    }

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

    // DELETE: api/books/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _bookService.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }

    // GET: api/books/search?title=some&author=some
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? title, [FromQuery] string? author)
    {
        var results = await _bookService.SearchAsync(title, author);
        return Ok(results);
    }
}
