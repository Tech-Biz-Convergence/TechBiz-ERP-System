using BusinessEntities.HR.MasterModels;
using BusinessLogic.HR.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace LeaveTypeService.Controllers
{

    [Route("Api/Hr/[controller]")]
    [Authorize]
    [ApiController]
    public class LeaveTypeController : ControllerBase
    {
        BizLeaveTypeManagement m_BizLeaveTypeMgr;

        public LeaveTypeController()
        {
            m_BizLeaveTypeMgr = new BizLeaveTypeManagement();
        }

        [HttpGet("Get")]
        public IActionResult GetLeave()
        {
            ResultMessage res = new ResultMessage();
            res = m_BizLeaveTypeMgr.GetAllLeave();
            return Ok(res);
        }

        [HttpGet("Get/{id}")]
        public IActionResult GetLeaveById(int id)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizLeaveTypeMgr.GetLeaveById(id);
            return Ok(res);
        }

        [HttpPost("AddNew")]
        public IActionResult AddNewLeave([FromBody] tbm_leave_type model)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizLeaveTypeMgr.AddNewLeave(model);
            return Ok(res);
        }

        [HttpPut("Update")]
        public IActionResult UpdateLeave([FromBody] tbm_leave_type model)
        { 
            ResultMessage res = new ResultMessage();
            res = m_BizLeaveTypeMgr.UpdateLeave(model);
            return Ok(res);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteLeave(int id)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizLeaveTypeMgr.DeleteLeave(id);
            return Ok(res);
        }

        [HttpGet("GetPaginate")]
        public IActionResult GetAllLeave([FromQuery] QueryParameter queryParameter)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizLeaveTypeMgr.GetAllLeave(queryParameter);
            return Ok(res);
        }

        [HttpPost("ImportDataExcleFile")]
        public IActionResult ImportDataExcleFile([FromBody] IFormFile file)
        { 
            ResultMessage resultMessage = m_BizLeaveTypeMgr.ImportDataExcelFile(file);
            return Ok(resultMessage);
        }

        //[HttpGet("GetLeaveType")]
        //public IActionResult GetLeaveType()
        //{
        //    ResultMessage res = m_BizLeaveTypeMgr.GetLeaveType();
        //    return Ok(res);
        //}
    }
}
