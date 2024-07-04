using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BusinessEntities.HR.MasterModels
{

    # region tbm_holiday_info
    public class tbm_holiday_info
    {
        [Key]
        public System.DateTime created_date { get; set; }
        public string created_by { get; set; }
        public System.DateTime updated_date { get; set; }
        public string updated_by { get; set; }
        public long? holiday_id { get; set; }
        public string holiday_year { get; set; }
        public string holiday_name { get; set; }
        public System.DateTime holiday_day { get; set; }
        public string holiday_status { get; set; }

    }
    #endregion

}
