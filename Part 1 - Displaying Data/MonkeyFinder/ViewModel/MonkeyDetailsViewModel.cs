namespace MonkeyFinder.ViewModel;


[QueryProperty("Monkey", "Monkey")]
public partial class MonkeyDetailsViewModel : BaseViewModel
{
    private readonly IMap _map;

    [ObservableProperty]
    private Monkey _monkey;

    public MonkeyDetailsViewModel(IMap map)
    {
        _map = map;
    }

    [RelayCommand]
    private async Task OpenMapAsync()
    {
        try
        {
            await _map.OpenAsync(Monkey.Latitude, Monkey.Longitude, new MapLaunchOptions
            {
                Name = Monkey.Name,
                NavigationMode = NavigationMode.None
            });
        }
        catch (Exception ex)
        {

            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert(
                "Error!",
                $"Unable to open map: {ex.Message}",
                "OK");
        }
    }
}
