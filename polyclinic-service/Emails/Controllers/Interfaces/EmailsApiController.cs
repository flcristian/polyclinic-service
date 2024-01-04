using Microsoft.AspNetCore.Mvc;
using polyclinic_service.Emails.DTOs;

namespace polyclinic_service.Emails.Controllers.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class EmailsApiController : ControllerBase
{
    [HttpPost("send_email")]
    [ProducesResponseType(statusCode: 200, type: typeof(String))]
    [ProducesResponseType(statusCode: 400, type: typeof(String))]
    [ProducesResponseType(statusCode: 500, type: typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult> SendEmail([FromBody]SendEmailRequest request);
}