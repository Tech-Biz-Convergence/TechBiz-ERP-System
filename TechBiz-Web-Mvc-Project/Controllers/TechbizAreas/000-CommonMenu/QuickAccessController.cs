using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Logic;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using AspnetCoreMvcFull.Controllers.TechbizAreas.Attributes;


namespace AspnetCoreMvcFull.Controllers.TechbizAreas
{
  [ValidateSessionAttrubute]
  public class QuickAccessController : BaseController
  {
    public QuickAccessController(IHttpContextAccessor httpContextAccessor):base(httpContextAccessor) { }

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
