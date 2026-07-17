# User Panel MVC
A simple ASP.NET Core MVC web app featuring secure user registration, role-based dashboards and a personal board for taking notes.

## Getting started
1. Open the project folder in your terminal and run the command:
   dotnet run
2. Open your browser and go to: http://localhost:5272

## Testing accounts
* Regular User: Go to /Account/Register to create a new account. It automatically gets the User role.
* Admin Account: Automatically seeded in the database on startup:
   - Email: smartstudent@pjwstk.edu.pl
   - Password: 12345_67_8

## Code guide
* Password Hashing: Handled in Services/AuthService.cs using BCrypt.Net.BCrypt.HashPassword() and checked via BCrypt.Net.BCrypt.Verify().
* Auth Configuration: Configured in Program.cs via builder.Services.AddAuthentication() and cookie middleware.
* Security Guardrails: Enforced using [Authorize] attributes:
   - DashboardController.cs uses [Authorize] to block guests from viewing or adding notes.
   - AdminController.cs uses [Authorize(Roles = "Admin")] to restrict access to admins only.

## Tech stack
* ASP.NET Core MVC, EF Core, SQLite (app.db), Cookie Auth & BCrypt.Net.