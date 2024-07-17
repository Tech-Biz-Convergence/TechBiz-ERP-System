using BusinessLogic.HR.Master;
using BusinessLogic.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace HrService.Controllers;
[Route("Api/Auth/[controller]")]
[Authorize]
[ApiController]
public class DepartmentController : Controller
{
    BizDepartmentManagement m_BizDeptMgr;
    public DepartmentController()
    {
        m_BizDeptMgr = new BizDepartmentManagement();

    }

    [HttpGet("GetPaginate")]
    public IActionResult GetAllDepartment([FromQuery] QueryParameter queryParameters)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizDeptMgr.GetAllDepartment(queryParameters);
        return Ok(res);
    }
    [HttpGet("GetDepartmentActive")]
    public IActionResult GetDepartmentActive()
    {
        ResultMessage res = new ResultMessage();
        res = m_BizDeptMgr.GetDepartmentActive();
        return Ok(res);

    }
}
