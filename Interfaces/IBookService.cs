using BookManagementSystem.DTOs;

namespace BookManagementSystem.Interfaces;

/// ממשק לשירות ספרים.
/// מגדיר את הפעולות האפשריות על ספרים במערכת.
public interface IBookService
{
    /// מחזיר רשימת ספרים עם תמיכה בפגינציה.
    /// <param name="page">מספר העמוד (מתחיל מ־1).</param>
    /// <param name="pageSize">מספר הספרים בכל עמוד.</param>
    /// <returns>רשימת ספרים בהתאם לפרמטרים.</returns>
    Task<IEnumerable<BookDto>> GetAllAsync(int page, int pageSize);

    /// מחזיר ספר לפי מזהה.
    /// <param name="id">מזהה הספר.</param>
    /// <returns>הספר המבוקש אם נמצא, אחרת null.</returns>
    Task<BookDto?> GetByIdAsync(int id);

    /// יוצר ספר חדש.
    /// <param name="dto">אובייקט עם פרטי הספר ליצירה.</param>
    /// <returns>הספר שנוצר כולל מזהה.</returns>
    Task<BookDto> CreateAsync(CreateBookDto dto);

    /// מעדכן ספר קיים לפי מזהה.
    /// <param name="id">מזהה הספר לעדכון.</param>
    /// <param name="dto">פרטי העדכון.</param>
    /// <returns>True אם העדכון הצליח, אחרת false.</returns>
    Task<bool> UpdateAsync(int id, UpdateBookDto dto);

    /// מוחק ספר לפי מזהה.
    /// <param name="id">מזהה הספר למחיקה.</param>
    /// <returns>True אם המחיקה הצליחה, אחרת false.</returns>
    Task<bool> DeleteAsync(int id);

    /// מחפש ספרים לפי כותרת ו/או מחבר.
    /// <param name="title">כותרת לחיפוש (לא חובה).</param>
    /// <param name="author">שם מחבר לחיפוש (לא חובה).</param>
    /// <returns>רשימת ספרים שתואמים לקריטריונים.</returns>
    Task<IEnumerable<BookDto>> SearchAsync(string? title, string? author);
}
