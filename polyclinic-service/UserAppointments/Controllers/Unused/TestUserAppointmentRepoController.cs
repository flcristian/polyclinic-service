/*using Microsoft.AspNetCore.Mvc;
using polyclinic_service.UserAppointments.DTOs;
using polyclinic_service.UserAppointments.Models;
using polyclinic_service.UserAppointments.Repository.Interfaces;

namespace polyclinic_service.UserAppointments.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TestUserAppointmentRepoController : ControllerBase
{
    private IUserAppointmentRepository _repository;

    public TestUserAppointmentRepoController(IUserAppointmentRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<UserAppointment>>> GetAllUserAppointments()
    {
        return Ok(await _repository.GetAllAsync());
    }
    
    [HttpGet("UserAppointment/{id}")]
    public async Task<ActionResult<UserAppointment>> GetUserAppointmentById([FromRoute]int id)
    {
        return Ok(await _repository.GetByIdAsync(id));
    }
    
    [HttpPost("create")]
    public async Task<ActionResult<UserAppointment>> CreateUserAppointment([FromBody]CreateUserAppointmentRequest userAppointmentRequest)
    {
        return Ok(await _repository.CreateAsync(userAppointmentRequest));
    }
    
    [HttpPut("update")]
    public async Task<ActionResult<UserAppointment>> UpdateUserAppointment([FromQuery]int id, [FromBody]UpdateUserAppointmentRequest userAppointmentRequest)
    {
        return Ok(await _repository.UpdateAsync(id, userAppointmentRequest));
    }
    
    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteUserAppointment([FromQuery]int id)
    {
        await _repository.DeleteAsync(id);
        return Ok("It worked.");
    }
}*/