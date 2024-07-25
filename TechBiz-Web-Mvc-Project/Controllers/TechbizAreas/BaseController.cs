using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcFull.Controllers.TechbizAreas
{
  public abstract class BaseController : Controller
  {
    protected readonly ISession _session;

    public BaseController(IHttpContextAccessor httpContextAccessor)
    {
      _session = httpContextAccessor.HttpContext.Session;
    }
  }
}
