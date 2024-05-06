
namespace MultaqaTech.Service.EventEntitesServices
{
    public class EventCategoryService(IUnitOfWork unitOfWork) : IEventCategoryService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<EventCategory?> CreateEventCategoryAsync(EventCategory category)
        {
            try
            {
                await _unitOfWork.Repository<EventCategory>().AddAsync(category);
                var result = await _unitOfWork.CompleteAsync();
                if (result <= 0) return null;

                return category;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return null;
            }
        }

        public async Task<bool> DeleteEventCategory(int categoryId)
        {
            var category = await _unitOfWork.Repository<EventCategory>().GetByIdAsync(categoryId);

            if (category == null)
                return false;

            try
            {
                _unitOfWork.Repository<EventCategory>().Delete(category);

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

        public async Task<IReadOnlyList<EventCategory>> ReadAllAsync()
        {
            try
            {
                var categories = await _unitOfWork.Repository<EventCategory>().GetAllAsync();

                return categories;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return null;
            }
        }

        public async Task<EventCategory?> ReadByIdAsync(int categoryId)
        {
            try
            {
                var category = await _unitOfWork.Repository<EventCategory>().GetByIdAsync(categoryId);

                return category;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return null;
            }
        }

        public async Task<EventCategory?> UpdateEventCategory(int categoryId, EventCategory updatedcategory)
        {
            var category = await _unitOfWork.Repository<EventCategory>().GetByIdAsync(categoryId);

            if (category == null) return null;

            if (updatedcategory == null || string.IsNullOrWhiteSpace(updatedcategory.Name))
                return null;

            category.Name = updatedcategory.Name;

            try
            {
                _unitOfWork.Repository<EventCategory>().Update(category);
                var result = await _unitOfWork.CompleteAsync();
                if (result <= 0) return null;

                return category;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return null;
            }
        }
    }
}
