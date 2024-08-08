using BusinessEntities.HR.MasterModels;
using BusinessLogic.HR.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace HrService.Controllers;


[Route("Api/Auth/[controller]")]
[Authorize]
[ApiController]

public class UserTypeController : Controller
{
    BizUserTypeManagement m_BizUserTypeMgr;
    public UserTypeController()
    {
        m_BizUserTypeMgr = new BizUserTypeManagement();

    }

    [HttpGet("Get")]
    public IActionResult GetUserType()
    {
        ResultMessage res = new ResultMessage();
        res = m_BizUserTypeMgr.GetAllUserType();
        return Ok(res);
    }

    [HttpGet("Get/{id}")]
    public IActionResult GetUserTypeById(int id)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizUserTypeMgr.GetUserTypeById(id);
        return Ok(res);
    }

    [HttpPost("AddNew")]
    public IActionResult AddNewUserType([FromBody] tbm_user_type model)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizUserTypeMgr.AddNewUserType(model);
        return Ok(res);
    }

    [HttpPut("Update")]
    public IActionResult UpdateUserType([FromBody] tbm_user_type model)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizUserTypeMgr.UpdateUserType(model);
        return Ok(res);
    }

    [HttpDelete("Delete/{id}")]
    public IActionResult DeleteUserType(int id)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizUserTypeMgr.DeleteUserType(id);
        return Ok(res);
    }

    [HttpGet("GetPaginate")]
    public IActionResult GetAllUserType([FromQuery] QueryParameter queryParameters)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizUserTypeMgr.GetAllUserType(queryParameters);
        return Ok(res);
    }
    [HttpGet("GetUserTypeActive")]
    public IActionResult GetUserTypeActive()
    {
        ResultMessage res = new ResultMessage();
        res = m_BizUserTypeMgr.GetUserTypeActive();
        return Ok(res);

    }
}
