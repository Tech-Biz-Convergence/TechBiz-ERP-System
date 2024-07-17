using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcFull.Models
{

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

  public class permissionRoleMappingModel : tbm_permission_role_mapping
  {
    public string menu_name { get; set; }
    public string? menu_icon { get; set; }
    public string? program_code { get; set; }
    public string? program_path { get; set; }
    // ฟังก์ชันเพื่อดึง controller จาก program_path
    public string? GetController()
    {
      if (string.IsNullOrEmpty(program_path))
        return null;
      string trimmedPath = program_path.TrimStart('/');
      string[] segments = trimmedPath.Split('/');
      if (segments.Length < 1)
        return null;

      return segments[0];
    }

    // ฟังก์ชันเพื่อดึง action method จาก program_path
    public string? GetAction()
    {
      if (string.IsNullOrEmpty(program_path))
        return null;
      string trimmedPath = program_path.TrimStart('/');
      string[] segments = trimmedPath.Split('/');
      if (segments.Length < 2)
        return null;

      return segments[1];
    }

  }

}

