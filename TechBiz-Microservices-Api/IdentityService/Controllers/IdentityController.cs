using IdentityService.Repository;
using Microsoft.AspNetCore.Mvc;
using Middleware;
using Utilities;
using BusinessLogic;
using BusinessLogic.Identity;
using BusinessEntities.Identity;

namespace IdentityMicroservice.Controllers;

[Route("Api/[controller]")]
[ApiController]
public class IdentityController(IUserRepository userRepository, IJwtBuilder jwtBuilder, IEncryptor encryptor)
    : ControllerBase
{
    [HttpPost("Login")]
    public IActionResult Login([FromBody] user_login model, [FromQuery(Name = "d")] string destination = "frontend")
    {
        ResultMessage resultMessage = new ResultMessage();
        BizIdentityManagement m_BizIdentityMgr = new BizIdentityManagement(jwtBuilder, encryptor);
        resultMessage = m_BizIdentityMgr.Authenticated(model);
        return Ok(resultMessage);
    }

    [HttpPost("Register")]
    public IActionResult Register([FromBody] tbm_user_info model)
    {
        ResultMessage resultMessage = new ResultMessage();
        BizIdentityManagement m_BizIdentityMgr = new BizIdentityManagement(jwtBuilder,encryptor);
        resultMessage = m_BizIdentityMgr.Register(model);
        return Ok(resultMessage);
        
    }

    [HttpGet("Validate")]
    public IActionResult Validate([FromQuery(Name = "user_name")] int userId, [FromQuery(Name = "token")] string token)
    {
        ResultMessage resultMessage = new ResultMessage();
        BizIdentityManagement m_BizIdentityMgr = new BizIdentityManagement(jwtBuilder, encryptor);
        resultMessage = m_BizIdentityMgr.Validate(userId, token);
        return Ok(resultMessage);
    }
}