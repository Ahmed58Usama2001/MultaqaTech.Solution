
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

    public async Task<IReadOnlyList<ZoomMeeting>> ReadZoomMeetingAsync(ZoomMeetingSpeceficationsParams speceficationsParams)
    {
        var spec = new ZoomMeetingWithIncludesSpecifications(speceficationsParams);

        var zoomMeetings = await _unitOfWork.Repository<ZoomMeeting>().GetAllWithSpecAsync(spec);

        return zoomMeetings;
    }
    public async Task<ZoomMeeting?> UpdateZoomMeeting(int zoomMeetingId, ZoomMeeting updatedZoomMeeting)
    {
        var zoomMeeting = await _unitOfWork.Repository<ZoomMeeting>().GetByIdAsync(zoomMeetingId);

        if (zoomMeeting == null) return null;

        if (updatedZoomMeeting == null || string.IsNullOrWhiteSpace(updatedZoomMeeting.Title))
            return null;

        zoomMeeting = updatedZoomMeeting;

        try
        {
            _unitOfWork.Repository<ZoomMeeting>().Update(zoomMeeting);
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
