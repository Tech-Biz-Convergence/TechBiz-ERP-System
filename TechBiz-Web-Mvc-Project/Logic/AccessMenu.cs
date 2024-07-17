using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Service;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Logic
{
  
  public class AccessMenu
  {
    private  ApiService m_ApiService;
    public AccessMenu()
    {
      m_ApiService = new ApiService();
    }

    public async Task<List<permissionRoleMappingModel>> GetMenuDataFromApi(string user_name, string token)
    {
      List<permissionRoleMappingModel> ret = new List<permissionRoleMappingModel>();
      string url = $"Api/Auth/Menu/GetMenuUserRoleMapping?user_name={user_name}";
      m_ApiService.SetToken(token);

      try
      {
        var responseApi = await m_ApiService.GetDataAsync(url);

        if (responseApi != null && responseApi.Status)
        {
          ret = JsonConvert.DeserializeObject<List<permissionRoleMappingModel>>(JsonConvert.SerializeObject(responseApi.Data));
        }
      }
      catch (Exception ex)
      {
        // Handle exceptions here, e.g., log the exception
        Console.WriteLine($"Error fetching menu data: {ex.Message}");
        throw; // Optionally rethrow the exception
      }

      return ret;
    }

    //public static  List<Menu> GetMenuData()
    //{
    //  // จำลองข้อมูลเมนู (ตัวอย่างเท่านี้เท่านั้น)
    //  return new List<Menu>
    //        {
    //            new Menu { Id = 1, Name = "Administration", IconClass = "bx bx-bar-chart-alt-2 me-2", ParentId = null, ProgramPath = null },
    //            new Menu { Id = 2, Name = "User Info", IconClass = "bx bx-bar-chart-alt-2 me-2", ParentId = 1, ProgramPath = "/UserManagement/Index", Controller = "UserManagement", ActionMethod = "Index" },
    //            new Menu { Id = 3, Name = "Access Role", IconClass = "bx bx-bar-chart-alt-2 me-2", ParentId = 1, ProgramPath = "/RoleManagement/Index", Controller = "RoleManagement", ActionMethod = "Index" },
    //            new Menu { Id = 4, Name = "Permission", IconClass = "bx bx-bar-chart-alt-2 me-2", ParentId = 1, ProgramPath = "/PermissionManagement/Index", Controller = "PermissionManagement", ActionMethod = "Index" },
    //            new Menu { Id = 5, Name = "Company Info", IconClass = "bx bx-bar-chart-alt-2 me-2", ParentId = 1, ProgramPath = "/CompanyManagement/Index", Controller = "CompanyManagement", ActionMethod = "Index" },
    //            new Menu { Id = 6, Name = "HR", IconClass = "bx bx-bar-chart-alt-2 me-2", ParentId = null, ProgramPath = null },
    //            new Menu { Id = 7, Name = "Employee", IconClass = "bx bx-bar-chart-alt-2 me-2", ParentId = 6, ProgramPath = "/EmployeeManagement/Index", Controller = "EmployeeManagement", ActionMethod = "Index" },
    //            new Menu { Id = 8, Name = "Department", IconClass = "bx bx-bar-chart-alt-2 me-2", ParentId = 6, ProgramPath = "/DepartmentManagement/Index", Controller = "DepartmentManagement", ActionMethod = "Index" },
    //            new Menu { Id = 9, Name = "Holiday", IconClass = "bx bx-bar-chart-alt-2 me-2", ParentId = 6, ProgramPath = "/HolidayManagement/Index", Controller = "HolidayManagement", ActionMethod = "Index" }

    //        };
    //}
  }
}

