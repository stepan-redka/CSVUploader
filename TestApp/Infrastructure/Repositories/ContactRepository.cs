using Microsoft.EntityFrameworkCore;
using TestApp.Core.Entities;
using TestApp.Core.Interfaces;
using TestApp.Infrastructure.Data;

namespace TestApp.Infrastructure.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly DbAppContext _context;

    public ContactRepository(DbAppContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Contact>> GetAllAsync()
    {
        return await _context.Contacts.ToListAsync();
    }

    public async Task<Contact?> GetByIdAsync(int id)
    {
        return await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Contact> CreateAsync(Contact contact)
    {
        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();
        return contact;
    }

    public async Task<Contact?> UpdateAsync(Contact contact)
    {
        var existingContact = await GetByIdAsync(contact.Id);
        if (existingContact == null)
        {
            return null;
        }

        existingContact.Name = contact.Name;
        existingContact.DateOfBirth = contact.DateOfBirth;
        existingContact.Married = contact.Married;
        existingContact.Phone = contact.Phone;
        existingContact.Salary = contact.Salary;

        await _context.SaveChangesAsync();
        return existingContact;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var contact = await GetByIdAsync(id);
        if (contact == null)
        {
            return false;
        }

        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
