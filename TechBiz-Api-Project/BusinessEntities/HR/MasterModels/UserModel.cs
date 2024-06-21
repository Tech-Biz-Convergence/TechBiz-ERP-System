using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BusinessEntities.HR.MasterModels
{

    # region tbm_user_info
    public class tbm_user_info
    {
        public DateTime? create_date { get; set; }
        public string create_by { get; set; }
        public DateTime? update_date { get; set; }
        public string update_by { get; set; }

        [Key]
        public int user_id { get; set; }

        public string user_code { get; set; }
        public string user_name { get; set; }
        public string user_mobile_no { get; set; }
        public string user_Email { get; set; }
        public int? role_id { get; set; }
        public string permis_id { get; set; }
        public string user_type { get; set; }
        public string password { get; set; }
        public string line_token { get; set; }
        public int? com_id { get; set; }
    }
    #endregion

    public class tbm_user_info_role : tbm_user_info
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string? role {  get; set; }
    }



}
