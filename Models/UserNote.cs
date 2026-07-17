using System.ComponentModel.DataAnnotations;

namespace UserPanelMVC.Models;

public class UserNote
{
    public int Id { get; set; }
    
    public int AppUserId { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    public string Content { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}