using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BusinessEntities.HR.MasterModels
{
    public class tbm_company_info
    {
        public DateTime ? create_date { get; set; }
        public string ? create_by { get; set; }
        public DateTime ? update_date { get; set; }
        public string ? update_by { get; set; }
        public Int64 ? company_id { get; set; }
        public string ? company_tax_id { get; set; }
        public string ? company_name_th { get; set; }
        public string ? company_name_en { get; set; }
        public string ? company_address { get; set; }
        public string ? company_city { get; set; }
        public string ? company_country { get; set; }
        public string ? company_province { get; set; }
        public string ? company_postal_code { get; set; }
        public string ? company_phone_no { get; set; }
        public string ? company_mobile_no { get; set; }
        public string ? company_fax_no { get; set; }
        public string ? company_key { get; set; }
        public string ? company_logo { get; set; }
        public string ? company_type { get; set; }
        public string ? company_email { get; set; }
        public string ? company_url { get; set; }
        public string ? company_database { get; set; }
        public string ? company_account { get; set; }
        public string ? company_license { get; set; }
        public string ? company_status { get; set; }
    }
}
