using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using UserPanelMVC.Models;
using UserPanelMVC.Service;

namespace UserPanelMVC.Controllers;

public class AccountController : Controller
{
    private readonly IAuthService _authService;
    public AccountController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var registrationSucceeded = await _authService.RegisterUserAsync(model.Email, model.Password);
        if (!registrationSucceeded)
        {
            ModelState.AddModelError(string.Empty, "Registration failed.");
            return View(model);
        }
        
        return RedirectToAction("Login", "Account");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _authService.LoginAsync(model.Email, model.Password);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid login.");
            return View(model);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role)
        };

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme
        );
        
        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal
        );
        
        if (user.Role == "Admin")
        {
            return RedirectToAction("Index", "Admin");
        }

        return RedirectToAction("Index", "Dashboard");
    }

    [HttpGet]
    public IActionResult Logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }
}