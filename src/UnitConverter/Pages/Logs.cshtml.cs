using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UnitConverter.Models;
using UnitConverter.Services;

namespace UnitConverter.Pages;

public class LogsModel : PageModel
{
    private readonly ILogReader _logReader;

    public LogsModel(ILogReader logReader)
    {
        _logReader = logReader;
    }

    public IEnumerable<LogEntry> Entries { get; private set; } =
        new List<LogEntry>();

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? Level { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? ConversionType { get; set; }

    public IEnumerable<SelectListItem> ConversionOptions =>
        ConversionTypes.All.Select(item =>
            new SelectListItem(item.Value, item.Key));

    public void OnGet()
    {
        var entries = _logReader.Read();
        var filteredEntries = new List<LogEntry>();

        foreach (var entry in entries)
        {
            if (!string.IsNullOrEmpty(Level) &&
                !string.Equals(
                    entry.Level,
                    Level,
                    StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (!string.IsNullOrEmpty(ConversionType) &&
                !string.Equals(
                    entry.ConversionType,
                    ConversionType,
                    StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (!string.IsNullOrWhiteSpace(Search))
            {
                bool matches =
                    (entry.Message?.Contains(
                        Search,
                        StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (entry.Exception?.Contains(
                        Search,
                        StringComparison.OrdinalIgnoreCase) ?? false);

                if (!matches)
                {
                    continue;
                }
            }

            filteredEntries.Add(entry);
        }

        Entries = filteredEntries;
    }
}
