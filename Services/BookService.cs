using BookManagementSystem.Interfaces;
using BookManagementSystem.DTOs;
using BookManagementSystem.Data;
using BookManagement.API.Models; 
using Microsoft.EntityFrameworkCore;

namespace BookManagementSystem.Services;

// שירות המממש את הלוגיקה העסקית לניהול ספרים
public class BookService : IBookService
{
    private readonly BookContext _context;

    // בונה את השירות ומקבל הקשר למסד הנתונים
    public BookService(BookContext context)
    {
        _context = context;
    }

    // מחזיר את כל הספרים עם תמיכה בפגינציה
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

    // מחזיר ספר לפי מזהה (אם קיים)
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

    // יוצר ספר חדש ומחזיר אותו לאחר שנשמר במסד הנתונים - במידה והספר לא קיים
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

    // מעדכן ספר קיים לפי מזהה ומחזיר true אם הצליח
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

    // מוחק ספר לפי מזהה ומחזיר true אם נמצא ונמחק
    public async Task<bool> DeleteAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return false;

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }

    // מחפש ספרים לפי כותרת ו/או מחבר
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
