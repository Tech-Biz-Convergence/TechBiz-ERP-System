using AspnetCoreMvcFull.Models;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcFull.Logic;
using Newtonsoft.Json;
using AspnetCoreMvcFull.Controllers.TechbizAreas.Attributes;

namespace AspnetCoreMvcFull.Controllers.TechbizAreas.Identity
{
  [ValidateSessionAttrubute]
  public class UserManagementController : BaseController
  {
    public UserManagementController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }

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
