using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BusinessEntities.HR.MasterModels
{

    # region tbm_interview
    public class tbm_interview
    {
        [Key]
        public System.DateTime created_date { get; set; }
        public string? created_by { get; set; }
        public System.DateTime updated_date { get; set; }
        public string? updated_by { get; set; }
        public long interview_id { get; set; }
        public long job_id { get; set; }
        public string interview_quest { get; set; }
        public string interview_status { get; set; }
        public string? hr_job_title { get; set; }

    }
    #endregion
    #region interviewforModel 
    public class interviewforModel : tbm_interview
    {
        public long hr_job_id { get; set; }
        public string? hr_job_title { get; set; }
        public string? hr_job_status { get; set; }

    }

}
#endregion