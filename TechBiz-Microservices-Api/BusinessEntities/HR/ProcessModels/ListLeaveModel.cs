using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BusinessEntities.HR.ProcessModels;


# region tbm_leave
public class tbm_leave
{
    [Key]
    public System.DateTime created_date { get; set; }
    public string? created_by { get; set; }
    public System.DateTime updated_date { get; set; }
    public string? updated_by { get; set; }
    public long leave_id { get; set; }
    public string emp_code { get; set; }
    public long leave_type_id { get; set; } /*ใช้ตัวนี้ในการเชื่อมกับ table leave_type*/
    public System.DateTime? leave_start_date { get; set; }
    public System.DateTime? leave_end_date { get; set; }
    public string leave_reason { get; set; }
    public string leave_status { get; set; }

}
#endregion
