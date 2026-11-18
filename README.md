# College Backend API
# College Backend API

A layered **backend REST API** for managing college students, courses, and course enrollments.

The project demonstrates authentication, authorization, data validation, asynchronous database access, DTO mapping, and centralized exception handling in an ASP.NET Core Web API.

## Features

- Student registration and login.
- JWT-based authentication.
- Role-based authorization with `Admin` and `Student` roles.
- Course management for administrators.
- Student management for administrators.
- Course enrollment management.
- Student-specific access to personal data and courses.
- Input validation with Data Annotations.
- Centralized handling of unexpected exceptions.
- Asynchronous Entity Framework Core database operations.
- DTOs to prevent exposing database entities directly through the API.
- Swagger/OpenAPI documentation in the Development environment.

## Technologies

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core 8
- SQL Server
- JWT Bearer Authentication
- ASP.NET Core Identity password hashing
- AutoMapper
- Swagger / Swashbuckle

## Project Structure

```text
College.collegeAPI   API controllers, authentication configuration, and application startup
College.Core         Entities, DTOs, interfaces, mappings, and middleware
College.service      Business logic and authentication services
College.Data         EF Core DbContext, repositories, and migrations
```

The project follows a layered architecture:

```text
Controller ? Service ? Repository ? Database
```

- **Controllers** handle HTTP requests, validation, authentication, authorization, and HTTP responses.
- **Services** contain business rules and coordinate application operations.
- **Repositories** handle persistence through Entity Framework Core.
- **DTOs** define the data exposed by the API.

## Prerequisites

- .NET 8 SDK
- SQL Server or SQL Server LocalDB
- Visual Studio 2022 or another .NET-compatible IDE
- Entity Framework Core CLI tools

Install the EF Core CLI tools if necessary:

```bash
dotnet tool install --global dotnet-ef
```

## Configuration

Sensitive values are not stored in `appsettings.json`. The API expects the JWT key and database connection string to be provided through configuration.

From the solution directory, set the User Secrets for the API project:

```bash
dotnet user-secrets set "Jwt:Key" "replace-with-a-long-random-secret-key" --project College.collegeAPI

dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=(localdb)\\MSSQLLocalDB;Database=CollegeDb;Trusted_Connection=True;TrustServerCertificate=True;" --project College.collegeAPI

dotnet user-secrets set "Admin:Email" "admin@example.com" --project College.collegeAPI

dotnet user-secrets set "Admin:Password" "replace-with-a-development-password" --project College.collegeAPI
```

Never commit production secrets, passwords, or connection strings to Git.

## Database Setup

Apply the existing Entity Framework Core migrations from the solution directory:

```powershell
dotnet ef database update --project College.Data --startup-project College.collegeAPI
```

## Run the API

From the solution directory:

```bash
dotnet run --project College.collegeAPI
```

When running in the Development environment, Swagger UI is available at the URL shown in the console, commonly:

```text
https://localhost:<port>/swagger
```

## Authentication and Authorization

Register a user through:

```text
POST /api/Auth/register
```

Newly registered users receive the `Student` role by default. Log in through:

```text
POST /api/Auth/login
```

Copy the returned JWT and use the **Authorize** button in Swagger with:

```text
Bearer <token>
```

Administrative endpoints require the `Admin` role. In the Development environment, an administrator is created from the `Admin:Email` and `Admin:Password` User Secrets when the API starts. Public registration does not create administrators.

## Main Endpoints

### Authentication

- `POST /api/Auth/register` — create a student account.
- `POST /api/Auth/login` — authenticate and receive a JWT.

### Courses

- `GET /api/Course` — list courses.
- `GET /api/Course/{id}` — get a course.
- `GET /api/Course/{id}/students` — administrators can view students enrolled in a course.
- `POST /api/Course` — administrators only.
- `PUT /api/Course/{id}` — administrators only.
- `DELETE /api/Course/{id}` — administrators only.

### Students

- `GET /api/Student` — administrators only.
- `GET /api/Student/{id}` — administrators can view any student; students can view themselves.
- `GET /api/Student/my-courses` — view courses associated with the authenticated student.
- `POST /api/Student` — administrators only.
- `PUT /api/Student/{id}` — administrators only.
- `DELETE /api/Student/{id}` — administrators only.

### Enrollments

- `GET /api/Enrollment` — administrators only.
- `GET /api/Enrollment/{id}` — administrators can view any enrollment; students can view their own enrollment.
- `POST /api/Enrollment` — administrators can enroll any student; students can enroll themselves.
- `PUT /api/Enrollment/{id}` — administrators only.
- `DELETE /api/Enrollment/{id}` — administrators only.

## Validation and Error Handling

Request models and DTOs use built-in Data Annotations for required fields, email format, password rules, identifier ranges, and text length limits.

Expected business errors return appropriate HTTP responses, while unexpected exceptions are handled centrally and logged on the server without exposing internal implementation details to API clients.

