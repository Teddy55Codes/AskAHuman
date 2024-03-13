using AskAHuman.Services.Interfaces;
using DataBaseLayer.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AskAHuman.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IAuthenticationService authenticationService, IUserService userService) : ControllerBase
{
    
    [AllowAnonymous]
    [HttpPost("/login")] 
    public IActionResult Login([FromBody] LoginDTO loginDTO) 
    { 
        string? tokenString = userService.Login(loginDTO.Username, loginDTO.Password);
        
        if (tokenString == null) return Unauthorized();
        IActionResult response = Ok(new { token = tokenString });
        
        return response;
    }
    
    [AllowAnonymous]
    [HttpPost("/register")] 
    public IActionResult Register([FromBody] LoginDTO loginDTO) => userService.Register(loginDTO.Username, loginDTO.Password) ? Ok() : BadRequest();

    
    [AllowAnonymous]
    [HttpGet("exists")]
    public IActionResult Exists(string username) => Ok(userService.UserExists(username));
}