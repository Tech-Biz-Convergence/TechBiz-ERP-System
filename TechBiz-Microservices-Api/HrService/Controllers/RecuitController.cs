using BusinessEntities.HR.MasterModels;
using BusinessLogic.HR.Master;
using BusinessLogic.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace HrService.Controllers;

[Route("api/hr/[controller]")]
[Authorize]
[ApiController]
public class RecuitController : ControllerBase
{
    BizPermissionRoleMappingManagement m_BizPerMgr;
    BizRecuitManagement m_BizRecuitMgr;

    public RecuitController()
    {
        m_BizPerMgr = new BizPermissionRoleMappingManagement();
        m_BizRecuitMgr = new BizRecuitManagement();

    }
    


    [HttpGet("Get")]
    public IActionResult GetRecuit()
    {
        ResultMessage res = new ResultMessage();
        res = m_BizRecuitMgr.GetAllRecuit();
        return Ok(res);
    }

    [HttpGet("Get/{id}")]
    public IActionResult GetRecuitById(int id)
    {
        ResultMessage res = new ResultMessage();
        //  User.FindFirst("Codemobiles");
        res = m_BizRecuitMgr.GetRecuitById(id);
        return Ok(res);
    }

    [HttpPost("AddNew")]
    public IActionResult AddNewRecuit([FromBody] tbm_recuit_stage model)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizRecuitMgr.AddNewRecuit(model);
        return Ok(res);
    }
    [HttpPut("Update")]
    public IActionResult UpdateRecuit([FromBody] tbm_recuit_stage model)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizRecuitMgr.UpdateRecuit(model);
        return Ok(res);
    }

    [HttpDelete("Delete/{id}")]
    public IActionResult DeleteRecuit(int id)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizRecuitMgr.DeleteRecuit(id);
        return Ok(res);
    }

    [HttpGet("GetPaginate")]
    public IActionResult GetAllRecuit([FromQuery] QueryParameter queryParameters)
    {
        ResultMessage res = new ResultMessage();
        //  User.FindFirst("Codemobiles");
        res = m_BizRecuitMgr.GetAllRecuit(queryParameters);
        return Ok(res);
    }

    [HttpGet("ActivateCondition/{id}")]
    public IActionResult ActivateCondition(int id, [FromQuery] string user_name, [FromQuery] string recuit_stage_status)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizPerMgr.CheckEditPermission(user_name);
        if (res.status)
        {
            res = m_BizRecuitMgr.ActivateCondition(id, user_name, recuit_stage_status);
        }
        return Ok(res);

        ResultMessage resultMessage = m_BizRecuitMgr.ActivateCondition(id, user_name, recuit_stage_status);
        return Ok(resultMessage);
    }
}

