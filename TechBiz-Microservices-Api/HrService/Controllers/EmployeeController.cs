using BusinessEntities.HR.MasterModels;
using BusinessEntities.Identity;
using BusinessLogic.HR.Master;
using BusinessLogic.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace HrService.Controllers;

[Route("Api/Auth/[controller]")]
[Authorize]
[ApiController]
public class EmployeeController : ControllerBase
{
    BizPermissionRoleMappingManagement m_BizPerMgr;
    BizEmployeeManagement m_BizEmployeeMgr;



    public EmployeeController()
    {
        m_BizPerMgr = new BizPermissionRoleMappingManagement();
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
    public IActionResult AddNewEmployee([FromBody] tbm_employee_info model)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizPerMgr.CheckAddPermission(model.create_by);
        if(res.status)
        {
            res = m_BizEmployeeMgr.AddNewEmployee(model);
        }
        return Ok(res);
    }
    [HttpPut("Update")]
    public IActionResult UpdateEmployee([FromBody] tbm_employee_info model)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizEmployeeMgr.UpdateEmployee(model);
        return Ok(res);
    }

    [HttpDelete("Delete/{id}")]
    public IActionResult DeleteEmployee(int emp_id, string user_name)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizPerMgr.CheckDeletePermission(user_name);
        if (res.status)
        {
            res = m_BizEmployeeMgr.DeleteEmployee(emp_id,user_name);
        }
        return Ok(res);
    }

    [HttpGet("GetPaginate")]
    public IActionResult GetAllEmployee([FromQuery] QueryParameter queryParameters)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizEmployeeMgr.GetAllEmployee(queryParameters);
        return Ok(res);
    }

    [HttpGet("ActivateCondition/{id}")]
    public IActionResult ActivateCondition(int id, [FromQuery] string user_name, [FromQuery] string emp_status)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizPerMgr.CheckEditPermission(user_name);
        if (res.status)
        {
            res = m_BizEmployeeMgr.ActivateCondition(id, user_name, emp_status);
        }
        return Ok(res);
    }

    [HttpPost("ImportDataExcelFile")]
    public  IActionResult ImportDataExcelFile(string user_name,[FromBody]IFormFile file)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizPerMgr.CheckUploadPermission(user_name);
        if (res.status)
        {
            res = m_BizEmployeeMgr.ImportDataExcelFile(file);
        }
        return Ok(res);
    }

}

