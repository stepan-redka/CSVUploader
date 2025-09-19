using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TestApp.Models;

namespace TestApp.Controllers;

public class UploadController : Controller
{
    private readonly ILogger<UploadController> _logger;
    
    public UploadController(ILogger<UploadController> logger)
    {
        _logger = logger;
    }
    
    public IActionResult Upload()
    {
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}