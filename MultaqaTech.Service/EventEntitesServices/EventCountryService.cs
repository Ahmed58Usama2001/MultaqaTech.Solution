
namespace MultaqaTech.Service.EventEntitesServices
{
    public class EventCountryService(IUnitOfWork unitOfWork) : IEventCountryService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<EventCountry?> CreateEventCountryAsync(EventCountry country)
        {
            try
            {
                await _unitOfWork.Repository<EventCountry>().AddAsync(country);
                var result = await _unitOfWork.CompleteAsync();
                if (result <= 0) return null;

                return country;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return null;
            }
        }

        public async Task<bool> DeleteEventCountry(int countryId)
        {
            var country = await _unitOfWork.Repository<EventCountry>().GetByIdAsync(countryId);

            if (country == null)
                return false;

            try
            {
                _unitOfWork.Repository<EventCountry>().Delete(country);

                var result = await _unitOfWork.CompleteAsync();

                if (result <= 0)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return false;
            }
        }

        public async Task<IReadOnlyList<EventCountry>> ReadAllAsync()
        {
            try
            {
                var countries = await _unitOfWork.Repository<EventCountry>().GetAllAsync();

                return countries;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return null;
            }
        }

        public async Task<EventCountry?> ReadByIdAsync(int countryId)
        {
            try
            {
                var country = await _unitOfWork.Repository<EventCountry>().GetByIdAsync(countryId);

                return country;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return null;
            }
        }

        public async Task<EventCountry?> UpdateEventCountry(int countryId, EventCountry updatedcountry)
        {
            var country = await _unitOfWork.Repository<EventCountry>().GetByIdAsync(countryId);

            if (country == null) return null;

            if (updatedcountry == null || string.IsNullOrWhiteSpace(updatedcountry.Name))
                return null;

            country.Name = updatedcountry.Name;

            try
            {
                _unitOfWork.Repository<EventCountry>().Update(country);
                var result = await _unitOfWork.CompleteAsync();
                if (result <= 0) return null;

                return country;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return null;
            }
        }
    }
}
