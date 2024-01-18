using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using polyclinic_service.UserActions.DTOs;

namespace polyclinic_service.UserActions.Controllers.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class UserActionsApiController : ControllerBase
{
    [HttpPost("create_appointment")]
    [ProducesResponseType(statusCode:200,type:typeof(String))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [ProducesResponseType(statusCode:409,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<String>> CreateSchedule([FromBody]CreateAppointmentActionRequest request);
}