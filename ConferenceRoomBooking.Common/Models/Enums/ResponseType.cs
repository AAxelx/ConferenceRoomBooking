namespace ConferenceRoomBooking.Common.Models.Enums;

public enum ResponseType
{
    Ok = 0,
    NoContent = 204,
    SeeOther = 303,
    BadRequest = 400,
    Forbidden = 403,
    NotFound = 404,
    InternalServerError = 500
}