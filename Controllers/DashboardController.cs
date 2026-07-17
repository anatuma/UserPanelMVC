using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserPanelMVC.Data;
using UserPanelMVC.Models;

namespace UserPanelMVC.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly AppDbContext _context;
    public DashboardController(AppDbContext context)
    {
        this._context = context;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var notes = _context.UserNotes
            .Where(n=>n.AppUserId==userId)
            .ToList();
        
        return View(notes);
    }

    [HttpPost]
    public async Task<IActionResult> AddNote(string title, string content)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var note = new UserNote
        {
            AppUserId = userId,
            Title = title,
            Content = content
        };
        
        _context.UserNotes.Add(note);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}