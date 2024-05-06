namespace MultaqaTech.Core.Services.Contract.EventContracts
{
    public interface IEventCategoryService
    {
        Task<EventCategory?> CreateEventCategoryAsync(EventCategory category);

        Task<IReadOnlyList<EventCategory>> ReadAllAsync();

        Task<EventCategory?> ReadByIdAsync(int categoryId);

        Task<EventCategory?> UpdateEventCategory(int categoryId, EventCategory category);

        Task<bool> DeleteEventCategory(int categoryId);
    }
}
