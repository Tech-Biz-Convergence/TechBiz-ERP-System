using AspnetCoreMvcFull.Models;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcFull.Logic;
using AspnetCoreMvcFull.Controllers.TechbizAreas.Attributes;
using Newtonsoft.Json;

namespace AspnetCoreMvcFull.Controllers.TechbizAreas.Hr
{
  [ValidateSessionAttrubute]
  public class ScheduleManagementController : BaseController
  {
    public ScheduleManagementController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }

    [HttpGet]
    public async Task<IActionResult> Index()
    {

      string menuJson = _session.GetString("MenuRoleMapping");

      if (string.IsNullOrEmpty(menuJson))
      {
        return View(new List<permissionRoleMappingModel>());
      }

      var menu = JsonConvert.DeserializeObject<List<permissionRoleMappingModel>>(menuJson);

      return View(menu);
    }


  }
}
