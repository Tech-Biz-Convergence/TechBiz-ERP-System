using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BusinessEntities.HR.MasterModels
{
    public class tbm_hr_job
    {
        public DateTime ? create_date { get; set; }
        public string ? create_by { get; set; }
        public DateTime ? update_date { get; set; }
        public string ? update_by { get; set; }
        public Int64 ? hr_job_id { get; set; }
        public string ? hr_job_title { get; set; }
        public Int64 ? dept_id{ get; set; }
        public string ? hr_job_types { get; set; }
        public DateTime ? hr_job_start_date { get; set; }
        public DateTime ? hr_job_expire_date { get; set; }       
        public string ? hr_job_status { get; set; }
    }
}
