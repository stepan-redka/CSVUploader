using TestApp.Core.Entities;
using TestApp.Core.Interfaces;
using TestApp.Web.DTOs;

namespace TestApp.Infrastructure.Services;

public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;
    private readonly ILogger<ContactService> _logger;

    public ContactService(IContactRepository contactRepository, ILogger<ContactService> logger)
    {
        _contactRepository = contactRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Contact>> GetAllContactsAsync()
    {
        try
        {
            return await _contactRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all contacts");
            throw;
        }
    }

    public async Task<Contact?> GetContactByIdAsync(int id)
    {
        try
        {
            return await _contactRepository.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving contact with ID {ContactId}", id);
            throw;
        }
    }

    public async Task<Contact> CreateContactAsync(ContactDto contactDto)
    {
        try
        {
            var contact = MapToContact(contactDto);
            return await _contactRepository.CreateAsync(contact);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating contact");
            throw;
        }
    }

    public async Task<Contact?> UpdateContactAsync(ContactDto contactDto)
    {
        try
        {
            if (contactDto.Id <= 0)
            {
                _logger.LogWarning("Invalid contact ID for update: {ContactId}", contactDto.Id);
                return null;
            }

            var contact = MapToContact(contactDto);
            return await _contactRepository.UpdateAsync(contact);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating contact with ID {ContactId}", contactDto.Id);
            throw;
        }
    }

    public async Task<bool> DeleteContactAsync(int id)
    {
        try
        {
            var result = await _contactRepository.DeleteAsync(id);
            if (result)
            {
                _logger.LogInformation("Contact with ID {ContactId} deleted successfully", id);
            }
            else
            {
                _logger.LogWarning("Contact with ID {ContactId} not found", id);
            }
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting contact with ID {ContactId}", id);
            throw;
        }
    }

    private static Contact MapToContact(ContactDto dto)
    {
        return new Contact
        {
            Id = dto.Id,
            Name = dto.Name,
            DateOfBirth = dto.DateOfBirth,
            Married = dto.Married,
            Phone = dto.Phone,
            Salary = dto.Salary
        };
    }
}
