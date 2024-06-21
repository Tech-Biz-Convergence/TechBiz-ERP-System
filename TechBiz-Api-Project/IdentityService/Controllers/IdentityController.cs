using BusinessEntities.HR.MasterModels;
using BusinessLogic.HR.Master;
using BusinessLogic.Identity;
using DataLayer.HR.MasterModels;
using Microsoft.AspNetCore.Mvc;
using Middleware;
using Utilities;


namespace IdentityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : Controller
    {
        private IConfiguration m_Config { get; }
        BizIdentityManagement m_BizIdentityMgr;

        public IdentityController(IConfiguration config)
        {
            m_Config = config;
            m_BizIdentityMgr = new BizIdentityManagement(m_Config);
        }


        [HttpPost("Login")]
        public IActionResult Login(tbm_user_info_role model)
        {
            ResultMessage resultMessage = new ResultMessage();
            resultMessage = m_BizIdentityMgr.Authenticated(model);
            return Ok(resultMessage);
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] tbm_user_info model)
        {
            ResultMessage resultMessage = new ResultMessage();
            resultMessage = m_BizIdentityMgr.Register(model);
            return Ok(resultMessage);
        }


    }
}