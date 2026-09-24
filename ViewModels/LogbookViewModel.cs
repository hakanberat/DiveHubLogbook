using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using DiveHubLogbook.Data;
using DiveHubLogbook.Models;

namespace DiveHubLogbook.ViewModels;

public partial class LogbookViewModel : ObservableObject
{
    private readonly DiveDatabase _database;

    private List<Dive> _allDives = new();

    public ObservableCollection<Dive> Dives { get; } = new();

    [ObservableProperty]
    public partial string? SearchText { get; set; }

    public LogbookViewModel(DiveDatabase database)
    {
        _database = database;
    }

    public async Task LoadDivesAsync()
    {
        _allDives = await _database.GetDivesAsync();

        ApplyFilter();
    }

    partial void OnSearchTextChanged(string? value)
    {
        ApplyFilter();
    }

    private void ApplyFilter()
    {
        IEnumerable<Dive> filteredDives = _allDives;

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            string search = SearchText.Trim();

            filteredDives = _allDives.Where(dive =>
                (dive.DiveSite?.Contains(
                    search,
                    StringComparison.OrdinalIgnoreCase) ?? false)

                ||

                (dive.DiveBuddy?.Contains(
                    search,
                    StringComparison.OrdinalIgnoreCase) ?? false)

                ||

                (dive.Notes?.Contains(
                    search,
                    StringComparison.OrdinalIgnoreCase) ?? false)
            );
        }

        Dives.Clear();

        foreach (var dive in filteredDives)
        {
            Dives.Add(dive);
        }
    }
}