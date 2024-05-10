namespace MultaqaTech.Repository.Repositories;

public class BasketRepository(IConnectionMultiplexer redis) : IBasketRepository
{
    private readonly IDatabase _context = redis.GetDatabase();

    public async Task<StudentBasket?> UpdateBasket(StudentBasket studentBasket, string email)
    {
        bool updateOrCreatedSuccessfully = await _context.StringSetAsync(email, JsonSerializer.Serialize(studentBasket), TimeSpan.FromDays(1));
        return updateOrCreatedSuccessfully ? studentBasket : null;
    }

    public async Task<StudentBasket?> GetBasket(string email)
    {
        RedisValue basketAsJson = await _context.StringGetAsync(email);

        return basketAsJson.IsNull ? null : JsonSerializer.Deserialize<StudentBasket>(basketAsJson);
    }

    public async Task<bool> DeleteBasket(string email)
        => await _context.KeyDeleteAsync(email);
}