using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BusinessEntities.HR.MasterModels
{
    public class tbm_recuit_stage
    {
        public DateTime ? create_date { get; set; }
        public string ? create_by { get; set; }
        public DateTime ? update_date { get; set; }
        public string ? update_by { get; set; }
        public Int64 ? recuit_stage_id { get; set; }
        public Int64? candidate_id { get; set; }
        public Int64 ? job_id { get; set; }        
        public Int64 ? pay_amount { get; set; }            
        public string ? recuit_stage_status { get; set; }
    }
}
