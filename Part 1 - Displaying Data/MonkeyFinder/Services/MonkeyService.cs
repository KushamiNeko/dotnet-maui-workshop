using System.Net.Http.Json;

namespace MonkeyFinder.Services;

public class MonkeyService : IMonkeyService
{

    private const string _url = "https://montemagno.com/monkeys.json";

    private readonly HttpClient _httpClient;

    private List<Monkey> _monkeyList = new();

    public MonkeyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        //_httpClient = new HttpClient();
    }

    public async Task<List<Monkey>> GetMonkeys()
    {
        if (_monkeyList.Count > 0)
        {
            return _monkeyList;
        }

        var response = await _httpClient.GetAsync(_url);

        if (response.IsSuccessStatusCode)
        {
            _monkeyList = await response.Content.ReadFromJsonAsync<List<Monkey>>();
        }

        //using var stream = await FileSystem.OpenAppPackageFileAsync("monkeydata.json");
        //using var reader = new StreamReader(stream);
        //var contents = await reader.ReadToEndAsync();
        //_monkeyList = JsonSerializer.Deserialize<List<Monkey>>(contents);

        return _monkeyList;
    }
}
