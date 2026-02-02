using TestApp.Core.Entities;
using TestApp.Web.DTOs;

namespace TestApp.Core.Interfaces;

public interface IContactService
{
    Task<IEnumerable<Contact>> GetAllContactsAsync();
    Task<Contact?> GetContactByIdAsync(int id);
    Task<Contact> CreateContactAsync(ContactDto contactDto);
    Task<Contact?> UpdateContactAsync(ContactDto contactDto);
    Task<bool> DeleteContactAsync(int id);
}
