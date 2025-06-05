using BookManagement.API.Models;
using BookManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using BookManagementSystem.Services;
using BookManagementSystem.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// מוסיף תמיכה ב־Controllers לפרויקט (API endpoints)
builder.Services.AddControllers();

// רישום השירות BookService לממשק IBookService (תבנית Dependency Injection)
builder.Services.AddScoped<IBookService, BookService>();

// רישום DbContext והגדרת SQLite כמסד הנתונים
builder.Services.AddDbContext<BookContext>(options =>
    options.UseSqlite("Data Source=books.db")); // ניתן להחליף ל-SQL Server אם נדרש

// הוספת תמיכה ב־Swagger (תיעוד API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//  קביעת הפורט ל־80 לטובת הדוקר (HTTP)
builder.WebHost.UseUrls("http://*:80");

var app = builder.Build();

// יצירת scope כדי להפעיל פעולות אתחול (כמו יצירת מסד נתונים וזריעת נתונים)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BookContext>();

    // מבצע מיגרציה למסד הנתונים (יוצר את הסכימה לפי EF Core)
    context.Database.Migrate();

    // מזין את מסד הנתונים עם ספרים ראשוניים אם ריק
    SeedData.Initialize(context);
}

// הפעלת Swagger תמיד 
app.UseSwagger();
app.UseSwaggerUI();

// הפנייה ל־HTTPS
app.UseHttpsRedirection();

// מיפוי הנתיבים לכל ה־Controllers
app.MapControllers();

// הפעלת האפליקציה
app.Run();
