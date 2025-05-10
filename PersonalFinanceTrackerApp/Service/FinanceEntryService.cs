using PersonalFinanceTrackerApp.Models;

namespace PersonalFinanceTrackerApp.Service;

public class FinanceEntryService
{
    private readonly HttpClient _http;

    public FinanceEntryService(HttpClient http) => _http = http;

    public async Task<List<FinanceEntry>> GetEntriesAsync()
    {
        try
        {
            var response = await _http.GetFromJsonAsync<List<FinanceEntry>>("api/financeentry");
            return response ?? new List<FinanceEntry>();
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"GetEntriesAsync failed: {e.Message}");
            return new List<FinanceEntry>();
        }
    }

    public async Task<bool> AddEntryAsync(FinanceEntry entry)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/FinanceEntry", entry);
            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"AddEntryAsync failed: {e.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateEntryAsync(FinanceEntry entry)
    {
        try
        {
            var response = await _http.PutAsJsonAsync($"api/FinanceEntry/{entry.Id}", entry);
            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"UpdateEntryAsync failed: {e.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteEntryAsync(int id)
    {
        try
        {
            var response = await _http.DeleteAsync($"api/FinanceEntry/{id}");
            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"DeleteEntryAsync failed: {e.Message}");
            return false;
        }
    }
}