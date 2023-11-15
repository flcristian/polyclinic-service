using Microsoft.AspNetCore.Mvc;
using polyclinic_service.Users.DTOs;
using polyclinic_service.Users.Models;

namespace polyclinic_service.Users.Controllers.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class UsersApiController : ControllerBase
{
    [HttpGet("all")]
    [ProducesResponseType(statusCode:200,type:typeof(IEnumerable<User>))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<IEnumerable<User>>> GetAllUsers();
    
    [HttpGet("user/{id}")]
    [ProducesResponseType(statusCode:200,type:typeof(User))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<User>> GetUserById([FromRoute]int id);
    
    [HttpPost("create")]
    [ProducesResponseType(statusCode:201,type:typeof(User))]
    [Produces("application/json")]
    public abstract Task<ActionResult<User>> CreateUser([FromBody]CreateUserRequest userRequest);
    
    [HttpPut("update")]
    [ProducesResponseType(statusCode:202,type:typeof(User))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<User>> UpdateUser([FromBody]UpdateUserRequest userRequest);
    
    [HttpDelete("delete/{id}")]
    [ProducesResponseType(statusCode:202,type:typeof(String))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult> DeleteUser([FromRoute]int id);
}