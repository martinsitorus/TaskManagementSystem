# Task Management System

A simple .NET 9 Web API for managing tasks and users.
---

## Features

- Create, update, delete, and retrieve users and tasks
- Assign tasks to users
- Validation using FluentValidation
- Repository pattern for data access
- Unit tests with xUnit and Moq
- API documentation with Swagger

---

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- (Optional) [Postman](https://www.postman.com/) or any API client

---

### How to Run

1. **Restore dependencies:**
    ```bash
    dotnet restore
    ```

2. **Build the project:**
    ```bash
    dotnet build
    ```

3. **Run the API:**
    ```bash
    dotnet run --project TaskManagementSystem/TaskManagementSystem.csproj
    ```
    The API will start (by default on https://localhost:5001 or http://localhost:5000).

---

### How to Test

1. **Navigate to the test project folder (if not already there):**
    ```bash
    cd TaskManagementSystem.Tests
    ```

2. **Run all unit tests:**
    ```bash
    dotnet test
    ```

---

### Check with Swagger

1. **Start the API** (see "How to Run" above).
2. **Open your browser and go to:**
    ```
    https://localhost:5001/swagger
    ```
    or
    ```
    http://localhost:5000/swagger
    ```
3. **You will see the Swagger UI** where you can try out all API endpoints interactively.

---

## Notes

- All validation errors will return a 400 Bad Request with details.
- Data is persisted in JSON files for demo purposes.
- For production, replace the repository implementations with a real database provider.

---
