using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Identity
{
    public class tbm_menu
    {
        public DateTime? create_date { get; set; }
        public string create_by { get; set; }
        public DateTime? update_date { get; set; }
        public string? update_by { get; set; }

        public long? menu_id { get; set; }
        public string menu_name { get; set; }
        public string? menu_icon { get; set; }
        public string? program_code { get; set; }
        public string? program_path { get; set; }
        public long? menu_parent_id { get; set; }
        public int menu_seq_no { get; set; }
        public string? status { get; set; }
    }
}
