
using Middleware;
using System.Formats.Asn1;
using System.Numerics;

namespace BusinessEntities.Identity;



public class tbm_permission_role_mapping
{ 
    public DateTime? create_date { get; set; }
    public string create_by { get; set; }
    public DateTime? update_date { get; set; }
    public string? update_by { get; set; }
    public long? permission_id { get; set; }
    public long dept_id { get; set; }
    public long role_id { get; set; }
    public long? user_id { get; set; }
    public long menu_id { get; set; }
    public long? menu_parent_id { get; set; }
    public int menu_seq_no { get; set; }
    public string? permiss_add_status { get; set; }
    public string? permiss_edit_status { get; set; }
    public string? permiss_delete_status { get; set; }
    public string? permiss_view_status { get; set; }
    public string? permiss_upload_status { get; set; }
    public string? permiss_download_status { get; set; }

}

public class permissionRoleMappingModel: tbm_permission_role_mapping
{
    public string menu_name { get; set; }
    public string? menu_icon { get; set; }
    public string? program_code { get; set; }
    public string? program_path { get; set; }
}