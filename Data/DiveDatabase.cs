using SQLite;
using DiveHubLogbook.Models;

namespace DiveHubLogbook.Data;

public class DiveDatabase
{
    private SQLiteAsyncConnection? _database;

    private async Task InitAsync()
    {
        if (_database is not null)
            return;

        string databasePath = Path.Combine(
            FileSystem.AppDataDirectory,
            "divelogbook.db3"
        );

        _database = new SQLiteAsyncConnection(databasePath);

        await _database.CreateTableAsync<Dive>();
    }

    public async Task<List<Dive>> GetDivesAsync()
    {
        await InitAsync();

        return await _database!
            .Table<Dive>()
            .OrderByDescending(d => d.DiveDate)
            .ToListAsync();
    }

    public async Task<Dive?> GetDiveAsync(int id)
    {
        await InitAsync();

        return await _database!
            .Table<Dive>()
            .Where(d => d.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<int> SaveDiveAsync(Dive dive)
    {
        await InitAsync();

        if (dive.Id != 0)
            return await _database!.UpdateAsync(dive);

        return await _database!.InsertAsync(dive);
    }

    public async Task<int> DeleteDiveAsync(Dive dive)
    {
        await InitAsync();

        return await _database!.DeleteAsync(dive);
    }
}