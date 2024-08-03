using BusinessEntities.HR.MasterModels;
using BusinessLogic.HR.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace HrService.Controllers;

[Route("api/hr/[controller]")]
[Authorize]
[ApiController]
public class ScheduleController : ControllerBase
{
    BizScheduleManagement m_BizScheduleMgr;

    public ScheduleController()
    {

        m_BizScheduleMgr = new BizScheduleManagement();

    }
    


    [HttpGet("Get")]
    public IActionResult GetSchedule()
    {
        ResultMessage res = new ResultMessage();
        res = m_BizScheduleMgr.GetAllSchedule();
        return Ok(res);
    }

    [HttpGet("Get/{id}")]
    public IActionResult GetScheduleById(int id)
    {
        ResultMessage res = new ResultMessage();
        //  User.FindFirst("Codemobiles");
        res = m_BizScheduleMgr.GetScheduleById(id);
        return Ok(res);
    }

    [HttpPost("AddNew")]
    public IActionResult AddNewSchedule([FromBody] tbm_hr_schedule model)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizScheduleMgr.AddNewSchedule(model);
        return Ok(res);
    }
    [HttpPut("Update")]
    public IActionResult UpdateSchedule([FromBody] tbm_hr_schedule model)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizScheduleMgr.UpdateSchedule(model);
        return Ok(res);
    }

    [HttpDelete("Delete/{id}")]
    public IActionResult DeleteSchedule(int id)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizScheduleMgr.DeleteSchedule(id);
        return Ok(res);
    }

    [HttpGet("GetPaginate")]
    public IActionResult GetAllSchedule([FromQuery] QueryParameter queryParameters)
    {
        ResultMessage res = new ResultMessage();
        //  User.FindFirst("Codemobiles");
        res = m_BizScheduleMgr.GetAllSchedule(queryParameters);
        return Ok(res);
    }

    [HttpGet("ActivateCondition/{id}")]
    public IActionResult ActivateCondition(int id, [FromQuery] string user_name, [FromQuery] string schedule_status)
    {
        ResultMessage resultMessage = m_BizScheduleMgr.ActivateCondition(id, user_name, schedule_status);
        return Ok(resultMessage);
    }    
}

