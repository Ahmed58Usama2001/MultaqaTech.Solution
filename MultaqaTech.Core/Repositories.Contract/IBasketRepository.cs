namespace MultaqaTech.Core.Repositories.Contract;

public interface IBasketRepository
{
    public Task<StudentBasket?> UpdateBasket(StudentBasket studentBasket);
    public Task<StudentBasket?> GetBasket(string basketId);
    public Task<bool> DeleteBasket(string basketId);
}