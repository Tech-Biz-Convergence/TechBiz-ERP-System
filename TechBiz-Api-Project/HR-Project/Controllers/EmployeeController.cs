using BusinessEntities.HR.MasterModels;
using BusinessLogic.HR.Master;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace TechBiz_Api_Project.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        BizEmployeeManagement m_BizEmployeeMgr;
        
        public EmployeeController()
        {
            m_BizEmployeeMgr =new BizEmployeeManagement();
        }
        [HttpGet("GetEmployee")]
        public IActionResult GetEmployee()
        {
            ResultMessage res = new ResultMessage();
            //  User.FindFirst("Codemobiles");
            res = m_BizEmployeeMgr.GetAllEmployee();
            return Ok(res);
        }

        [HttpGet("GetEmployee/{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            ResultMessage res = new ResultMessage();
            //  User.FindFirst("Codemobiles");
            res = m_BizEmployeeMgr.GetEmployeeById(id);
            return Ok(res);
        }

        [HttpPost("AddNewEmployee")]
        public IActionResult AddNewEmployee([FromBody] tm_employee_info model)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizEmployeeMgr.AddNewEmployee(model);
            return Ok(res);
        }
    }
    
}
