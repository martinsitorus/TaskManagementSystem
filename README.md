# Task Management System

A .NET 9 Web API for managing tasks and users. Tasks are looked up by **title** in the API routes.

---

## Features

- Create, update, delete, and retrieve users and tasks
- Assign tasks to users (assignee must exist)
- Validation using FluentValidation (due date cannot be in the past; Priority must be Low/Medium/High; Status must be To Do/In Progress/Done)
- Repository pattern with JSON-file persistence (`Data/tasks.json`, `Data/users.json`)
- Logging of critical operations via `ILogger`
- Unit tests with xUnit and Moq (34 tests)
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

2. **Build the project:**
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

From the `TaskManagementSystem` folder (runs the `TaskManagementSystem.Tests` project in the solution):

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

Tasks (`api/tasks`):

- `GET api/tasks/getalltasks`
- `POST api/tasks/createtask` — body: `{ "title", "description", "dueDate": "2026-12-31", "priority": "High", "assignedTo": { "username": "Tommy", "userID": "002" } }`
- `GET api/tasks/gettaskbyid?taskId={title}`
- `GET api/tasks/gettaskbyassignedto?assignedTo={username}`
- `PUT api/tasks/updatetaskstatus?taskId={title}&newStatus=Done`
- `PUT api/tasks/updatetaskpriority?taskId={title}&newPriority=High`
- `PUT api/tasks/updatetaskduedate?taskId={title}&newDueDate=2026-12-31`
- `PUT api/tasks/updatetaskassignedto?taskId={title}` — body: `{ "username": "Tommy", "userID": "002" }`
- `DELETE api/tasks/deletetask?taskId={title}`

Users (`api/users`): `createUser`, `getUserByUserID/{userID}`, `getUserByUsername/{username}`, `getAllUsers`, `updateUser/{userID}`, `deleteUserByID/{userID}`, `deleteUserByUsername/{username}`, `userIDExists/{userID}`, `usernameExists/{username}`.

---

## Notes

- All validation errors return a 400 Bad Request with details; missing resources return 404.
- Data is persisted in JSON files under `Data/` for demo purposes.
- For production, replace the repository implementations with a real database provider.

---
