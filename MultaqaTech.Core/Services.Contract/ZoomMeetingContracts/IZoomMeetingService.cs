
namespace MultaqaTech.Core.Services.Contract.ZoomMeetingContracts;

public interface IZoomMeetingService
{
    Task<ZoomMeeting?> CreateZoomMeetingAsync(ZoomMeeting zoomMeeting);

    Task<IReadOnlyList<ZoomMeeting>> ReadZoomMeetingAsync(ZoomMeetingSpeceficationsParams speceficationsParams);

    Task<ZoomMeeting?> ReadByIdAsync(int zoomMeetingId);

    Task<ZoomMeeting?> UpdateZoomMeeting(int zoomMeetingId, ZoomMeeting updatedZoomMeeting);

    Task<bool> DeleteZoomMeeting(ZoomMeeting zoomMeeting);

    Task<int> GetCountAsync(ZoomMeetingSpeceficationsParams speceficationsParams);
}
