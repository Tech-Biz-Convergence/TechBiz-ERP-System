using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BusinessEntities.HR.MasterModels
{
    #region tbm_recuit_stage
    public class tbm_recuit_stage
    {
        public DateTime ? create_date { get; set; }
        public string ? create_by { get; set; }
        public DateTime ? update_date { get; set; }
        public string ? update_by { get; set; }
        public Int64 ? recuit_stage_id { get; set; }
        public Int64? hr_candidate_id { get; set; }
        public Int64 ? hr_job_id { get; set; }        
        public Int64 ? pay_amount { get; set; }            
        public string ? recuit_stage_status { get; set; }
    }
    #endregion

    #region RecuitModel 
    public class RecuitModel : tbm_recuit_stage
    {
        public string hr_job_title { get; set; }
        public string hr_candidate_name { get; set; }
    }
    #endregion    
}
