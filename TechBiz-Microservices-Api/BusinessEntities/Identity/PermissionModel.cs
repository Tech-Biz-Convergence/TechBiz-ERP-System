
using Middleware;
using System.Formats.Asn1;
using System.Numerics;

namespace BusinessEntities.Identity;



public class tbm_permission
{ 
    public DateTime create_date { get; set; }
    public string create_by { get; set; }
    public DateTime update_date { get; set; }
    public string update_by { get; set; }
    public long? permiss_id { get; set; }
    public string permiss_read_status { get; set; }
    public string company_license { get; set; }
    public long dept_id { get; set; }
    public long app_id { get; set; }
    public string permiss_edit_status { get; set; }
    public string permiss_delete_status { get; set; }
    public string permiss_add_status { get; set; }
    public string permiss_upload_status { get; set; }
    public string permiss_download_status { get; set; }

}
