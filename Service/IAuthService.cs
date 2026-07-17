using UserPanelMVC.Models;

namespace UserPanelMVC.Service;

public interface IAuthService
{
    Task<bool> RegisterUserAsync(string email, string password);
    Task<AppUser?> LoginAsync(string email, string password);
}