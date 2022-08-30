using MonkeyFinder.Services;

namespace MonkeyFinder.ViewModel;

public partial class MonkeysViewModel : BaseViewModel
{
    private readonly IMonkeyService _monkeyService;

    private readonly IConnectivity _connectivity;
    private readonly IGeolocation _geolocation;

    [ObservableProperty]
    private bool _isRefreshing;

    public ObservableCollection<Monkey> Monkeys { get; } = new();

    public MonkeysViewModel(IMonkeyService monkeyService, IConnectivity connectivity, IGeolocation geolocation)
    {
        Title = "Monkey Finder";

        _monkeyService = monkeyService;
        _connectivity = connectivity;
        _geolocation = geolocation;
    }

    [RelayCommand]
    private async Task GetClosestMonkeyAsync()
    {
        if (IsBusy || Monkeys.Count == 0)
        {
            return;
        }

        try
        {
            var location = await _geolocation.GetLastKnownLocationAsync() ??
                await _geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });

            if (location is null)
            {
                return;
            }

            var first = Monkeys.OrderBy(m =>
                location.CalculateDistance(m.Latitude, m.Longitude, DistanceUnits.Miles)
            ).FirstOrDefault();

            if (first is null)
            {
                return;
            }

            await Shell.Current.DisplayAlert(
                "Closest Monkey",
                $"{first.Name} in {first.Location}",
                "OK");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert(
                "Error!",
                $"Unable to get closest monkeys: {ex.Message}",
                "OK");
        }
    }

    [RelayCommand]
    private async Task GoToDetailsAsync(Monkey monkey)
    {
        if (monkey is null)
        {
            return;
        }

        await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object>
        {
            {"Monkey", monkey }
        });
    }

    [RelayCommand]
    private async Task GetMonkeysAsync()
    {
        if (IsBusy)
        {
            return;
        }

        try
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert(
              "Internet Issue",
              "Check your internet and try again!",
              "OK");

                return;
            }

            IsBusy = true;

            var monkeys = await _monkeyService.GetMonkeys();

            if (Monkeys.Count != 0)
            {
                Monkeys.Clear();
            }

            foreach (var monkey in monkeys)
            {
                Monkeys.Add(monkey);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert(
                "Error!",
                $"Unable to get monkeys: {ex.Message}",
                "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;   
        }
    }
}
