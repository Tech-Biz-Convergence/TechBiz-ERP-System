using BusinessEntities.HR.MasterModels;
using BusinessLogic.HR.Master;
using BusinessLogic.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace HrService.Controllers;
[Route("Api/Hr/[controller]")]
[Authorize]
[ApiController]
public class DepartmentController : Controller
{
    BizDepartmentManagement m_BizDeptMgr;
    public DepartmentController()
    {
        m_BizDeptMgr = new BizDepartmentManagement();

    }

    [HttpGet("Get")]
    public IActionResult GetDepartment()
    {
        ResultMessage res = new ResultMessage();
        res = m_BizDeptMgr.GetAllDepartment();
        return Ok(res);
    }

    [HttpGet("Get/{id}")]
    public IActionResult GetDepartmentById(int id)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizDeptMgr.GetDepartmentById(id);
        return Ok(res);
    }

    [HttpPost("AddNew")]
    public IActionResult AddNewDepartment([FromBody] tbm_dept_info model)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizDeptMgr.AddNewDepartment(model);
        return Ok(res);
    }

    [HttpPut("Update")]
    public IActionResult UpdateDepartment([FromBody] tbm_dept_info model)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizDeptMgr.UpdateDepartment(model);
        return Ok(res);
    }

    [HttpDelete("Delete/{id}")]
    public IActionResult DeleteDepartment(int id)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizDeptMgr.DeleteDepartment(id);
        return Ok(res);
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
