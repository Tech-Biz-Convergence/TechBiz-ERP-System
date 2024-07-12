using BusinessEntities.HR.MasterModels;
using BusinessLogic.HR.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace DepartmentService.Controllers
{

    [Route("Api/Hr/[controller]")]
    [Authorize]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        BizDepartmentManagement m_BizDepartmentMgr;

        public DepartmentController()
        {
            m_BizDepartmentMgr = new BizDepartmentManagement();
        }

        [HttpGet("Get")]
        public IActionResult GetDepartment()
        {
            ResultMessage res = new ResultMessage();
            res = m_BizDepartmentMgr.GetAllDepartment();
            return Ok(res);
        }

        [HttpGet("Get/{id}")]
        public IActionResult GetDepartmentById(int id)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizDepartmentMgr.GetDepartmentById(id);
            return Ok(res);
        }

        [HttpPost("AddNew")]
        public IActionResult AddNewDepartment([FromBody] tbm_dept_info model)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizDepartmentMgr.AddNewDepartment(model);
            return Ok(res);
        }

        [HttpPut("Update")]
        public IActionResult UpdateDepartment([FromBody] tbm_dept_info model)
        { 
            ResultMessage res = new ResultMessage();
            res = m_BizDepartmentMgr.UpdateDepartment(model);
            return Ok(res);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizDepartmentMgr.DeleteDepartment(id);
            return Ok(res);
        }

        [HttpGet("GetPaginate")]
        public IActionResult GetAllDepartment([FromQuery] QueryParameter queryParameter)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizDepartmentMgr.GetAllDepartment(queryParameter);
            return Ok(res);
        }

        [HttpPost("ImportDataExcleFile")]
        public IActionResult ImportDataExcleFile([FromBody] IFormFile file)
        { 
            ResultMessage resultMessage = m_BizDepartmentMgr.ImportDataExcelFile(file);
            return Ok(resultMessage);
        }
    }
}
