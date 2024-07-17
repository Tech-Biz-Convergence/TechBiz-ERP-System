using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Logic;
using Microsoft.AspNetCore.Http;


namespace AspnetCoreMvcFull.Controllers;

public class DashboardsController : Controller
{

  [HttpGet]
  public async Task<IActionResult> Index(string user_name,string token = "")
  {

    AccessMenu accessMenu = new AccessMenu();
    var menu = await  accessMenu.GetMenuDataFromApi(user_name,token);
    return View(menu);
  }


}
