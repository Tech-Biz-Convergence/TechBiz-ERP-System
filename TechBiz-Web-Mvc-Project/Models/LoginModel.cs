using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcFull.Models
{
  public class LoginModel
  {
   
    [Required(ErrorMessage = "The user name is required.")]
    public string user_name { get; set; }
    [Required(ErrorMessage = "The password is required.")]
    public string user_password { get; set; }

      
  }

}
