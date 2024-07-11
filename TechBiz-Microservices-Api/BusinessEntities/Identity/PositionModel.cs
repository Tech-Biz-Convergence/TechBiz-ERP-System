
using Middleware;
using System.Formats.Asn1;
using System.Numerics;

namespace BusinessEntities.Identity;



public class tbm_position
{ 
    public DateTime create_date { get; set; }
    public string create_by { get; set; }
    public DateTime update_date { get; set; }
    public string update_by { get; set; }
    public long? position_id { get; set; }
    public string status { get; set; }
    public long dept_id { get; set; }
    public string position_name { get; set; }

}
