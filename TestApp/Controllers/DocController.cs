using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TestApp.Models;

namespace TestApp.Controllers;

public class DocController : Controller
{
    private readonly ILogger<DocController> _logger;
    
    public DocController(ILogger<DocController> logger)
    {
        _logger = logger;
    }

    public IActionResult Documents()
    {
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}