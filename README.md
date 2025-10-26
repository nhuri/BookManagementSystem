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
*Please make sure that line 24 in Program.cs is commented out before running the application.*
dotnet run
```

Access the Swagger UI at:  
[*address in the output* + "/swagger/index.html"]

### Running via Docker

```bash
# Build the Docker image
docker build -t bookmanagementsystem .

# Run the container
*Please make sure that line 24 in Program.cs is not commented out before running the application.*
docker run -p 8080:80 bookmanagementsystem
```

Access the Swagger UI at:  
[http://localhost:8080/swagger/index.html]

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
- ID auto-increments and is managed by the database, in ascending order usually.
- Client input is assumed to be in valid JSON format.

---

### 🧱 Design Patterns & Architecture

The project follows a clean and modular structure, implementing several key design patterns:

- **Repository Pattern**: Abstracts data access logic for better separation and testability.
- **DTOs (Data Transfer Objects)**: Prevents direct exposure of entity models through the API.
- **Middleware for Centralized Error Handling**: Simplifies error management and maintains clean controller code.
- **Separation of Concerns**: Clear separation between Controllers, Services, and Data Access logic.
- **Dependency Inversion Principle (SOLID)**: Controllers depend on abstractions (`IBookService`), enabling loose coupling and flexible dependency injection.
- **Service Layer Pattern**: Business logic is abstracted into a dedicated service layer (`IBookService`), promoting separation of concerns and easier unit testing.

---

## Development Decisions

- **DTO Usage**: All client-server communication is handled via DTOs to prevent exposing domain entities.
- **Error Handling**:
  - Preferred centralized error handling via custom ErrorHandlingMiddleware over scattered try-catch blocks to keep the codebase clean and maintainable
- **Validation**:
  - Input validation is applied using `[Required]`, `[StringLength]`, and `[Date in the past]`
- **Pagination & Search**:
  - Implemented via query parameters (`page`, `pageSize`, `title`, `author`)
  - Case-insensitive and partial search supported
- **Intefaces**: - The `IBookService` interface was placed in the `Interfaces` folder to separate service contracts from their implementations. This approach provides flexibility for future extensions — for example, supporting multiple book service types (`EBookService`, `PremiumBookService`, etc.). It also aligns with SOLID principles and facilitates clean code and unit testing.

---

## Future Improvements

- Add unit and integration tests using xUnit or NUnit
- Improve input validation (e.g., with FluentValidation)
- Implement authentication and authorization
- Use SQL Server instead of SQLite in production
- Extend search with sorting and filtering options (e.g., price range)
- Supporting multiple book service types (`EBookService`, `PremiumBookService`, etc.).
- Implement caching to reduce redundant database queries and improve overall performance.

---

## Technology Stack

- **.NET 8**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **SQLite**
- **Docker**
- **Swagger**

---

## Author

Developed by Netanel as part of a Junior .NET Developer assessment.

