using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TestApp.Core.Entities;
using TestApp.Core.Interfaces;

namespace TestApp.Web.Controllers;

public class UploadController : Controller
{
    private readonly ILogger<UploadController> _logger;
    private readonly ICsvParserService _csvParserService;
    private readonly IContactRepository _contactRepository;

    public UploadController(
        ILogger<UploadController> logger,
        ICsvParserService csvParserService,
        IContactRepository contactRepository)
    {
        _logger = logger;
        _csvParserService = csvParserService;
        _contactRepository = contactRepository;
    }

    public IActionResult Upload()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            ModelState.AddModelError("", "Please select a CSV file.");
            return View("Upload");
        }

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (extension != ".csv")
        {
            ModelState.AddModelError("", "Only CSV files are allowed.");
            return View("Upload");
        }

        try
        {
            var contacts = await _csvParserService.ParseCsvFileAsync(file.OpenReadStream());
            
            foreach (var contact in contacts)
            {
                await _contactRepository.CreateAsync(contact);
            }

            _logger.LogInformation("Successfully uploaded {Count} contacts", contacts.Count());
            return RedirectToAction("Documents", "Doc");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing CSV file");
            ModelState.AddModelError("", "An error occurred while processing the file.");
            return View("Upload");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}