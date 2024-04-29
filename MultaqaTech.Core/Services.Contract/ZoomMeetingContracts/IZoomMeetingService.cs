
namespace MultaqaTech.Core.Services.Contract.ZoomMeetingContracts;

public interface IZoomMeetingService
{
    Task<ZoomMeeting?> CreateZoomMeetingAsync(ZoomMeeting zoomMeeting);

    Task<IReadOnlyList<ZoomMeeting>> ReadAllZoomMeetingsAsync(ZoomMeetingSpeceficationsParams speceficationsParams);

    Task<ZoomMeeting?> ReadByIdAsync(int zoomMeetingId);

    Task<ZoomMeeting?> UpdateZoomMeeting(ZoomMeeting storedZoomMeeting, ZoomMeeting newZoomMeeting);

    Task<bool> DeleteZoomMeeting(ZoomMeeting zoomMeeting);

    Task<int> GetCountAsync(ZoomMeetingSpeceficationsParams speceficationsParams);
}
