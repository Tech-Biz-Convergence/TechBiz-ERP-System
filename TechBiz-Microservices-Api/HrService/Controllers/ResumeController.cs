using BusinessEntities.HR.MasterModels;
using BusinessLogic.HR.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace HrService.Controllers;

[Route("api/hr/[controller]")]
[Authorize]
[ApiController]
public class ResumeController : ControllerBase
{
    BizResumeManagement m_BizResumeMgr;

    public ResumeController()
    {

        m_BizResumeMgr = new BizResumeManagement();

    }
    


    [HttpGet("Get")]
    public IActionResult GetResume()
    {
        ResultMessage res = new ResultMessage();
        res = m_BizResumeMgr.GetAllResume();
        return Ok(res);
    }

    [HttpGet("Get/{id}")]
    public IActionResult GetResumeById(int id)
    {
        ResultMessage res = new ResultMessage();
        //  User.FindFirst("Codemobiles");
        res = m_BizResumeMgr.GetResumeById(id);
        return Ok(res);
    }

    [HttpPost("AddNew")]
    public IActionResult AddNewResume([FromBody] tbm_hr_resume model)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizResumeMgr.AddNewResume(model);
        return Ok(res);
    }
    [HttpPut("Update")]
    public IActionResult UpdateResume([FromBody] tbm_hr_resume model)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizResumeMgr.UpdateResume(model);
        return Ok(res);
    }

    [HttpDelete("Delete/{id}")]
    public IActionResult DeleteResume(int id)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizResumeMgr.DeleteResume(id);
        return Ok(res);
    }

    [HttpGet("GetPaginate")]
    public IActionResult GetAllResume([FromQuery] QueryParameter queryParameters)
    {
        ResultMessage res = new ResultMessage();
        //  User.FindFirst("Codemobiles");
        res = m_BizResumeMgr.GetAllResume(queryParameters);
        return Ok(res);
    }

    [HttpGet("ActivateCondition/{id}")]
    public IActionResult ActivateCondition(int id, [FromQuery] string user_name, [FromQuery] string resume_status)
    {
        ResultMessage resultMessage = m_BizResumeMgr.ActivateCondition(id, user_name, resume_status);
        return Ok(resultMessage);
    }    
}

