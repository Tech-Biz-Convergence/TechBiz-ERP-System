using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BusinessEntities.HR.MasterModels
{
    public class tbm_hr_resume
    {
        public DateTime ? create_date { get; set; }
        public string ? create_by { get; set; }
        public DateTime ? update_date { get; set; }
        public string ? update_by { get; set; }
        public Int64 ? hr_resume_id { get; set; }
        public Int64 ? hr_job_id { get; set; }
        public Int64 ? hr_candidate_id{ get; set; }
        public string ? resume_path { get; set; }            
        public string ? resume_status { get; set; }
    }
}
