using BookManagementSystem.Interfaces;
using BookManagementSystem.DTOs;
using BookManagementSystem.Data;
using BookManagement.API.Models; // אם Book נמצא כאן
using Microsoft.EntityFrameworkCore;

namespace BookManagementSystem.Services;

public class BookService : IBookService
{
    private readonly BookContext _context;

    public BookService(BookContext context)
    {
        _context = context;
    }

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

    public async Task<BookDto?> GetByIdAsync(int id)
    {
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

    public async Task<BookDto> CreateAsync(CreateBookDto dto)
    {
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

    public async Task<bool> DeleteAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return false;

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<BookDto>> SearchAsync(string? title, string? author)
    {
        var query = _context.Books.AsQueryable();

        if (!string.IsNullOrWhiteSpace(title))
            query = query.Where(b => b.Title.Contains(title));

        if (!string.IsNullOrWhiteSpace(author))
            query = query.Where(b => b.Author.Contains(author));

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
