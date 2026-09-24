using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DiveHubLogbook.Data;
using DiveHubLogbook.Models;

namespace DiveHubLogbook.ViewModels;

public partial class EditDiveViewModel : ObservableObject
{
    private readonly DiveDatabase _database;

    private Dive? _dive;

    public EditDiveViewModel(DiveDatabase database)
    {
        _database = database;
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

    public async Task LoadDiveAsync(int id)
    {
        _dive = await _database.GetDiveAsync(id);

        if (_dive is null)
            return;

        DiveNumber = _dive.DiveNumber;
        DiveDate = _dive.DiveDate;
        DiveSite = _dive.DiveSite;
        MaxDepth = _dive.MaxDepth;
        BottomTimeMinutes = _dive.BottomTimeMinutes;
        WaterTemperature = _dive.WaterTemperature;
        SuitType = _dive.SuitType;
        SuitLength = _dive.SuitLength;
        SuitThicknessMm = _dive.SuitThicknessMm;
        WeightKg = _dive.WeightKg;
        DiveBuddy = _dive.DiveBuddy;
        Notes = _dive.Notes;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (_dive is null)
            return;

        _dive.DiveNumber = DiveNumber;
        _dive.DiveDate = DiveDate;
        _dive.DiveSite = DiveSite;
        _dive.MaxDepth = MaxDepth;
        _dive.BottomTimeMinutes = BottomTimeMinutes;
        _dive.WaterTemperature = WaterTemperature;
        _dive.SuitType = SuitType;
        _dive.SuitLength = SuitLength;
        _dive.SuitThicknessMm = SuitThicknessMm;
        _dive.WeightKg = WeightKg;
        _dive.DiveBuddy = DiveBuddy;
        _dive.Notes = Notes;

        await _database.SaveDiveAsync(_dive);

        await Shell.Current.GoToAsync("..");
    }
}