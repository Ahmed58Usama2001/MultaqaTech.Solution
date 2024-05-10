namespace MultaqaTech.Core.Repositories.Contract;

public interface IBasketRepository
{
    public Task<StudentBasket?> UpdateBasket(StudentBasket studentBasket, string email);
    public Task<StudentBasket?> GetBasket(string email);
    public Task<bool> DeleteBasket(string email);
}