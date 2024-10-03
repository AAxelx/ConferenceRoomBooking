using ConferenceRoomBooking.Common.Models.Enums;
using ConferenceRoomBooking.Common.Models.ServiceResult;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.API.Controllers;

[Route("api/[controller]")]
public abstract class BaseController : Controller
{
    public BaseController()
    {
    }

    protected IActionResult MapResponse(ServiceResult result)
    {
        return GetResponseByType(result.ResponseType);
    }

    protected IActionResult MapResponse<TServiceModel, TResponseModel>(ServiceValueResult<TServiceModel> result, Func<TServiceModel, TResponseModel> map)
    {
        if (result.ResponseType != ResponseType.Ok)
        {
            return GetResponseByType(result.ResponseType);
        }

        return Ok(map.Invoke(result.Value));
    }

    protected IActionResult GetResponseByType(ResponseType responseType)
    {
        switch (responseType)
        {
            case ResponseType.Ok:
                return Ok();
            case ResponseType.NoContent:
                return NoContent();
            case ResponseType.BadRequest:
                return BadRequest(); ;
            case ResponseType.Forbidden:
                return Forbid();
            case ResponseType.NotFound:
                return NotFound();
            default:
            case ResponseType.InternalServerError:
                return StatusCode(500);
        }
    }
}