using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BusinessEntities.Identity
{
    public class tbm_role
    {
        public DateTime? create_date { get; set; }
        public string? create_by { get; set; }
        public DateTime? update_date { get; set; }
        public string? update_by { get; set; }
        public long? role_id { get; set; }
        public string? role_name { get; set; }
        public string? role_status { get; set; }
    }
}
