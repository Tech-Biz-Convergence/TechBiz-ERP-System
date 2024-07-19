using AspnetCoreMvcFull.Models;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcFull.Logic;

namespace AspnetCoreMvcFull.Controllers.TechbizAreas.Hr
{
  public class RecuitManagementController : Controller
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
