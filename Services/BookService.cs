using BookManagementSystem.Interfaces;
using BookManagementSystem.DTOs;
using BookManagementSystem.Data;
using BookManagement.API.Models; 
using Microsoft.EntityFrameworkCore;

namespace BookManagementSystem.Services;

// Service implementing the business logic for managing books
public class BookService : IBookService
{
    private readonly BookContext _context;

    // Constructs the service and receives the database context
    public BookService(BookContext context)
    {
        _context = context;
    }

    // Returns all books with pagination support
    public async Task<IEnumerable<BookDto>> GetAllAsync(int page, int pageSize)
    {
        return await _context.Books
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(book => new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PublicationDate = book.PublicationDate,
                Price = book.Price
            })
            .ToListAsync();
    }

    // Returns a book by ID (if exists)
    public async Task<BookDto?> GetByIdAsync(int id)
    {
        //throw new Exception("This is a test exception from controller");
        var book = await _context.Books.FindAsync(id);
        if (book == null) return null;

        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            PublicationDate = book.PublicationDate,
            Price = book.Price
        };
    }

    // Creates a new book and returns it after saving to the database - if it does not exist
    public async Task<BookDto> CreateAsync(CreateBookDto dto)
    {
        var exists = await _context.Books.AnyAsync(b =>
            b.Title.ToLower() == dto.Title.ToLower() &&
            b.Author.ToLower() == dto.Author.ToLower());

        if (exists)
            throw new Exception("A book with the same title and author already exists.");

        var book = new Book
        {
            Title = dto.Title,
            Author = dto.Author,
            PublicationDate = dto.PublicationDate,
            Price = dto.Price
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            PublicationDate = book.PublicationDate,
            Price = book.Price
        };
    }

    // Updates an existing book by ID and returns true if successful
    public async Task<bool> UpdateAsync(int id, UpdateBookDto dto)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return false;

        book.Title = dto.Title;
        book.Author = dto.Author;
        book.PublicationDate = dto.PublicationDate;
        book.Price = dto.Price;

        await _context.SaveChangesAsync();
        return true;
    }

    // Deletes a book by ID and returns true if found and deleted
    public async Task<bool> DeleteAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return false;

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }

    // Searches for books by title and/or author
    public async Task<IEnumerable<BookDto>> SearchAsync(string? title, string? author)
    {
        var query = _context.Books.AsQueryable();

        if (!string.IsNullOrWhiteSpace(title))
            query = query.Where(b => b.Title.ToLower().Contains(title.ToLower()));

        if (!string.IsNullOrWhiteSpace(author))
            query = query.Where(b => b.Author.ToLower().Contains(author.ToLower()));

        return await query
            .Select(book => new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PublicationDate = book.PublicationDate,
                Price = book.Price
            })
            .ToListAsync();
    }
}
