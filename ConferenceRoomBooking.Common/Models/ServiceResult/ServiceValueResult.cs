using ConferenceRoomBooking.Common.Models.Enums;

namespace ConferenceRoomBooking.Common.Models.ServiceResult;

public class ServiceValueResult<T> : ServiceResult
{
    public T? Value { get; set; }

    public ServiceValueResult(ResponseType type) : base(type)
    {
    }

    public ServiceValueResult(T value, ResponseType type = ResponseType.Ok) : base(type)
    {
        Value = value;
    }
}