using CommunityToolkit.Mvvm.ComponentModel;
using DiveHubLogbook.Data;

namespace DiveHubLogbook.ViewModels;

public partial class DashboardViewModel : ObservableObject
{
    private readonly DiveDatabase _database;

    public DashboardViewModel(DiveDatabase database)
    {
        _database = database;
    }

    [ObservableProperty]
    public partial int TotalDives { get; set; }

    [ObservableProperty]
    public partial double MaxDepth { get; set; }

    [ObservableProperty]
    public partial int TotalBottomTimeMinutes { get; set; }
    public string TotalBottomTimeText =>
    $"{TotalBottomTimeMinutes / 60} h {TotalBottomTimeMinutes % 60} min";

    partial void OnTotalBottomTimeMinutesChanged(int value)
    {
    OnPropertyChanged(nameof(TotalBottomTimeText));
    }

    [ObservableProperty]
    public partial string? LastDiveSite { get; set; }

    [ObservableProperty]
    public partial DateTime? LastDiveDate { get; set; }

    public async Task LoadAsync()
    {
        var dives = await _database.GetDivesAsync();

        TotalDives = dives.Count;

        MaxDepth = dives.Count > 0
            ? dives.Max(d => d.MaxDepth)
            : 0;

        TotalBottomTimeMinutes = dives
            .Where(d => d.BottomTimeMinutes.HasValue)
            .Sum(d => d.BottomTimeMinutes ?? 0);

        var lastDive = dives
            .OrderByDescending(d => d.DiveDate)
            .FirstOrDefault();

        LastDiveSite = lastDive?.DiveSite;
        LastDiveDate = lastDive?.DiveDate;
    }
}