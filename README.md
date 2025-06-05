````markdown
# 📚 Book Management System

מערכת ניהול ספרים מבוססת ASP.NET Core 8 עם Entity Framework Core ו-SQLite, הכוללת תמיכה בפעולות CRUD, ולידציה, ניהול שגיאות, חיפוש, דוקר, ותיעוד API.

---

## 🛠 טכנולוגיות בשימוש

- ASP.NET Core 8
- Entity Framework Core
- SQLite (ניתן להמיר ל-SQL Server)
- Swagger (OpenAPI)
- Docker

---

## 🚀 הוראות התקנה

### 1. שיבוט והרצה מקומית

```bash
git clone https://github.com/your-username/book-management-system.git
cd book-management-system
dotnet restore
dotnet ef database update
dotnet run
```
````

🔗 נווט לכתובת: [http://localhost:5000/swagger](http://localhost:5000/swagger)

---

### 2. הרצה עם Docker

```bash
docker build -t bookmanagementsystem .
docker run -p 80:80 bookmanagementsystem
```

🔗 נווט לכתובת: [http://localhost](http://localhost)

---

## 📡 API - תיעוד נקודות קצה

| מתודה  | נתיב              | תיאור                   |
| ------ | ----------------- | ----------------------- |
| GET    | /api/books        | קבלת כל הספרים (עמודים) |
| GET    | /api/books/{id}   | קבלת ספר לפי מזהה       |
| POST   | /api/books        | יצירת ספר חדש           |
| PUT    | /api/books/{id}   | עדכון ספר קיים          |
| DELETE | /api/books/{id}   | מחיקת ספר               |
| GET    | /api/books/search | חיפוש לפי שם או מחבר    |

- דוגמה לפאגינציה: `/api/books?page=1&pageSize=10`
- דוגמה לחיפוש: `/api/books/search?title=harry&author=rowling`

---

## ✅ הנחות עבודה

- חיפוש לפי שם/מחבר הוא **case-insensitive** ותומך גם בהתאמה חלקית.
- ספר נחשב כ"כפול" אם יש לו גם את אותו שם וגם את אותו מחבר.
- המזהה (Id) של הספר נוצר אוטומטית במסד הנתונים.
- המערכת משתמשת ב-SQLite לנוחות, אך תואמת גם ל-SQL Server.

---

## ⚙️ החלטות פיתוח ודגשים ארכיטקטוניים

- שימוש ב־**DTOs** להפרדה בין מודל הדומיין ל־API.
- שימוש ב־**Middleware** לניהול שגיאות אחיד בכל המערכת.
- ולידציה עם `DataAnnotations` ב־DTO (למשל: תאריך תקין, מחיר חיובי, שדות חובה).
- הוספת בדיקות לוגיות — למשל, מניעת הוספת ספר שכבר קיים.
- חלוקה בין שכבת שליטה (Controller) לשכבת שירות (Service) לשיפור ארגון הקוד.

---

## 🔧 שיפורים עתידיים

- הוספת בדיקות יחידה ואינטגרציה (xUnit + Moq)
- הוספת Authentication + Authorization
- מעבר ל־SQL Server בפרודקשן עם קונפיגורציה סביבתית
- תמיכה בסינון לפי תאריך פרסום, טווח מחירים וכו'
- שימוש ב־FluentValidation לוולידציה מתקדמת יותר
- הוספת Cache לביצועים משופרים

---

## 🧪 איך להפעיל מיגרציות

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## 📂 מבנה הפרויקט

```
BookManagementSystem/
│
├── Controllers/            → API Controllers
├── DTOs/                   → קבצי DTO עם ולידציה
├── Data/                   → קונטקסט של EF והגדרת מסד הנתונים
├── Middleware/             → טיפול בשגיאות גלובלי
├── Models/                 → מודל הדומיין (Book.cs)
├── Services/               → לוגיקת השירות (BookService וכו')
├── Program.cs              → קונפיגורציית האפליקציה
├── Dockerfile              → קובץ דוקר
└── README.md               → קובץ זה
```

---

## 💡 נקודות בונוס

- ✅ תמיכה בדוקר
- ✅ תיעוד API עם Swagger
- ✅ ולידציה מלאה כולל לוגית
- ✅ טיפול בשגיאות עם Middleware
- 🟡 בדיקות יחידה - לא כלולות
- 🟡 תבניות עיצוב מתקדמות - שמירה על פשטות

---

## 👨‍💻 מחבר

נכתב על ידי Avi כהגשה למבחן מיון למשרת מפתח .NET מתחיל ב־Cambium.

---

## 📜 רישיון

פרויקט פתוח לשימוש חופשי.

```

```

# Book Management System

A RESTful Web API for managing a collection of books, developed as part of a technical assessment for a Junior .NET Developer role.

## Table of Contents

1. [Setup Instructions](#setup-instructions)
2. [API Documentation](#api-documentation)
3. [Assumptions Made](#assumptions-made)
4. [Development Decisions](#development-decisions)
5. [Future Improvements](#future-improvements)
6. [Technology Stack](#technology-stack)

---

## Setup Instructions

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Docker](https://www.docker.com/) (optional but supported)
- SQLite (default database)

### Running Locally (without Docker)

```bash
dotnet restore
dotnet ef database update
dotnet run
```

### Running via Docker

```bash
# Build the Docker image
docker build -t bookmanagementsystem .

# Run the container
docker run -p 8080:80 bookmanagementsystem
```

Access the Swagger UI at:  
[http://localhost:8080/swagger/index.html](http://localhost:8080/swagger/index.html)

---

## API Documentation

All endpoints are available under `/api/books`:

| Method | Endpoint            | Description                     |
| ------ | ------------------- | ------------------------------- |
| GET    | `/api/books`        | List all books (paginated)      |
| GET    | `/api/books/{id}`   | Get a book by ID                |
| POST   | `/api/books`        | Create a new book               |
| PUT    | `/api/books/{id}`   | Update an existing book         |
| DELETE | `/api/books/{id}`   | Delete a book                   |
| GET    | `/api/books/search` | Search books by title or author |

### Example JSON (Create Book)

```json
{
  "title": "Clean Code",
  "author": "Robert C. Martin",
  "publicationDate": "2008-08-01",
  "price": 39.99
}
```

---

## Assumptions Made

- Book uniqueness is defined by combination of Title and Author.
- Price must be a positive decimal number.
- Validation is handled via data annotations in the DTO layer.
- ID auto-increments and is managed by the database.
- Client input is assumed to be in valid JSON format.

---

## Development Decisions

- **DTO Usage**: All client-server communication is handled via DTOs to prevent exposing domain entities.
- **Error Handling**:
  - Centralized via custom `ErrorHandlingMiddleware`
  - `try-catch` is used for specific cases such as database operations
- **Validation**:
  - Input validation is applied using `[Required]`, `[StringLength]`, and `[Range]`
- **Pagination & Search**:
  - Implemented via query parameters (`page`, `pageSize`, `title`, `author`)
  - Case-insensitive and partial search supported

---

## Future Improvements

- Add unit and integration tests using xUnit or NUnit
- Improve input validation (e.g., with FluentValidation)
- Implement authentication and authorization
- Add logging with Serilog
- Use SQL Server instead of SQLite in production
- Use environment-based configuration for connection strings
- Extend search with sorting and filtering options

---

## Technology Stack

- **.NET 8**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **SQLite**
- **Docker**
- **Swagger / OpenAPI** (for API documentation)

---

## Author

Developed by Avi as part of a Junior .NET Developer assessment.
