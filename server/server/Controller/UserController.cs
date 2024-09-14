using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using server.DTO;
using server.Model;
using server.Service;

namespace server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly TokenService _tokenService;

        public UserController(UserService userService, TokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        } 
        
        // GET
        [Authorize]
        [HttpGet("")]
        public IActionResult Information()
        {
            UserDto? user = null;

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string username = null;
            string email = null;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;
                email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value;
            }

            if (!username.IsNullOrEmpty() || !email.IsNullOrEmpty())
            {
                user = _userService.GetInformation(username, email);
            }
            
            return user == null ? NotFound("User not found") : Ok(user);
        }
        
        // PUT
        [Authorize]
        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] UserDto user)
        {
            UserDto? newUser = null;

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string username = null;
            string email = null;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;
                email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value;
            }

            if (!username.IsNullOrEmpty() || !email.IsNullOrEmpty())
            {
                newUser = await _userService.UpdateInformation(username, email, user);
            }

            if (newUser == null)
            {
                return NotFound("Information can't be update");
            }
            var newToken = _tokenService.Generate(newUser);

            return Ok(
                new
                    {
                        user = newUser, 
                        token = newToken
                    }
                );
        }
        
        // DELETE
        [Authorize]
        [HttpDelete("")]
        public IActionResult Delete()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string username = null;
            string email = null;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;
                email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value;
            }

            if (!username.IsNullOrEmpty() || !email.IsNullOrEmpty())
            {
                _userService.DeleteUser(username, email);
            }
            
            return Ok("User deleted");
        }
    }
};

