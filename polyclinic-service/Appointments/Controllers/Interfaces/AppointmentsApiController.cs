using Microsoft.AspNetCore.Mvc;
using polyclinic_service.Appointments.DTOs;
using polyclinic_service.Appointments.Models;

namespace polyclinic_service.Appointments.Controllers.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class AppointmentsApiController : ControllerBase
{
    [HttpGet("all")]
    [ProducesResponseType(statusCode:200,type:typeof(IEnumerable<Appointment>))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<IEnumerable<Appointment>>> GetAllAppointments();
    
    [HttpGet("appointment/{id}")]
    [ProducesResponseType(statusCode:200,type:typeof(Appointment))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<Appointment>> GetAppointmentById([FromRoute]int id);
    
    [HttpPost("create")]
    [ProducesResponseType(statusCode:201,type:typeof(Appointment))]
    [Produces("application/json")]
    public abstract Task<ActionResult<Appointment>> CreateAppointment([FromBody]CreateAppointmentRequest appointmentRequest);
    
    [HttpPut("update")]
    [ProducesResponseType(statusCode:202,type:typeof(Appointment))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<Appointment>> UpdateAppointment([FromBody]UpdateAppointmentRequest appointmentRequest);
    
    [HttpDelete("delete/{id}")]
    [ProducesResponseType(statusCode:202,type:typeof(String))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult> DeleteAppointment([FromRoute]int id);

    [HttpGet("check_availability_for_day")]
    [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<FreeTimeSlotResponse>))]
    [ProducesResponseType(statusCode: 404, type: typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<IEnumerable<FreeTimeSlotResponse>>> CheckAvailabilityForDay([FromQuery]int userId, [FromQuery]int day, [FromQuery]int month, [FromQuery]int year);
    
    [HttpGet("check_availability_for_week")]
    [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<FreeTimeSlotResponse>))]
    [ProducesResponseType(statusCode: 404, type: typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<IEnumerable<FreeTimeSlotResponse>>> CheckAvailabilityForWeek([FromQuery]int userId, [FromQuery]int weekNumber, [FromQuery]int year);
    
    [HttpGet("check_availability_for_month")]
    [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<FreeTimeSlotResponse>))]
    [ProducesResponseType(statusCode: 404, type: typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<IEnumerable<FreeTimeSlotResponse>>> CheckAvailabilityForMonth([FromQuery]int userId, [FromQuery]int month, [FromQuery]int year);
    
    [HttpGet("check_availability_for_interval")]
    [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<FreeTimeSlotResponse>))]
    [ProducesResponseType(statusCode: 404, type: typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<IEnumerable<FreeTimeSlotResponse>>> CheckAvailabilityForInterval([FromQuery]int userId, [FromQuery]int startDateDay, [FromQuery]int startDateMonth, [FromQuery]int startDateYear, [FromQuery]int endDateDay, [FromQuery]int endDateMonth, [FromQuery]int endDateYear);

    [HttpGet("day_with_most_appointments_from_month")]
    [ProducesResponseType(statusCode: 200, type: typeof(DateResponse))]
    [ProducesResponseType(statusCode: 404, type: typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<DateResponse>> GetDayWithMostAppointmentsFromMonth([FromQuery] int month, [FromQuery]int year);
    
    [HttpGet("day_with_most_appointments_from_week")]
    [ProducesResponseType(statusCode: 200, type: typeof(DateResponse))]
    [ProducesResponseType(statusCode: 404, type: typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<DateResponse>> GetDayWithMostAppointmentsFromWeek([FromQuery] int weekNumber, [FromQuery]int year);
}