using Middleware;
using System.Formats.Asn1;
using System.Numerics;

namespace BusinessEntities.Identity;



public class tbm_user_info
{

    public DateTime  create_date { get; set; }
    public string create_by { get; set; }
    public DateTime update_date { get; set; }
    public string update_by { get; set; }
    public long? user_id { get; set; }
    public long emp_id { get; set; }
    public string user_name { get; set; }
    public string user_mobile_no { get; set; }
    public string user_email { get; set; }
    public string user_type { get; set; }
    public string user_password { get; set; }
    public string user_status { get; set; }
    public string line_token { get; set; }
    public long role_id { get; set; }
    public long permiss_id { get; set; }
    public long company_id { get; set; }
    public string user_lang_def { get; set; }
    public string salt { get; set; }
    public void SetPassword(string password, IEncryptor encryptor)
    {
        salt = encryptor.GetSalt();
        this.user_password = encryptor.GetHash(password, salt);
    }

    public bool ValidatePassword(string password, IEncryptor encryptor) =>
        this.user_password == encryptor.GetHash(password, salt);
}

public class tbm_user_info_role:tbm_user_info
{
    public String role { get; set; }
}


public class user_login
{
    public string user_name { get; set; }
    public string user_password { get; set;}
}