using AspnetCoreMvcFull.Logic;
using AspnetCoreMvcFull.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AspnetCoreMvcFull.Controllers.TechbizAreas.Identity
{
  public class PermissionManagementController : BaseController
  {
    public PermissionManagementController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }

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
