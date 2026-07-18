# User Panel MVC

Register, log in, take notes, feel important on an admin page. Classic ASP.NET Core MVC — cookies, roles, BCrypt, no JWT ceremony required.

A small web app with user registration, role-based access, a personal notes dashboard, and a separate admin area. SQLite keeps setup painless; the auth flow is the real star.

## What it does

- **Register & login** — cookie-based authentication with hashed passwords
- **User dashboard** — logged-in users create and view their own notes
- **Admin panel** — `[Authorize(Roles = "Admin")]` — admins only
- **Auto-seeded admin** — created on first startup from `appsettings.json`

## Tech stack

- **ASP.NET Core MVC**
- **EF Core** + **SQLite** (`app.db`)
- **Cookie authentication**
- **BCrypt.Net** for password hashing

## Main routes

| Area | Route | Who |
|------|-------|-----|
| Home | `/` | Everyone |
| Register | `/Account/Register` | Guests |
| Login | `/Account/Login` | Guests |
| Notes dashboard | `/Dashboard` | Authenticated users |
| Admin | `/Admin` | Admin role only |

## Run it locally

1. From the project folder:
   ```bash
   dotnet run
   ```
2. Open **http://localhost:5272**
3. **Try it:**
   - Register a normal user at `/Account/Register` → gets the **User** role automatically
   - Or log in as the seeded admin (from `appsettings.json`):
     - Email: `smartstudent@pjwstk.edu.pl`
     - Password: `12345_67_8`

The SQLite database is created on first run — no manual DB setup.
