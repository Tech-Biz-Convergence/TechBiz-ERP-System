using BusinessEntities.HR.MasterModels;
using BusinessLogic.HR.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace CandidateService.Controllers
{

    [Route("Api/Hr/[controller]")]
    [Authorize]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        BizCandidatesManagement m_BizCandidatesMgr;

        public CandidatesController()
        {
            m_BizCandidatesMgr = new BizCandidatesManagement();
        }

        [HttpGet("Get")]
        public IActionResult GetCandidates()
        {
            ResultMessage res = new ResultMessage();
            res = m_BizCandidatesMgr.GetAllCandidates();
            return Ok(res);
        }

        [HttpGet("Get/{id}")]
        public IActionResult GetCandidatesById(int id)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizCandidatesMgr.GetCandidatesById(id);
            return Ok(res);
        }

        [HttpPost("AddNew")]
        public IActionResult AddNewCandidates([FromBody] tbm_hr_candidates model)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizCandidatesMgr.AddNewCandidates(model);
            return Ok(res);
        }

        [HttpPut("Update")]
        public IActionResult UpdateCandidates([FromBody] tbm_hr_candidates model)
        { 
            ResultMessage res = new ResultMessage();
            res = m_BizCandidatesMgr.UpdateCandidates(model);
            return Ok(res);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteCandidates(int id)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizCandidatesMgr.DeleteCandidates(id);
            return Ok(res);
        }

        [HttpGet("GetPaginate")]
        public IActionResult GetAllCandidates([FromQuery] QueryParameter queryParameter)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizCandidatesMgr.GetAllCandidates(queryParameter);
            return Ok(res);
        }

        [HttpPost("ImportDataExcleFile")]
        public IActionResult ImportDataExcleFile([FromBody] IFormFile file)
        { 
            ResultMessage resultMessage = m_BizCandidatesMgr.ImportDataExcelFile(file);
            return Ok(resultMessage);
        }

        [HttpGet("GetCandidatesName")]
        public IActionResult GetCandidatesName()
        {
            ResultMessage res = m_BizCandidatesMgr.GetCandidatesName();
            return Ok(res);
        }
    }
}
