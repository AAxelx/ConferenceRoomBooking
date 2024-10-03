using ConferenceRoomBooking.Common.Models.Enums;

namespace ConferenceRoomBooking.Common.Models.ServiceResult;

public class ServiceValueResult<T> : ServiceResult
{
    public T? Value { get; set; }

    public ServiceValueResult(ResponseType type, string message) : base(type, message)
    {
    }

    public ServiceValueResult(T value, string message = default, ResponseType type = ResponseType.Ok) : base(type, message)
    {
        Value = value;
    }
}