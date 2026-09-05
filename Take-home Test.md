Take-home Test
    • Duration: 3 Day
    • Programing Language: C#
    • Framework: .NET 6 or later
Task Management System
The candidate will build a Task Management System that allows users to create, assign, and track tasks. The system should follow SOLID principles and use clean architecture practices.
Requirements
    1. Core Features:
        ◦ Create a new task (Title, Description, Due Date, Priority, Status).
        ◦ Assign a task to a user.
        ◦ Update task details (e.g., status, priority).
        ◦ Delete a task.
        ◦ Retrieve a list of all tasks.
        ◦ Retrieve tasks assigned to a specific user.
        ◦ Add validation for task data (e.g., due date cannot be in the past).
        ◦ Implement logging for critical operations.
        ◦ Add unit tests for core functionality.
    2. SOLID Principles:
        a. Single Responsibility Principle (SRP): Each class/method should have a single responsibility.
        b. Open/Closed Principle (OCP): The system should be open for extension but closed for modification.
        c. Liskov Substitution Principle (LSP): Subtypes should be substitutable for their base types.
        d. Interface Segregation Principle (ISP): Interfaces should be specific to the needs of the client.
        e. Dependency Inversion Principle (DIP): High-level modules should not depend on low-level modules; both should depend on abstractions.

    3. Clean Infrastructure
        a. Use dependency injection to manage dependencies.
        b. Separate concerns into different layers (e.g., Presentation, Application, Domain, Infrastructure).
        c. Use a repository pattern to abstract data access logic.
        d. Use a mock database (e.g., in-memory database or JSON file) for persistence.


    4. Presentation Layer
        a. Provides RESTful API that serve data to front-end.
        b. UI / Front-end is not required.
Evaluation Criteria
    1. Code Quality:
        ◦ Adherence to SOLID principles.
        ◦ Clean and readable code.
        ◦ Proper separation of concerns.

    2. Functionality:
    • All core features are implemented.

    3. Testing:
        ◦ Unit tests cover critical functionality.
        ◦ Tests are well-structured and maintainable.

    4. Infrastructure:
        ◦ Proper use of dependency injection.
        ◦ Repository pattern is correctly implemented.

