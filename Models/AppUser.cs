using System.ComponentModel.DataAnnotations;

namespace UserPanelMVC.Models;

public class AppUser
{
    public int Id { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string PasswordHash { get; set; }
    
    public string Role { get; set; } = "User";
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<UserNote> Notes { get; set; } = new List<UserNote>();
}