using Microsoft.AspNetCore.Mvc;
using ADO.NET.DTOs.Request;
using ADO.NET.Services.Interfaces;

namespace ADO.NET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
            
        }

        [HttpPost("login")]
        
        public async Task<IActionResult> Login (LoginDto request)
        {
            var token = await _authService.IsLogin(request.Username, request.Password);

            if(token == null) 
                return Unauthorized("Invalid credentials");

            return Ok(new { token });
        }
    }
}