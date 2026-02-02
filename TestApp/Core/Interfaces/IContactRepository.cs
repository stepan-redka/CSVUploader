using TestApp.Core.Entities;

namespace TestApp.Core.Interfaces;

public interface IContactRepository
{
    Task<IEnumerable<Contact>> GetAllAsync();
    Task<Contact?> GetByIdAsync(int id);
    Task<Contact> CreateAsync(Contact contact);
    Task<Contact?> UpdateAsync(Contact contact);
    Task<bool> DeleteAsync(int id);
    Task<int> SaveChangesAsync();
}
