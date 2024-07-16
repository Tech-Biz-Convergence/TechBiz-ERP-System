using BusinessEntities.HR.MasterModels;
using BusinessLogic.HR.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace HrService.Controllers;

[Route("api/hr/[controller]")]
[Authorize]
[ApiController]
public class RecuitController : ControllerBase
{
    BizRecuitManagement m_BizRecuitMgr;

    public RecuitController()
    {

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

    [HttpGet("ActivateCondition")]
    public IActionResult ActivateCondition(int id, int loginId, bool is_active)
    {
        ResultMessage resultMessage = m_BizRecuitMgr.ActivateCondition(id, loginId, is_active);
        return Ok(resultMessage);
    }    
}

