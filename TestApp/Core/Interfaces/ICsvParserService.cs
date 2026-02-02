using TestApp.Core.Entities;

namespace TestApp.Core.Interfaces;

public interface ICsvParserService
{
    Task<IEnumerable<Contact>> ParseCsvFileAsync(Stream fileStream);
}
