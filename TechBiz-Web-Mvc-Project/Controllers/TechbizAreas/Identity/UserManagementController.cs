using AspnetCoreMvcFull.Logic;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcFull.Controllers.TechbizAreas.Identity
{
  public class UserManagementController : Controller
  {
    [HttpGet]
    public async Task<IActionResult> Index(string user_name, string token)
    {
      AccessMenu accessMenu = new AccessMenu();
      var menu = await accessMenu.GetMenuDataFromApi(user_name, token);
      return View(menu);
    }
  }
}
