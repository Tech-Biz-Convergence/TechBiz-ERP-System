using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessEntities.HR.MasterModels;
using BusinessLogic.HR.Master;
using Utilities;

//JWT = https://www.youtube.com/watch?v=9O0vnOTUd9o
//JWT GET TOKEN = https://www.youtube.com/watch?v=kM1fPt1BcLc&t=155s
namespace HR_Project.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class JwtController : Controller
    {
        private IConfiguration Config { get; }
        public JwtController(IConfiguration config)
        {
            Config = config;
        }


        [AllowAnonymous]
        [HttpPost("CreateToken")]
        public IActionResult CreateToken([FromBody]UserModel model)
        {
            IActionResult response = Unauthorized();
            var user = Authenticated(model);
            if (user != null)
            {
                var tokenString = BuildToken(model);
                response = Ok(new { token = tokenString });
            }
            return response;
        }
        private IActionResult Authenticated(UserModel model)
        {
            ResultMessage resultMessage = new ResultMessage();
            var bizUser= new BizUserManagement();
            resultMessage = bizUser.Authenticated(model);
            return Ok(resultMessage);
        }

        private string BuildToken(UserModel model)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(Config["Jwt:Expires"]));

            var claims = new[]
            {
                /*new Claim(JwtRegisteredClaimNames.Sub,model.user_name),
                new Claim(JwtRegisteredClaimNames.Email,model.user_Email),
                new Claim("Codemobiles","Learn JWT BY Codemobiles"),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()), */

                new Claim(JwtRegisteredClaimNames.NameId,model.Username),
                new Claim(JwtRegisteredClaimNames.Email,model.EmailAddress),
                new Claim(JwtRegisteredClaimNames.GivenName,model.Givename),
                new Claim(JwtRegisteredClaimNames.FamilyName,model.Surname),
                new Claim("role", model.Role)
            };

            var token = new JwtSecurityToken(
                Config["Jwt:Issuer"],
                Config["Jwt:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
