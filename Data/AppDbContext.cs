using Microsoft.EntityFrameworkCore;
using UserPanelMVC.Models;

namespace UserPanelMVC.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<UserNote> UserNotes { get; set; }
}