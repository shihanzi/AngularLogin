using AngularLogin.Context;
using AngularLogin.Helpers;
using AngularLogin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AngularLogin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _authDbContext;
        public UserController(AppDbContext appDbContext)
        {
            _authDbContext = appDbContext;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User loginObj)
        {
            if (loginObj == null)
                return BadRequest();
            
            //username exist
            var user = await _authDbContext.Users
                    .FirstOrDefaultAsync(x => x.UserName == loginObj.UserName);

                if (user == null)
                    return NotFound(new { Message = "User Not Found" });

                if (!PasswordHasher.VerifyPassword(loginObj.Password , user.Password))
                {
                    return BadRequest(new { Message = "Incorrect Password" });
                }
            
            return Ok(new
            {
                Token = "",
            Message = "Login Success" });
        }

        [HttpPost("registeruser")]
        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            if (await CheckUserNameExistAsync(userObj.UserName))
                return BadRequest(new { Message = "Username Already Exist" });

            if (await CheckEmailExistAsync(userObj.Email))
                return BadRequest(new { Message = "Email Already Exist" });

            var pass = PasswordStrength(userObj.Password);
            if (!string.IsNullOrEmpty(pass))
                return BadRequest(new { Message = pass.ToString() }) ;

            userObj.Password = PasswordHasher.HashPassword(userObj.Password);
            userObj.Role = "User";
            userObj.Token = "";
            await _authDbContext.Users.AddAsync(userObj);
            await _authDbContext.SaveChangesAsync();
            return Ok(new { Message = "User Registered Successfully" });

        }
        private Task <bool> CheckUserNameExistAsync(string username)
            => _authDbContext.Users.AnyAsync(x => x.UserName == username);

        private Task<bool> CheckEmailExistAsync(string email)
            => _authDbContext.Users.AnyAsync(x => x.Email == email);

        private string PasswordStrength(string password)
        {
            StringBuilder sb = new StringBuilder();
            if (password.Length < 8)
                sb.Append("Password Length Should be more than 8 Characters" + Environment.NewLine);
            if (!(Regex.IsMatch(password,"[a-z]") && Regex.IsMatch(password, "[A-Z]") &&
                Regex.IsMatch(password, "[0-9]")))
                sb.Append("Password Should Contain Capitol, Small Letters & Numbers" + Environment.NewLine);
            if (!(Regex.IsMatch(password, "[!#$%&'()*+,-./:;<=>?@_`{|}~]")))
                sb.Append("Password Should Contain Special Characters" + Environment.NewLine);
            return sb.ToString();
        }
        private string CreateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysecret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role,user.Role),
                new Claim(ClaimTypes.Name,$"{user.FirstName} {user.LastName}")
            }
                );
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(2),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }

}
