namespace MultaqaTech.Service;

public class TagService(IUnitOfWork unitOfWork) : ITagService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Tag?> CreateTagAsync(Tag tag)
    {
        try
        {
            await _unitOfWork.Repository<Tag>().AddAsync(tag);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return tag;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<Tag?> GetTagAsync(int tagId)
    {
        try
        {
            var tag = await _unitOfWork.Repository<Tag>().GetByIdAsync(tagId);

            return tag;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<IReadOnlyList<Tag>> GetTagsAsync()
    {
        try
        {
            var tags = await _unitOfWork.Repository<Tag>().GetAllAsync();

            return tags;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<Tag?> UpdateTag(int tagId, Tag updatedTag)
    {
        var tag = await _unitOfWork.Repository<Tag>().GetByIdAsync(tagId);

        if(tag == null) return null;

        if (updatedTag == null || string.IsNullOrWhiteSpace(updatedTag.Name))
            return null;

        tag.Name= updatedTag.Name;

        try
        {
            _unitOfWork.Repository<Tag>().Update(tag);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return tag;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<bool> DeleteTag(int tagId)
    {
        var tag = await _unitOfWork.Repository<Tag>().GetByIdAsync(tagId);

        if (tag == null)
            return false;

        try
        {
            _unitOfWork.Repository<Tag>().Delete(tag);

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

}
