using BusinessEntities.Identity;
using BusinessLogic.Identity;
using DataLayer.Identitys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace HrService.Controllers;

[Route("Api/Auth/[controller]")]
[Authorize]
[ApiController]
public class MenuController : ControllerBase
{
    BizMenuManagement m_BizMenuMgr;

    public MenuController()
    {

        m_BizMenuMgr = new BizMenuManagement();

    }

    [HttpGet("GetMenu")]
    public IActionResult GetMenu()
    {
        ResultMessage res = new ResultMessage();
        res = m_BizMenuMgr.GetMenu();
        return Ok(res);
    }

    [HttpGet("GetMenuUserRoleMapping")]
    public IActionResult GetMenuUserRoleMapping(string user_name)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizMenuMgr.GetMenuUserRoleMapping(user_name);
        return Ok(res);
    }




}

