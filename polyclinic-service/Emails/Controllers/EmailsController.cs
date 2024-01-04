using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using polyclinic_service.Emails.Controllers.Interfaces;
using polyclinic_service.Emails.DTOs;
using polyclinic_service.Emails.Services.Interfaces;
using polyclinic_service.System.Constants;

namespace polyclinic_service.emails.Controllers;

public class EmailsController : EmailsApiController
{
    private IEmailSenderService _service;

    private ILogger<EmailsController> _logger;
    
    public EmailsController(IEmailSenderService service, ILogger<EmailsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    public override async Task<ActionResult> SendEmail(SendEmailRequest request)
    {
        _logger.LogInformation($"Rest request: Send email to {request.Email} with subject {request.Subject} and message {request.Message}.");
        try
        {
            await _service.SendEmailAsync(request);

            return Ok(Constants.EMAIL_SENT);
        }
        catch (SmtpException ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return StatusCode(500, ex.Message);
        }
        catch (TimeoutException ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return StatusCode(500, ex.Message);
        }
        catch (ObjectDisposedException ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }
}