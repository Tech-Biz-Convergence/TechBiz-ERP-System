using BusinessEntities.HR.MasterModels;
using BusinessLogic.HR.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace HolidayService.Controllers
{

    [Route("Api/Hr/[controller]")]
    [Authorize]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        BizLeaveManagement m_BizLeaveMgr;

        public LeaveController()
        {
            m_BizLeaveMgr = new BizLeaveManagement();
        }

        [HttpGet("Get")]
        public IActionResult GetLeave()
        {
            ResultMessage res = new ResultMessage();
            res = m_BizLeaveMgr.GetAllLeave();
            return Ok(res);
        }

        [HttpGet("Get/{id}")]
        public IActionResult GetLeaveById(int id)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizLeaveMgr.GetLeaveById(id);
            return Ok(res);
        }

        [HttpPost("AddNew")]
        public IActionResult AddNewLeave([FromBody] tbm_leave_type model)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizLeaveMgr.AddNewLeave(model);
            return Ok(res);
        }

        [HttpPut("Update")]
        public IActionResult UpdateLeave([FromBody] tbm_leave_type model)
        { 
            ResultMessage res = new ResultMessage();
            res = m_BizLeaveMgr.UpdateLeave(model);
            return Ok(res);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteLeave(int id)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizLeaveMgr.DeleteLeave(id);
            return Ok(res);
        }

        [HttpGet("GetPaginate")]
        public IActionResult GetAllLeave([FromQuery] QueryParameter queryParameter)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizLeaveMgr.GetAllLeave(queryParameter);
            return Ok(res);
        }

        [HttpPost("ImportDataExcleFile")]
        public IActionResult ImportDataExcleFile([FromBody] IFormFile file)
        { 
            ResultMessage resultMessage = m_BizLeaveMgr.ImportDataExcelFile(file);
            return Ok(resultMessage);
        }
    }
}
