/*using Microsoft.AspNetCore.Mvc;
using polyclinic_service.Appointments.DTOs;
using polyclinic_service.Appointments.Models;
using polyclinic_service.Appointments.Repository.Interfaces;

namespace polyclinic_service.Appointments.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TestAppointmentRepoController : ControllerBase
{
    private IAppointmentRepository _repository;

    public TestAppointmentRepoController(IAppointmentRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<Appointment>>> GetAllAppointments()
    {
        return Ok(await _repository.GetAllAsync());
    }
    
    [HttpGet("appointment/{id}")]
    public async Task<ActionResult<Appointment>> GetAppointmentById([FromRoute]int id)
    {
        return Ok(await _repository.GetByIdAsync(id));
    }
    
    [HttpPost("create")]
    public async Task<ActionResult<Appointment>> CreateAppointment([FromBody]CreateAppointmentRequest appointmentRequest)
    {
        return Ok(await _repository.CreateAsync(appointmentRequest));
    }
    
    [HttpPut("update")]
    public async Task<ActionResult<Appointment>> UpdateAppointment([FromQuery]int id, [FromBody]UpdateAppointmentRequest appointmentRequest)
    {
        return Ok(await _repository.UpdateAsync(id, appointmentRequest));
    }
    
    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteAppointment([FromQuery]int id)
    {
        await _repository.DeleteAsync(id);
        return Ok("It worked.");
    }
}*/