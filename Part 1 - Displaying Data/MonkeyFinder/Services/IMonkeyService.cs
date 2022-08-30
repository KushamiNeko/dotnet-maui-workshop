namespace MonkeyFinder.Services;

public interface IMonkeyService
{
    public Task<List<Monkey>> GetMonkeys();
}