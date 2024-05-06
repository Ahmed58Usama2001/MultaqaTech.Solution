namespace MultaqaTech.Repository.Repositories;

public class BasketRepository(IConnectionMultiplexer redis) : IBasketRepository
{
    private readonly IDatabase _context = redis.GetDatabase();

    public async Task<bool> DeleteBasket(string basketId)
        => await _context.KeyDeleteAsync(basketId);

    public async Task<StudentBasket?> GetBasket(string basketId)
    {
        RedisValue basketAsJson = await _context.StringGetAsync(basketId);

        return basketAsJson.IsNull ? null : JsonSerializer.Deserialize<StudentBasket>(basketAsJson!);
    }

    public async Task<StudentBasket?> UpdateBasket(StudentBasket studentBasket)
    {
        bool updateOrCreatedSuccessfully = await _context.StringSetAsync(studentBasket.Id, JsonSerializer.Serialize(studentBasket), TimeSpan.FromDays(1));
        return updateOrCreatedSuccessfully ? studentBasket : null;
    }
}