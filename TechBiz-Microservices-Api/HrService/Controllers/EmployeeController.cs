using BusinessEntities.HR.MasterModels;
using BusinessLogic.HR.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace HrService.Controllers;

[Route("Api/Auth/[controller]")]
[Authorize]
[ApiController]
public class EmployeeController : ControllerBase
{
    BizEmployeeManagement m_BizEmployeeMgr;

    public EmployeeController()
    {
        m_BizEmployeeMgr = new BizEmployeeManagement();
    }
    


    [HttpGet("Get")]
    public IActionResult GetEmployee()
    {
        ResultMessage res = new ResultMessage();
        res = m_BizEmployeeMgr.GetAllEmployee();
        return Ok(res);
    }

    [HttpGet("Get/{id}")]
    public IActionResult GetEmployeeById(int id)
    {
        ResultMessage res = new ResultMessage();
        //  User.FindFirst("Codemobiles");
        res = m_BizEmployeeMgr.GetEmployeeById(id);
        return Ok(res);
    }

    [HttpPost("AddNew")]
    public IActionResult AddNewEmployee([FromBody] tm_employee_info model)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizEmployeeMgr.AddNewEmployee(model);
        return Ok(res);
    }
    [HttpPut("Update")]
    public IActionResult UpdateEmployee([FromBody] tm_employee_info model)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizEmployeeMgr.UpdateEmployee(model);
        return Ok(res);
    }

    [HttpDelete("Delete/{id}")]
    public IActionResult DeleteEmployee(int id)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizEmployeeMgr.DeleteEmployee(id);
        return Ok(res);
    }

    [HttpGet("GetPaginate")]
    public IActionResult GetAllEmployee([FromQuery] QueryParameter queryParameters)
    {
        ResultMessage res = new ResultMessage();
        //  User.FindFirst("Codemobiles");
        res = m_BizEmployeeMgr.GetAllEmployee(queryParameters);
        return Ok(res);
    }

    [HttpGet("ActivateCondition")]
    public IActionResult ActivateCondition(int id, int loginId, bool is_active)
    {
        ResultMessage resultMessage = m_BizEmployeeMgr.ActivateCondition(id, loginId, is_active);
        return Ok(resultMessage);
    }





}
