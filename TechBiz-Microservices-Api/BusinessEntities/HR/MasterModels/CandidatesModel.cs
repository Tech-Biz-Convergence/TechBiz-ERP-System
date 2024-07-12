using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BusinessEntities.HR.MasterModels
{

    # region tbm_hr_candidates
    public class tbm_hr_candidates
    {
        [Key]
        public System.DateTime created_date { get; set; }
        public string? created_by { get; set; }
        public System.DateTime updated_date { get; set; }
        public string? updated_by { get; set; }
        public long hr_candidate_id { get; set; }
        public string hr_candidate_name { get; set; }
        public long hr_job_id { get; set; }
        public string mobile_number { get; set; }
        public string email { get; set; }
        public string hr_candidate_status { get; set; }

    }
    #endregion

}
