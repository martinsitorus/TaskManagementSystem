# Task Management System

A .NET 9 Web API for managing tasks and users, built with clean architecture:

- **TaskManagementSystem.Domain** — entities (`TaskItem`, `User`), enums (`TaskPriority`, `TaskState`), domain exceptions
- **TaskManagementSystem.Application** — DTOs, repository/service ports, services, FluentValidation validators
- **TaskManagementSystem.Infrastructure** — JSON-file repository implementations
- **TaskManagementSystem.Api** — REST controllers, exception-handling middleware, composition root
- **TaskManagementSystem.Tests** — xUnit + Moq unit tests

Tasks are identified by Guid `Id` in API routes.

---

## Features

- Create, update, delete, and retrieve users and tasks
- Assign tasks to users (assignee must exist)
- Validation using FluentValidation (due date cannot be in the past; Priority must be Low/Medium/High; Status must be Todo/InProgress/Done)
- Repository pattern with JSON-file persistence (`Data/tasks.json`, `Data/users.json`)
- Logging of critical operations via `ILogger`
- Unit tests with xUnit and Moq (38 tests)
- API documentation with Swagger

---

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- (Optional) [Postman](https://www.postman.com/) or any API client

---

### How to Run

From the `TaskManagementSystem` folder:

1. **Restore dependencies:**
    ```bash
    dotnet restore
    ```

2. **Build the solution:**
    ```bash
    dotnet build
    ```

3. **Run the API:**
    ```bash
    dotnet run
    ```
    The API starts on `http://localhost:5120` (see `Properties/launchSettings.json`).

---

### How to Test

From the `TaskManagementSystem` folder:

```bash
dotnet test
```

---

### Check with Swagger

1. **Start the API** (see "How to Run" above).
2. **Open your browser and go to:**
    ```
    http://localhost:5120/swagger
    ```
3. **You will see the Swagger UI** where you can try out all API endpoints interactively.

---

## API quick reference

Tasks (`/api/tasks`):

- `GET /api/tasks` — list all
- `GET /api/tasks/{id}` — get one (404 when missing)
- `POST /api/tasks` — body: `{ "title", "description", "assignedTo": { "username", "userID" }, "priority": "High", "dueDate": "2026-12-31" }` → 201 + `Location`
- `PUT /api/tasks/{id}` — full update: `{ "title", "description", "assignedTo", "priority", "status": "InProgress", "dueDate" }`
- `PATCH /api/tasks/{id}/status` — `{ "status": "Done" }`
- `PATCH /api/tasks/{id}/priority` — `{ "priority": "Low" }`
- `PATCH /api/tasks/{id}/due-date` — `{ "dueDate": "2026-12-31" }`
- `PATCH /api/tasks/{id}/assignee` — `{ "assignedTo": { "username", "userID" } }`
- `DELETE /api/tasks/{id}` → 204

Users (`/api/users`):

- `GET /api/users` — list all; `GET /api/users?username={name}` — look up by username
- `GET /api/users/{id}` — get one
- `GET /api/users/{id}/tasks` — tasks assigned to the user
- `POST /api/users` — `{ "username", "userID" }` → 201 (409 on duplicate)
- `PUT /api/users/{id}` — `{ "username" }`
- `DELETE /api/users/{id}` → 204

---

## Notes

- Validation errors return 400 with details; missing resources return 404; duplicates return 409.
- Enum values in JSON: Priority `Low`/`Medium`/`High`, Status `Todo`/`InProgress`/`Done`.
- Data is persisted in JSON files under `Data/` for demo purposes.
- For production, replace the Infrastructure repositories with a real database provider (no Application/Api changes needed).

---
