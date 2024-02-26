namespace MultaqaTech.Core.Services.Contract;

public interface ITagService
{
    Task<Tag?> CreateTagAsync(Tag tag);
    Task<IReadOnlyList<Tag>> GetTagsAsync();

    Task<Tag?> GetTagAsync(int tagId);

    Task<Tag?> UpdateTag(int tagId,Tag tag);

    Task<bool> DeleteTag(int tagId);


}
