using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BusinessEntities.HR.MasterModels
{

    # region tbm_dept_info
    public class tbm_dept_info
    {
        [Key]
        public System.DateTime create_date { get; set; }
        public string? create_by { get; set; }
        public System.DateTime update_date { get; set; }
        public string? update_by { get; set; }
        public long dept_id { get; set; }
        public string dept_name { get; set; }
        public long? dept_manager { get; set; }
        public string dept_status { get; set; }

    }
    #endregion

}
