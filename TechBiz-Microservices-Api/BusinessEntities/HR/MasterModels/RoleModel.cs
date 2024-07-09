using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BusinessEntities.HR.MasterModels
{
    public class tbm_role
    {
        public DateTime? create_date { get; set; }
        public string? create_by { get; set; }
        public DateTime? update_date { get; set; }
        public string? update_by { get; set; }
        public Int64? role_id { get; set; }
        public string? role_name { get; set; }
        public string? role_status { get; set; }
        public Int64? app_grp_id { get; set; }
        public Int64? dept_id { get; set; }
    }
}
