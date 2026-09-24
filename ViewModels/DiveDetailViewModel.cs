using CommunityToolkit.Mvvm.ComponentModel;
using DiveHubLogbook.Data;
using DiveHubLogbook.Models;

namespace DiveHubLogbook.ViewModels;

public partial class DiveDetailViewModel : ObservableObject
{
    private readonly DiveDatabase _database;

    public DiveDetailViewModel(DiveDatabase database)
    {
        _database = database;
    }

    [ObservableProperty]
    public partial Dive? Dive { get; set; }

    public async Task LoadDiveAsync(int id)
    {
        Dive = await _database.GetDiveAsync(id);
    }

    public async Task DeleteDiveAsync()
    {
        if (Dive is null)
            return;

        await _database.DeleteDiveAsync(Dive);
    }
}