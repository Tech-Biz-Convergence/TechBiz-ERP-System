using BusinessEntities.HR.MasterModels;
using BusinessEntities.HR.ProcessModels;
using BusinessLogic.HR.Master;
using BusinessLogic.HR.Process;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Utilities;

namespace HolidayService.Controllers
{

    [Route("Api/Hr/[controller]")]
    [Authorize]
    [ApiController]
    public class ListLeaveController : ControllerBase
    {
        BizListLeaveManagement m_BizListLeaveMgr;

        public ListLeaveController()
        {
            m_BizListLeaveMgr = new BizListLeaveManagement();
        }

        [HttpGet("Get")]
        public IActionResult GetListLeave()
        {
            ResultMessage res = new ResultMessage();
            res = m_BizListLeaveMgr.GetAllListLeave();
            return Ok(res);
        }

        [HttpGet("Get/{id}")]
        public IActionResult GetListLeaveById(int id)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizListLeaveMgr.GetListLeaveById(id);
            return Ok(res);
        }

        [HttpPost("AddNew")]
        public IActionResult AddNewListLeave([FromBody] tbm_leave model)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizListLeaveMgr.AddNewListLeave(model);
            return Ok(res);
        }

        [HttpPut("Update")]
        public IActionResult UpdateListLeave([FromBody] tbm_leave model)
        { 
            ResultMessage res = new ResultMessage();
            res = m_BizListLeaveMgr.UpdateListLeave(model);
            return Ok(res);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteListLeave(int id)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizListLeaveMgr.DeleteListLeave(id);
            return Ok(res);
        }

        [HttpGet("GetPaginate")]
        public IActionResult GetAllListLeave([FromQuery] QueryParameter queryParameter)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizListLeaveMgr.GetAllListLeave(queryParameter);
            return Ok(res);
        }

        [HttpPost("ImportDataExcleFile")]
        public IActionResult ImportDataExcleFile([FromBody] IFormFile file)
        { 
            ResultMessage resultMessage = m_BizListLeaveMgr.ImportDataExcelFile(file);
            return Ok(resultMessage);
        }

        [HttpGet("GetLeaveType")]
        public IActionResult GetLeaveTypesDropdown()
        {
            ResultMessage res = m_BizListLeaveMgr.GetLeaveTypes();
            if (res.status)
            {
                List<SelectListItem> items = new List<SelectListItem>();
                var leaveTypes = res.data as List<tbm_leave_type>;
                foreach (var leaveType in leaveTypes)
                {
                    items.Add(new SelectListItem { Value = leaveType.leave_type_id.ToString(), Text = leaveType.leave_type_name });
                }
                return Ok(items);
            }
            return BadRequest(res);
        }
    }
}
