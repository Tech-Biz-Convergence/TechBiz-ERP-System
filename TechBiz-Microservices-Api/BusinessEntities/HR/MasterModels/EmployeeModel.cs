using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BusinessEntities.HR.MasterModels
{

 

    #region tbm_employee_info 
    public class tbm_employee_info
    {
        public DateTime? create_date { get; set; }
        public string? create_by { get; set; }
        public DateTime? update_date { get; set; }
        public string? update_by { get; set; }
        public long? emp_id { get; set; }
        public string? emp_code { get; set; }
        public string? emp_firstname { get; set; }
        public string? emp_lastname { get; set; }
        public string? emp_mobile_no { get; set; }
        public string? emp_status { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public long? position_id { get; set; }


    }
    #endregion

    #region employeeInforModel 
    public class employeeInforModel : tbm_employee_info
    {
        public string position_name { get; set; }


    }
    #endregion

}
