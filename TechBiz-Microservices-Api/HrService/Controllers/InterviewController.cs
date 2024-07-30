using BusinessEntities.HR.MasterModels;
using BusinessLogic.HR.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace InterviewService.Controllers
{

    [Route("Api/Hr/[controller]")]
    [Authorize]
    [ApiController]
    public class InterviewController : ControllerBase
    {
        BizInterviewManagement m_BizInterviewMgr;

        public InterviewController()
        {
            m_BizInterviewMgr = new BizInterviewManagement();
        }

        [HttpGet("Get")]
        public IActionResult GetInterview()
        {
            ResultMessage res = new ResultMessage();
            res = m_BizInterviewMgr.GetAllInterview();
            return Ok(res);
        }

        [HttpGet("Get/{id}")]
        public IActionResult GetInterviewById(int id)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizInterviewMgr.GetInterviewById(id);
            return Ok(res);
        }

        [HttpPost("AddNew")]
        public IActionResult AddNewInterview([FromBody] tbm_interview model)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizInterviewMgr.AddNewInterview(model);
            return Ok(res);
        }

        [HttpPut("Update")]
        public IActionResult UpdateInterview([FromBody] tbm_interview model)
        { 
            ResultMessage res = new ResultMessage();
            res = m_BizInterviewMgr.UpdateInterview(model);
            return Ok(res);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteInterview(int id)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizInterviewMgr.DeleteInterview(id);
            return Ok(res);
        }

        [HttpGet("GetPaginate")]
        public IActionResult GetAllInterview([FromQuery] QueryParameter queryParameter)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizInterviewMgr.GetAllInterview(queryParameter);
            return Ok(res);
        }

        [HttpPost("ImportDataExcleFile")]
        public IActionResult ImportDataExcleFile([FromBody] IFormFile file)
        { 
            ResultMessage resultMessage = m_BizInterviewMgr.ImportDataExcelFile(file);
            return Ok(resultMessage);
        }

        [HttpGet("GetJobName")]
        public IActionResult GetJobName()
        {
            ResultMessage res = m_BizInterviewMgr.GetJobName();
            return Ok(res);
        }
    }
}
