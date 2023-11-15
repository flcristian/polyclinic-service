/*using Microsoft.AspNetCore.Mvc;
using polyclinic_service.Users.DTOs;
using polyclinic_service.Users.Models;
using polyclinic_service.Users.Repository.Interfaces;

namespace polyclinic_service.Users.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TestUserRepoController : ControllerBase
{
    private IUserRepository _repository;

    public TestUserRepoController(IUserRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        return Ok(await _repository.GetAllAsync());
    }
    
    [HttpGet("user/{id}")]
    public async Task<ActionResult<User>> GetUserById([FromRoute]int id)
    {
        return Ok(await _repository.GetByIdAsync(id));
    }
    
    [HttpPost("create")]
    public async Task<ActionResult<User>> CreateUser([FromBody]CreateUserRequest userRequest)
    {
        return Ok(await _repository.CreateAsync(userRequest));
    }
    
    [HttpPut("update")]
    public async Task<ActionResult<User>> UpdateUser([FromQuery]int id, [FromBody]UpdateUserRequest userRequest)
    {
        return Ok(await _repository.UpdateAsync(id, userRequest));
    }
    
    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteUser([FromQuery]int id)
    {
        await _repository.DeleteAsync(id);
        return Ok("It worked.");
    }
}*/