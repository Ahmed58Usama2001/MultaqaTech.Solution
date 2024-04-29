
namespace MultaqaTech.Service.ZoomMeetingEntites;

public class ZoomMeetingService(IUnitOfWork unitOfWork) : IZoomMeetingService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ZoomMeeting?> CreateZoomMeetingAsync(ZoomMeeting zoomMeeting)
    {
        try
        {
            await _unitOfWork.Repository<ZoomMeeting>().AddAsync(zoomMeeting);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return zoomMeeting;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }
    public async Task<ZoomMeeting?> ReadByIdAsync(int zoomMeetingId)
    {
        var spec = new ZoomMeetingWithIncludesSpecifications(zoomMeetingId);

        var zoomMeeting = await _unitOfWork.Repository<ZoomMeeting>().GetByIdWithSpecAsync(spec);

        return zoomMeeting;
    }

    public async Task<IReadOnlyList<ZoomMeeting>> ReadAllZoomMeetingsAsync(ZoomMeetingSpeceficationsParams speceficationsParams)
    {
        var spec = new ZoomMeetingWithIncludesSpecifications(speceficationsParams);

        var zoomMeetings = await _unitOfWork.Repository<ZoomMeeting>().GetAllWithSpecAsync(spec);

        return zoomMeetings;
    }

    public async Task<ZoomMeeting?> UpdateZoomMeeting(ZoomMeeting storedZoomMeeting, ZoomMeeting newZoomMeeting)
    {
        if (storedZoomMeeting == null || newZoomMeeting == null)
            return null;

        storedZoomMeeting.Content = newZoomMeeting.Content;
        storedZoomMeeting.ZoomPictureUrl = newZoomMeeting.ZoomPictureUrl;
        storedZoomMeeting.TimeZone = newZoomMeeting.TimeZone;
        storedZoomMeeting.Category = newZoomMeeting.Category;
        storedZoomMeeting.ZoomMeetingCategoryId = newZoomMeeting.ZoomMeetingCategoryId;

        try
        {
            _unitOfWork.Repository<ZoomMeeting>().Update(storedZoomMeeting);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return storedZoomMeeting;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<bool> DeleteZoomMeeting(ZoomMeeting zoomMeeting)
    {
        try
        {
            _unitOfWork.Repository<ZoomMeeting>().Delete(zoomMeeting);

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


    public async Task<int> GetCountAsync(ZoomMeetingSpeceficationsParams speceficationsParams)
    {
        var countSpec = new ZoomMeetingWithFilterationForCountSpecifications(speceficationsParams);

        var count = await _unitOfWork.Repository<ZoomMeeting>().GetCountAsync(countSpec);

        return count;
    }
}
