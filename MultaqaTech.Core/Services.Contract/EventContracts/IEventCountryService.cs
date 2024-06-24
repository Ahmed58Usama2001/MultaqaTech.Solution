
namespace MultaqaTech.Core.Services.Contract.EventContracts
{
    public interface IEventCountryService
    {
        Task<EventCountry?> CreateEventCountryAsync(EventCountry country);

        Task<IReadOnlyList<EventCountry>> ReadAllAsync();

        Task<EventCountry?> ReadByIdAsync(int countryId);

        Task<EventCountry?> UpdateEventCountry(int countryId, EventCountry country);

        Task<bool> DeleteEventCountry(int countryId);
    }
}
