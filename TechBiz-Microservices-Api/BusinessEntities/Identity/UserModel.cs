using Middleware;

namespace BusinessEntities.Identity;



public class tbm_user_info
{
    public DateTime create_date { get; set; }
    public string? create_by { get; set; }
    public DateTime update_date { get; set; }
    public string? update_by { get; set; }
    public int? user_id { get; set; }
    public string? user_code { get; set; }
    public string? user_name { get; set; }
    public string? user_mobile_no { get; set; }
    public string? user_email { get; set; }
    public int? role_id { get; set; }
    public string? permis_id { get; set; }
    public string? user_type { get; set; }
    public string? password { get; set; }
    public string? line_token { get; set; }
    public int? com_id { get; set; }
    public string? salt { get; set; }
    public bool? isAdmin { get; init; }

    public void SetPassword(string password, IEncryptor encryptor)
    {
        salt = encryptor.GetSalt();
        this.password = encryptor.GetHash(password, salt);
    }

    public bool ValidatePassword(string password, IEncryptor encryptor) =>
        this.password == encryptor.GetHash(password, salt);
}

public class tbm_user_info_role:tbm_user_info
{
    public String role { get; set; }
}


public class user_login
{
    public int? user_id { get; set; }
    public string user_name { get; set; }
    public string password { get; set;}
    public string? salt { get; set; }
    public bool? isAdmin { get; set; }

    public void SetPassword(string password, IEncryptor encryptor)
    {
        salt = encryptor.GetSalt();
        this.password = encryptor.GetHash(password, salt);
    }

    public bool ValidatePassword(string password, IEncryptor encryptor) =>
        this.password == encryptor.GetHash(password, salt);
}