using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcFull.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Newtonsoft.Json;

namespace AspnetCoreMvcFull.Controllers;

public class AuthController : Controller
{
  private readonly IConfiguration _configuration;
  private readonly ISession _session;

  public AuthController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
  {
    _configuration = configuration;
    _session = httpContextAccessor.HttpContext.Session;
  }
  public IActionResult ForgotPasswordBasic() => View();
  public IActionResult LoginBasic() => View();

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> SubmitLogin(LoginModel model)
  {
    //check validate
    if (!ModelState.IsValid) {
      return View("LoginBasic", model);
    }
    //get menu from api
    var client = new HttpClient();
    var apiUrl = _configuration["ApiUrl"];
    var result = await client.PostAsJsonAsync(apiUrl + "/Api/Identity/Login", model);
    if (result.IsSuccessStatusCode)
    {
      var jsonResponse = await result.Content.ReadAsStringAsync();

      var responseApiModel = JsonConvert.DeserializeObject<ResponseApiModel>(jsonResponse);
      if(responseApiModel != null && responseApiModel.Status)
      {
        var userModel = JsonConvert.DeserializeObject<UserModel>(JsonConvert.SerializeObject(responseApiModel.Data));
        _session.SetString("Token", userModel.token);
        _session.SetString("UserId", userModel.user_id);
        _session.SetString("UserName", userModel.user_name);

        var permissionRoleMapping = JsonConvert.SerializeObject(userModel.menu_role_mapping);
        _session.SetString("MenuRoleMapping", permissionRoleMapping);
        return RedirectToAction("Index", "Dashboards");
      }
    
    }
   
    return View("LoginBasic", model);

  }
  public IActionResult RegisterBasic() => View();
}
