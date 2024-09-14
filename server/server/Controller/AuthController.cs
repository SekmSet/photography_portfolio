using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using server.DTO;
using server.Service;

namespace server.Controller {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly TokenService _tokenService;

        public AuthController(UserService userService, TokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        } 
        
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            var user = await _userService.Sign(userDto);
            
            return user == null ?  BadRequest("Username or Email already exists.") : Ok(user.ToDto());
        }
        
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserDto userLogin)
        {
            var user = _userService.Authenticate(userLogin);
            
            if (user != null)
            {
                var token = _tokenService.Generate(user.ToDto());
                return Ok( new { token } );
            }

            return NotFound("User not found");
        }
    }
}