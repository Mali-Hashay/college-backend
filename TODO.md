# College Backend - TODO

## Completed

- [x] Add `UserSecretsId` to `College.collegeAPI`.
- [x] Create a new JWT key and store it in User Secrets.
- [x] Remove the old JWT key from `College.collegeAPI/appsettings.json`.
- [x] Update `Program.cs` to read and validate `Jwt:Key`.
- [x] Fix `DataContext` registration by removing `AddSingleton` and keeping `AddDbContext`.
- [x] Remove duplicate `ProjectReference` entries from `College.collegeAPI.csproj`.
- [x] Fix the missing `await` in `StudentService.AddStudentAsync`.
- [x] Fix the JSON structure in `appsettings.json`.
- [x] Run a successful Build.

## Completed authentication work

- [x] Add `PasswordHash` to `Student`.
- [x] Create `RegisterModel` with `Name`, `Email`, `Password`, and `ConfirmPassword`.
- [x] Update `LoginModel` to use `Email` and `Password`.
- [x] Register and use `PasswordHasher<Student>`.
- [x] Add the registration endpoint.
- [x] Check for duplicate email during registration.
- [x] Validate the password during login.
- [x] Create JWT with `Student.Id` as the identifier.
- [x] Create and apply the database migration.
- [x] Verify registration and login through Swagger.
- [x] Add the missing database foreign keys for enrollments.
- [x] Remove the existing development student while preserving courses.

## Security and authorization

- [ ] Separate self-registration from administrator-created students.
- [ ] Add Roles or Policies, such as `Admin` and `Student`.
- [x] Restrict administrative operations to authorized users.
- [x] Prevent students from accessing other students' data.
- [x] Add centralized exception handling without exposing internal messages.
- [ ] Store Production secrets using environment variables or Azure Key Vault.

## Code quality and architecture

- [x] Use asynchronous database operations consistently.
- [x] Return DTOs instead of Entities directly from the API.
- [x] Add input validation.
- [ ] Remove remaining `TODO` items and unused code.
- [ ] Standardize names and formatting.
- [ ] Add structured logging.
- [ ] Add pagination for large lists.

## Tests and documentation

- [ ] Add unit tests for services.
- [ ] Add integration tests for the API.
- [ ] Add tests for login, registration, and JWT validation.
- [ ] Add authorization and error-scenario tests.
- [x] Write `README.md` with setup and run instructions.
- [ ] Document endpoints in Swagger.
- [x] Add and apply the required migration.
- [ ] Add basic CI with GitHub Actions.

## Manual verification

- [ ] Start the API in the `Development` environment with valid User Secrets.
- [ ] Apply migrations or verify that the existing database is up to date.
- [ ] Register a new student account.
- [ ] Log in as a student and authorize Swagger with the returned JWT.
- [ ] Verify that a student can view their own courses and enrollment.
- [ ] Verify that a student cannot view or modify another student's data.
- [ ] Verify that a student can enroll themselves in a course without sending `StudentId`.
- [ ] Verify that a student cannot enroll another student by sending a different `StudentId`.
- [ ] Verify that duplicate enrollment returns `409 Conflict`.
- [ ] Log in with the Development Admin account.
- [ ] Verify that an administrator can manage courses, students, and enrollments.
- [ ] Verify that a student receives `403 Forbidden` for administrator-only operations.
- [ ] Verify invalid input returns `400 Bad Request`.
- [ ] Verify unexpected errors return a safe generic response without internal details.
