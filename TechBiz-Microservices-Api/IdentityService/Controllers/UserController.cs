using BusinessEntities.Identity;
using BusinessLogic.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace IdentityService.Controllers;

[Route("Api/Auth/[controller]")]
[Authorize]
[ApiController]

public class UserController : Controller
{
    BizUserManagement m_BizUserMgr;

    public UserController()
    {
        m_BizUserMgr = new BizUserManagement();
    }

    [HttpGet("Get")]
    public IActionResult GetUser()
    {
        ResultMessage res = new ResultMessage();
        res = m_BizUserMgr.GetAllUser();
        return Ok(res);
    }

    [HttpGet("Get/{id}")]
    public IActionResult GetUserById(int id)
    {
        ResultMessage res = new ResultMessage();
        //  User.FindFirst("Codemobiles");
        res = m_BizUserMgr.GetUserById(id);
        return Ok(res);
    }

    [HttpPost("AddNew")]
    public IActionResult AddNewUser([FromBody] tbm_user_info model)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizUserMgr.AddNewUser(model);
        return Ok(res);
    }
    [HttpPut("Update")]
    public IActionResult UpdateUser([FromBody] tbm_user_info model)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizUserMgr.UpdateUser(model);
        return Ok(res);
    }

    [HttpDelete("Delete/{id}")]
    public IActionResult DeleteUser(int id)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizUserMgr.DeleteUser(id);
        return Ok(res);
    }

    [HttpGet("GetPaginate")]
    public IActionResult GetAllUser([FromQuery] QueryParameter queryParameters)
    {
        ResultMessage res = new ResultMessage();
        //  User.FindFirst("Codemobiles");
        res = m_BizUserMgr.GetAllUser(queryParameters);
        return Ok(res);
    }

    [HttpGet("ActivateCondition/{id}")]
    public IActionResult ActivateCondition(int id, [FromQuery] string user_name, [FromQuery] string user_status)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizUserMgr.ActivateCondition(id, user_name, user_status);
        return Ok(res);
    }

    [HttpPost("ImportDataExcelFile")]
    public IActionResult ImportDataExcelFile([FromBody] IFormFile file)
    {
        ResultMessage resultMessage = m_BizUserMgr.ImportDataExcelFile(file);
        return Ok(resultMessage);
    }
}

