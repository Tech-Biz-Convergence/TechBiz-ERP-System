using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcFull.Models;

namespace AspnetCoreMvcFull.Controllers;

public class AuthController : Controller
{
  public IActionResult ForgotPasswordBasic() => View();
  public IActionResult LoginBasic() => View();

  [HttpPost]
  public IActionResult Login([FromBody] string user_name,string user_password)
  {
    return RedirectToAction("Index","DashBoards");
  
  }
  public IActionResult RegisterBasic() => View();
}
