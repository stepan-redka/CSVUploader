using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TestApp.Core.Entities;
using TestApp.Core.Interfaces;
using TestApp.Web.DTOs;

namespace TestApp.Web.Controllers;

public class DocController : Controller
{
    private readonly IContactService _contactService;
    private readonly ILogger<DocController> _logger;

    public DocController(IContactService contactService, ILogger<DocController> logger)
    {
        _contactService = contactService;
        _logger = logger;
    }

    public async Task<IActionResult> Documents()
    {
        try
        {
            var contacts = await _contactService.GetAllContactsAsync();
            return View(contacts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading documents");
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> UpdateContact([FromBody] ContactDto updatedContact)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _contactService.UpdateContactAsync(updatedContact);
            if (result == null)
            {
                return NotFound("Contact not found.");
            }

            return Ok("Contact updated successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating contact {ContactId}", updatedContact.Id);
            return StatusCode(500, "An error occurred while updating the contact.");
        }
    }

    [HttpDelete("Doc/DeleteContact/{id}")]
    public async Task<IActionResult> DeleteContact(int id)
    {
        try
        {
            var result = await _contactService.DeleteContactAsync(id);
            if (!result)
            {
                return NotFound("Contact not found.");
            }

            return Ok("Contact deleted successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting contact {ContactId}", id);
            return StatusCode(500, "An error occurred while deleting the contact.");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}