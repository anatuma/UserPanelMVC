using Microsoft.EntityFrameworkCore;
using UserPanelMVC.Data;
using UserPanelMVC.Models;

namespace UserPanelMVC.Service;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    
    public AuthService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> RegisterUserAsync(string email, string password)
    {
        if (_context.AppUsers.Any(u => u.Email == email))
        {
            return false;
        }

        var user = new AppUser
        {
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
        };
        await _context.AppUsers.AddAsync(user);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<AppUser?> LoginAsync(string email, string password)
    {
        var user = await _context.AppUsers
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user == null) return null;

        bool isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

        return isValid ? user : null;
    }
}