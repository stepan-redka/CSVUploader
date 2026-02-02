using System.Globalization;
using TestApp.Core.Entities;
using TestApp.Core.Interfaces;

namespace TestApp.Infrastructure.Services;

public class CsvParserService : ICsvParserService
{
    private readonly ILogger<CsvParserService> _logger;

    public CsvParserService(ILogger<CsvParserService> logger)
    {
        _logger = logger;
    }

    public async Task<IEnumerable<Contact>> ParseCsvFileAsync(Stream fileStream)
    {
        var contacts = new List<Contact>();
        using var reader = new StreamReader(fileStream);
        int lineNumber = 0;

        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync();
            lineNumber++;

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            // Skip header row
            if (lineNumber == 1 && line.StartsWith("Name", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            try
            {
                var contact = ParseCsvLine(line, lineNumber);
                if (contact != null)
                {
                    contacts.Add(contact);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to parse line {LineNumber}: {Line}", lineNumber, line);
                // Continue processing other lines
            }
        }

        return contacts;
    }

    private Contact? ParseCsvLine(string line, int lineNumber)
    {
        var parts = line.Split(',');

        if (parts.Length != 5)
        {
            _logger.LogWarning("Line {LineNumber} has invalid number of columns: {ColumnCount}", lineNumber, parts.Length);
            return null;
        }

        try
        {
            return new Contact
            {
                Name = parts[0].Trim(),
                DateOfBirth = DateTime.Parse(parts[1].Trim()),
                Married = bool.Parse(parts[2].Trim()),
                Phone = parts[3].Trim(),
                Salary = decimal.Parse(parts[4].Trim(), CultureInfo.InvariantCulture)
            };
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to parse data on line {LineNumber}", lineNumber);
            return null;
        }
    }
}
