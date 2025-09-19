using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TestApp.Models;

namespace TestApp.Controllers;

public class DocController : Controller
{
    private readonly DbAppContext _dbContext;

    public DocController(DbAppContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IActionResult Documents()
    {
        var contacts = _dbContext.Contacts.ToList(); // data from db
        return View(contacts); 
    }
    
    [HttpPost]
    public IActionResult UpdateContact([FromBody] Contact updatedContact)
    {
        if (updatedContact == null || updatedContact.Id <= 0)
        {
            return BadRequest("Invalid contact data.");
        }

        var existingContact = _dbContext.Contacts.FirstOrDefault(c => c.Id == updatedContact.Id);
        if (existingContact == null)
        {
            return NotFound("Contact not found.");
        }

        existingContact.Name = updatedContact.Name;
        existingContact.DateOfBirth = updatedContact.DateOfBirth;
        existingContact.Married = updatedContact.Married;
        existingContact.Phone = updatedContact.Phone;
        existingContact.Salary = updatedContact.Salary;

        _dbContext.SaveChanges();

        return Ok("Contact updated successfully.");
    }
    
    [HttpDelete("DeleteContact/{id}")]
    public IActionResult DeleteContact(int id)
    {
        var contact = _dbContext.Contacts.FirstOrDefault(c => c.Id == id);
        if (contact == null)
        {
            return NotFound("Contact not found.");
        }

        _dbContext.Contacts.Remove(contact);
        _dbContext.SaveChanges();

        return Ok("Contact deleted successfully.");
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}