using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DiveHubLogbook.Data;
using DiveHubLogbook.Models;

namespace DiveHubLogbook.ViewModels;

public partial class AddDiveViewModel : ObservableObject
{
    private readonly DiveDatabase _database;

    public AddDiveViewModel(DiveDatabase database)
    {
        _database = database;
        DiveDate = DateTime.Today;
    }

    [ObservableProperty]
    public partial int DiveNumber { get; set; }

    [ObservableProperty]
    public partial DateTime DiveDate { get; set; }

    [ObservableProperty]
    public partial string? DiveSite { get; set; }

    [ObservableProperty]
    public partial double MaxDepth { get; set; }

    [ObservableProperty]
    public partial int? BottomTimeMinutes { get; set; }

    [ObservableProperty]
    public partial double? WaterTemperature { get; set; }

    [ObservableProperty]
    public partial string? SuitType { get; set; }
    public bool IsWetSuit => SuitType == "Wet";
    partial void OnSuitTypeChanged(string? value)
{
    OnPropertyChanged(nameof(IsWetSuit));

    if (value == "Dry")
    {
        SuitLength = null;
        SuitThicknessMm = null;
    }
}

    [ObservableProperty]
    public partial string? SuitLength { get; set; }

    [ObservableProperty]
    public partial double? SuitThicknessMm { get; set; }

    [ObservableProperty]
    public partial double? WeightKg { get; set; }

    [ObservableProperty]
    public partial string? DiveBuddy { get; set; }

    [ObservableProperty]
    public partial string? Notes { get; set; }

    [RelayCommand]
    private async Task SaveDiveAsync()
    {
        var dive = new Dive
        {
            DiveNumber = DiveNumber,
            DiveDate = DiveDate,
            DiveSite = DiveSite,
            MaxDepth = MaxDepth,
            BottomTimeMinutes = BottomTimeMinutes,
            WaterTemperature = WaterTemperature,
            SuitType = SuitType,
            SuitLength = SuitLength,
            SuitThicknessMm = SuitThicknessMm,
            WeightKg = WeightKg,
            DiveBuddy = DiveBuddy,
            Notes = Notes
        };

        await _database.SaveDiveAsync(dive);
        await Shell.Current.GoToAsync("..");
    }
}