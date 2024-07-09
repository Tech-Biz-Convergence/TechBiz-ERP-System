using BusinessEntities.HR.MasterModels;
using BusinessLogic.HR.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace HrService.Controllers;

[Route("Api/Auth/[controller]")]
[Authorize]
[ApiController]
public class CompanyInfoController : ControllerBase
{
    BizCompanyInfoManagement m_BizCompanyInfoMgr;

    public CompanyInfoController()
    {
        m_BizCompanyInfoMgr = new BizCompanyInfoManagement();
    }



    [HttpGet("Get")]
    public IActionResult GetCompanyInfo()
    {
        ResultMessage res = new ResultMessage();
        res = m_BizCompanyInfoMgr.GetAllCompanyInfo();
        return Ok(res);
    }

    [HttpGet("Get/{id}")]
    public IActionResult GetCompanyInfoById(int id)
    {
        ResultMessage res = new ResultMessage();
        //  User.FindFirst("Codemobiles");
        res = m_BizCompanyInfoMgr.GetCompanyInfoById(id);
        return Ok(res);
    }

    [HttpPost("AddNew")]
    public IActionResult AddNewCompanyInfo([FromBody] tbm_company_info model)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizCompanyInfoMgr.AddNewCompanyInfo(model);
        return Ok(res);
    }
    [HttpPut("Update")]
    public IActionResult UpdateCompanyInfo([FromBody] tbm_company_info model)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizCompanyInfoMgr.UpdateCompanyInfo(model);
        return Ok(res);
    }

    [HttpDelete("Delete/{id}")]
    public IActionResult DeleteCompanyInfo(int id)
    {
        ResultMessage res = new ResultMessage();
        res = m_BizCompanyInfoMgr.DeleteCompanyInfo(id);
        return Ok(res);
    }    
}
