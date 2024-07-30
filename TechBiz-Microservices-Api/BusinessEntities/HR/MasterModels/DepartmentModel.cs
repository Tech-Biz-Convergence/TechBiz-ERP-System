using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.HR.MasterModels
{
    #region tbm_department_info 
    public class tbm_dept_info
    {
        public DateTime create_date { get; set; }
        public string create_by { get; set; }
        public DateTime? update_date { get; set; }
        public string? update_by { get; set; }
        public long? dept_id { get; set; }
        public string? dept_name { get; set; }
        public string dept_status { get; set; }
        public long emp_id { get; set; }
        public string emp_name { get; set; }

    }
    #endregion
}
