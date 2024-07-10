using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BusinessEntities.HR.MasterModels
{

    # region tbm_leave_type
    public class tbm_leave_type
    {
        [Key]
        public System.DateTime created_date { get; set; }
        public string created_by { get; set; }
        public System.DateTime updated_date { get; set; }
        public string updated_by { get; set; }
        public long? leave_type_id { get; set; }
        public string leave_type_name { get; set; }
        public int? leave_max_days { get; set; }
        public string leave_type_comment { get; set; }
        public string leave_type_status { get; set; }

    }
    #endregion

}
