﻿using BusinessEntities.HR.MasterModels;
using BusinessLogic.HR.Master;
using BusinessLogic.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace HrService.Controllers;

[Route("api/hr/[controller]")]
[Authorize]
[ApiController]
public class JobController : ControllerBase
{
    BizPermissionRoleMappingManagement m_BizPerMgr;
    BizJobManagement m_BizJobMgr;

    public JobController()
    {
        m_BizPerMgr = new BizPermissionRoleMappingManagement();
        m_BizJobMgr = new BizJobManagement();        

    }
    


    [HttpGet("Get")]
    public IActionResult GetJob()
    {
        ResultMessage res = new ResultMessage();
        res = m_BizJobMgr.GetAllJob();
        return Ok(res);
    }

    [HttpGet("Get/{id}")]
    public IActionResult GetJobById(int id)
    {
        ResultMessage res = new ResultMessage();
        //  User.FindFirst("Codemobiles");
        res = m_BizJobMgr.GetJobById(id);
        return Ok(res);
    }

    [HttpPost("AddNew")]
    public IActionResult AddNewJob([FromBody] tbm_hr_job model)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizJobMgr.AddNewJob(model);
        return Ok(res);
    }
    [HttpPut("Update")]
    public IActionResult UpdateJob([FromBody] tbm_hr_job model)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizJobMgr.UpdateJob(model);
        return Ok(res);
    }

    [HttpDelete("Delete/{id}")]
    public IActionResult DeleteJob(int id)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizJobMgr.DeleteJob(id);
        return Ok(res);
    }

    [HttpGet("GetPaginate")]
    public IActionResult GetAllJob([FromQuery] QueryParameter queryParameters)
    {
        ResultMessage res = new ResultMessage();
        //  User.FindFirst("Codemobiles");
        res = m_BizJobMgr.GetAllJob(queryParameters);
        return Ok(res);
    }

    [HttpGet("ActivateCondition/{id}")]
    public IActionResult ActivateCondition(int id, [FromQuery] string user_name, [FromQuery] string hr_job_status)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizPerMgr.CheckEditPermission(user_name);
        if (res.status)
        {
            res = m_BizJobMgr.ActivateCondition(id, user_name, hr_job_status);
        }
        return Ok(res);

        ResultMessage resultMessage = m_BizJobMgr.ActivateCondition(id, user_name, hr_job_status);
        return Ok(resultMessage);
    }

    [HttpPost("ImportDataExcelFile")]
    public  IActionResult ImportDataExcelFile([FromBody]IFormFile file)
    {
        ResultMessage resultMessage = m_BizJobMgr.ImportDataExcelFile(file);
        return Ok(resultMessage);
    }

    [HttpGet("GetJobName")]
    public IActionResult GetJobName()
    {
        ResultMessage res = m_BizJobMgr.GetJobName();
        return Ok(res);
    }
}

