using AspnetCoreMvcFull.Controllers.TechbizAreas.Attributes;
using AspnetCoreMvcFull.Logic;
using AspnetCoreMvcFull.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AspnetCoreMvcFull.Controllers.TechbizAreas.Hr
{
  [ValidateSessionAttrubute]
  public class HolidayManagementController : BaseController
  {
    public HolidayManagementController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }

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
