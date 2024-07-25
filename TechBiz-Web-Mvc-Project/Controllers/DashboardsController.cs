using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Logic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using AspnetCoreMvcFull.Controllers.TechbizAreas;
using AspnetCoreMvcFull.Controllers.TechbizAreas.Attributes;


namespace AspnetCoreMvcFull.Controllers;
[ValidateSessionAttrubute]
public class DashboardsController : BaseController
{
  public DashboardsController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }

  [HttpGet]
  public async Task<IActionResult> Index(string user_name,string token = "")
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
