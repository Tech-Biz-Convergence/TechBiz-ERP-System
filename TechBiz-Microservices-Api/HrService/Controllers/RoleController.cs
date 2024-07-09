using BusinessEntities.HR.MasterModels;
using BusinessLogic.HR.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace HrService.Controllers;

[Route("Api/Auth/[controller]")]
[Authorize]
[ApiController]

public class RoleController : Controller
{
    BizRoleManagement m_BizRoleMgr;

    public RoleController()
    {
        m_BizRoleMgr = new BizRoleManagement();
    }



    [HttpGet("Get")]
    public IActionResult GetRole()
    {
        ResultMessage res = new ResultMessage();
        res = m_BizRoleMgr.GetAllRole();
        return Ok(res);
    }

    [HttpGet("Get/{id}")]
    public IActionResult GetRoleById(int id)
    {
        ResultMessage res = new ResultMessage();
        //  User.FindFirst("Codemobiles");
        res = m_BizRoleMgr.GetRoleById(id);
        return Ok(res);
    }

    [HttpPost("AddNew")]
    public IActionResult AddNewRole([FromBody] tbm_role model)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizRoleMgr.AddNewRole(model);
        return Ok(res);
    }
    [HttpPut("Update")]
    public IActionResult UpdateRole([FromBody] tbm_role model)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizRoleMgr.UpdateRole(model);
        return Ok(res);
    }

    [HttpDelete("Delete/{id}")]
    public IActionResult DeleteRole(int id)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizRoleMgr.DeleteRole(id);
        return Ok(res);
    }
}

