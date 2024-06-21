using BusinessEntities.HR.MasterModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HR_Project
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet("GetCurrentUser")]
        public UserModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if ( identity != null)
            {
                var userClaims = identity.Claims;
                return new UserModel
                {
                    Username = userClaims.FirstOrDefault(o => o.Type == JwtRegisteredClaimNames.Sub)?.Value ?? "",
                    Password = userClaims.FirstOrDefault(o => o.Type == JwtRegisteredClaimNames.Email)?.Value ?? "",
                    Givename = userClaims.FirstOrDefault(o => o.Type == JwtRegisteredClaimNames.GivenName)?.Value ?? "",
                    Surname = userClaims.FirstOrDefault(o => o.Type == JwtRegisteredClaimNames.FamilyName)?.Value ?? "",
                    Role = userClaims.FirstOrDefault(o => o.Type == "role")?.Value ?? "",
                };
            }

            return null;
           
        }
    }
}
