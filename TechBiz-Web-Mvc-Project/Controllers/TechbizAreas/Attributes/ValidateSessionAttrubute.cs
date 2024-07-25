using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspnetCoreMvcFull.Controllers.TechbizAreas.Attributes
{
  public class ValidateSessionAttrubute: ActionFilterAttribute
  {
    public override void OnActionExecuting(ActionExecutingContext context)
    {
      var session = context.HttpContext.Session;
      var token = session.GetString("Token");

      if (string.IsNullOrEmpty(token))
      {
        context.Result = new RedirectToActionResult("LoginBasic", "Auth", null);
      }

      base.OnActionExecuting(context);
    }
  }
}
