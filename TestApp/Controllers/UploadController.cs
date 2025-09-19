using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TestApp.Models;

namespace TestApp.Controllers;

public class UploadController : Controller
{
    private readonly ILogger<UploadController> _logger;
    private readonly DbAppContext _dbContext; // Add DbContext

    public UploadController(ILogger<UploadController> logger, DbAppContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext; // Initialize DbContext
    }

    // GET: Upload page
    public IActionResult Upload()
    {
        return View();
    }

    // POST: Handle file upload
    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file.Length == 0)
        {
            ModelState.AddModelError("", "Please select a CSV file.");
            return View("Upload"); // Ensure the correct view is returned
        }

        using var reader = new StreamReader(file.OpenReadStream());
        int lineNumber = 0;

        String ext = Path.GetExtension(file.FileName).ToLower();
        if (ext == ".csv")
        {
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                lineNumber++;

                if (string.IsNullOrWhiteSpace(line)) continue;
                if (lineNumber == 1 && line.StartsWith("Name")) continue; // Skip header row

                var parts = line.Split(',');

                if (parts.Length != 5) continue; // Basic validation

                var contact = new Contact
                {
                    Name = parts[0].Trim(),
                    DateOfBirth = DateTime.Parse(parts[1].Trim()),
                    Married = bool.Parse(parts[2].Trim()),
                    Phone = parts[3].Trim(),
                    Salary = decimal.Parse(parts[4].Trim(), System.Globalization.CultureInfo.InvariantCulture)
                };

                _dbContext.Contacts.Add(contact);
            }

            _logger.LogInformation("Saving changes...");

            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Documents", "Doc");
        }
        else
        {
            ModelState.AddModelError("", "Only CSV files are allowed.");
            return View("Upload");
        }
    }
    

    
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}