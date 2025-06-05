using BookManagementSystem.DTOs;

namespace BookManagementSystem.Interfaces;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllAsync(int page, int pageSize);
    Task<BookDto?> GetByIdAsync(int id);
    Task<BookDto> CreateAsync(CreateBookDto dto);
    Task<bool> UpdateAsync(int id, UpdateBookDto dto);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<BookDto>> SearchAsync(string? title, string? author);
}
