using ConferenceRoomBooking.Common.Models.Enums;

namespace ConferenceRoomBooking.Common.Models.ServiceResult;

public class ServiceResult
{
    public ResponseType ResponseType { get; set; }
    public string Message { get; set; }

    public ServiceResult(ResponseType type, string message = default)
    {
        ResponseType = type;
        Message = message;
    }
}