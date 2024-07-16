using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BusinessEntities.HR.MasterModels
{
    public class tbm_hr_schedule
    {
        public DateTime ? create_date { get; set; }
        public string ? create_by { get; set; }
        public DateTime ? update_date { get; set; }
        public string ? update_by { get; set; }
        public Int64 ? schedule_id { get; set; }
        public Int64? candidate_id { get; set; }
        public Int64 ? user_interview_id { get; set; }        
        public string ? available_time_slots { get; set; }            
        public string ? schedule_status { get; set; }
    }
}
