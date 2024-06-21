using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BusinessEntities.HR.MasterModels
{

    # region tm_employee_info
    public class tm_employee_info
    {
        [Key]
        public int? id { get; set; }
        public string name { get; set; }
        public string position { get; set; }
        public string department { get; set; }
        public double salary { get; set; }
        public bool is_active { get; set; }

    }
    #endregion

}
