using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.HR.MasterModels
{
    #region tmb_user_type 
    public class tbm_user_type
    {
        public DateTime create_date { get; set; }
        public string create_by { get; set; }
        public DateTime? update_date { get; set; }
        public string? update_by { get; set; }
        public long? user_type_id { get; set; }
        public string? user_type_name { get; set; }
        public string? user_type_status { get; set; }
    }
    #endregion
}
