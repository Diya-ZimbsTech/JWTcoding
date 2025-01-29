using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestAuthJWT.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService= new JwtService();

        [HttpPost]
        public IActionResult Login([FromBody] LoginInfo loginInfo)
        {
            if (loginInfo.LoginName == "Admin" && loginInfo.Password == "Admin@123")
            { 
                var token = _jwtService.GenerateToken(loginInfo.LoginName);
                return Ok(new { Token=token });
            }
            return Unauthorized();
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetSecureData()
        {
            return Ok("This is the secret message");
        }
    }

    public class LoginInfo
    {
        public string LoginName { get; set; }
        public string Password { get; set; }
    }

}
