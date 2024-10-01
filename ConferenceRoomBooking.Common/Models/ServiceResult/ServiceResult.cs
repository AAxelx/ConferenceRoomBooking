using ConferenceRoomBooking.Common.Models.Enums;

namespace ConferenceRoomBooking.Common.Models.ServiceResult;

public class ServiceResult
{
    public ResponseType ResponseType { get; set; }

    public ServiceResult(ResponseType type)
    {
        ResponseType = type;
    }
}