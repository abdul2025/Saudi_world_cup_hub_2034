using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaudiWorldCupHub.Models;

namespace SaudiWorldCupHub.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            // The user is logged in
            ViewData["UserStatus"] = "Logged in as " + User.Identity.Name;
        }
        else
        {
            // The user is not logged in
            ViewData["UserStatus"] = "Not logged in";
        }
        return View();
    }




    [Authorize]
    public IActionResult Hotels()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
