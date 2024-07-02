using BusinessEntities.Identity;
using BusinessLogic.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace HrService.Controllers;

[Route("Api/Auth/[controller]")]
[Authorize]
[ApiController]
public class PermissionController : ControllerBase
{
    BizPermissionManagement m_BizPermissionMgr;

    public PermissionController()
    {

        m_BizPermissionMgr = new BizPermissionManagement();

    }

    [HttpGet("Get")]
    public IActionResult GetPermission()
    {
        ResultMessage res = new ResultMessage();
        res = m_BizPermissionMgr.GetAllPermission();
        return Ok(res);
    }

    [HttpGet("Get/{id}")]
    public IActionResult GetPermissionById(int id)
    {
        ResultMessage res = new ResultMessage();
        //  User.FindFirst("Codemobiles");
        res = m_BizPermissionMgr.GetPermissionById(id);
        return Ok(res);
    }

    [HttpPost("AddNew")]
    public IActionResult AddNewPermission([FromBody] tbm_permission model)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizPermissionMgr.AddNewPermission(model);
        return Ok(res);
    }
    [HttpPut("Update")]
    public IActionResult UpdatePermission([FromBody] tbm_permission model)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizPermissionMgr.UpdatePermission(model);
        return Ok(res);
    }

    [HttpDelete("Delete/{id}")]
    public IActionResult DeletePermission(int id)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizPermissionMgr.DeletePermission(id);
        return Ok(res);
    }


  
}

