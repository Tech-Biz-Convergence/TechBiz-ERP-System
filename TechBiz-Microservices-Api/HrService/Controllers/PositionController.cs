using BusinessEntities.HR.MasterModels;
using BusinessLogic.HR.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace PositionService.Controllers
{

    [Route("Api/Hr/[controller]")]
    [Authorize]
    [ApiController]
    public class PositionController : ControllerBase
    {
        BizPositionManagement m_BizPositionMgr;

        public PositionController()
        {
            m_BizPositionMgr = new BizPositionManagement();
        }

        [HttpGet("Get")]
        public IActionResult GetPosition()
        {
            ResultMessage res = new ResultMessage();
            res = m_BizPositionMgr.GetAllPosition();
            return Ok(res);
        }

        [HttpGet("Get/{id}")]
        public IActionResult GetPositionById(int id)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizPositionMgr.GetPositionById(id);
            return Ok(res);
        }

        [HttpPost("AddNew")]
        public IActionResult AddNewPosition([FromBody] tbm_position model)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizPositionMgr.AddNewPosition(model);
            return Ok(res);
        }

        [HttpPut("Update")]
        public IActionResult UpdatePosition([FromBody] tbm_position model)
        { 
            ResultMessage res = new ResultMessage();
            res = m_BizPositionMgr.UpdatePosition(model);
            return Ok(res);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeletePosition(int id)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizPositionMgr.DeletePosition(id);
            return Ok(res);
        }

        [HttpGet("GetPaginate")]
        public IActionResult GetAllPosition([FromQuery] QueryParameter queryParameter)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizPositionMgr.GetAllPosition(queryParameter);
            return Ok(res);
        }

    }
}
