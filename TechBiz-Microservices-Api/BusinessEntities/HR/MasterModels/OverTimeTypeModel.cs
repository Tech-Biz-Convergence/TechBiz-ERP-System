using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BusinessEntities.HR.MasterModels
{
    public class tbm_overtime_type
    {
        public DateTime ? create_date { get; set; }
        public string ? create_by { get; set; }
        public DateTime ? update_date { get; set; }
        public string ? update_by { get; set; }
        public Int64 ? overtime_type_id { get; set; }
        public string ? overtime_type { get; set; }
        public string ? overtime_type_comment { get; set; }                     
        public string ? overtime_type_status { get; set; }
    }
}
