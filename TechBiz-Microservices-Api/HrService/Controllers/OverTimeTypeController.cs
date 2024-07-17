using BusinessEntities.HR.MasterModels;
using BusinessLogic.HR.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace HrService.Controllers;

[Route("api/hr/[controller]")]
[Authorize]
[ApiController]
public class OverTimeTypeController : ControllerBase
{
    BizOverTimeTypeManagement m_BizOverTimeTypeMgr;

    public OverTimeTypeController()
    {

        m_BizOverTimeTypeMgr = new BizOverTimeTypeManagement();

    }
    


    [HttpGet("Get")]
    public IActionResult GetOverTimeType()
    {
        ResultMessage res = new ResultMessage();
        res = m_BizOverTimeTypeMgr.GetAllOverTimeType();
        return Ok(res);
    }

    [HttpGet("Get/{id}")]
    public IActionResult GetOverTimeTypeById(int id)
    {
        ResultMessage res = new ResultMessage();
        //  User.FindFirst("Codemobiles");
        res = m_BizOverTimeTypeMgr.GetOverTimeTypeById(id);
        return Ok(res);
    }

    [HttpPost("AddNew")]
    public IActionResult AddNewOverTimeType([FromBody] tbm_overtime_type model)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizOverTimeTypeMgr.AddNewOverTimeType(model);
        return Ok(res);
    }
    [HttpPut("Update")]
    public IActionResult UpdateOverTimeType([FromBody] tbm_overtime_type model)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizOverTimeTypeMgr.UpdateOverTimeType(model);
        return Ok(res);
    }

    [HttpDelete("Delete/{id}")]
    public IActionResult DeleteOverTimeType(int id)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizOverTimeTypeMgr.DeleteOverTimeType(id);
        return Ok(res);
    }

    [HttpGet("GetPaginate")]
    public IActionResult GetAllOverTimeType([FromQuery] QueryParameter queryParameters)
    {
        ResultMessage res = new ResultMessage();
        //  User.FindFirst("Codemobiles");
        res = m_BizOverTimeTypeMgr.GetAllOverTimeType(queryParameters);
        return Ok(res);
    }

    [HttpGet("ActivateCondition")]
    public IActionResult ActivateCondition(int id, int loginId, bool is_active)
    {
        ResultMessage resultMessage = m_BizOverTimeTypeMgr.ActivateCondition(id, loginId, is_active);
        return Ok(resultMessage);
    }    
}

