﻿using BusinessEntities.HR.MasterModels;
using BusinessLogic.HR.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace HolidayService.Controllers
{

    [Route("Api/Hr/[controller]")]
    [Authorize]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        BizHolidayManagement m_BizHolidayMgr;

        public HolidayController()
        {
            m_BizHolidayMgr = new BizHolidayManagement();
        }

        [HttpGet("Get")]
        public IActionResult GetHoliday()
        {
            ResultMessage res = new ResultMessage();
            res = m_BizHolidayMgr.GetAllHoliday();
            return Ok(res);
        }

        [HttpGet("Get/{id}")]
        public IActionResult GetHolidayById(int id)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizHolidayMgr.GetHolidayById(id);
            return Ok(res);
        }

        [HttpPost("AddNew")]
        public IActionResult AddNewHoliday([FromBody] tbm_holiday_info model)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizHolidayMgr.AddNewHoliday(model);
            return Ok(res);
        }

        [HttpPut("Update")]
        public IActionResult UpdateHoliday([FromBody] tbm_holiday_info model)
        { 
            ResultMessage res = new ResultMessage();
            res = m_BizHolidayMgr.UpdateHoliday(model);
            return Ok(res);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteHoliday(int id)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizHolidayMgr.DeleteHoliday(id);
            return Ok(res);
        }

        [HttpGet("GetPaginate")]
        public IActionResult GetAllHoliday([FromQuery] QueryParameter queryParameter)
        {
            ResultMessage res = new ResultMessage();
            res = m_BizHolidayMgr.GetAllHoliday(queryParameter);
            return Ok(res);
        }

        [HttpPost("ImportDataExcleFile")]
        public IActionResult ImportDataExcleFile([FromBody] IFormFile file)
        { 
            ResultMessage resultMessage = m_BizHolidayMgr.ImportDataExcelFile(file);
            return Ok(resultMessage);
        }
    }
}
