using Amazon.Runtime.Internal.Util;
using BusinessEntities.HR.MasterModels;
using DataLayer.Core;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace DataLayer.HR.MasterModels
{
    public class CompanyInfoRepository
    {
        public int Delete(int Key, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"DELETE FROM hr.tbm_company_info WHERE company_id = @key";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@key", NpgsqlDbType.Integer).Value = Key;

                    if (transaction != null)
                    {
                        cmd.Transaction = transaction;
                    }

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public DataTable GetAll(NpgsqlConnection conn)
        {
            try
            {
                NpgsqlCommand sqlCommand = new NpgsqlCommand();
                DataTable dataTable = new DataTable();

                String select = @" SELECT * ";
                String from = @" FROM  hr.tbm_company_info  ";


                sqlCommand.Connection = conn;

                //get data
                sqlCommand.CommandText = select + from;
                NpgsqlDataReader reader = sqlCommand.ExecuteReader();
                dataTable.Load(reader);
                return dataTable;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable GetByKey(int Key, NpgsqlConnection conn)
        {
            try
            {
                NpgsqlCommand sqlCommand = new NpgsqlCommand();
                DataTable dataTable = new DataTable();

                String select = @" SELECT * ";
                String from = @" FROM  hr.tbm_company_info  ";
                String where = @" WHERE  company_id = @key  ";

                sqlCommand.Parameters.Add(new NpgsqlParameter("@key", NpgsqlDbType.Integer)).Value = Key;


                sqlCommand.Connection = conn;

                //get data
                sqlCommand.CommandText = select + from + where;
                NpgsqlDataReader reader = sqlCommand.ExecuteReader();
                dataTable.Load(reader);
                return dataTable;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int Insert(tbm_company_info model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"INSERT INTO hr.tbm_company_info 											
                                        (				
                                        company_tax_id,
                                        company_name_th,											
                                        company_name_en,											
                                        company_address,
                                        company_city,
                                        company_country,
                                        company_province,
                                        company_postal_code,
                                        company_phone_no,
                                        company_mobile_no,
                                        company_fax_no,                                        
                                        company_email,
                                        company_url,
                                        create_by,
                                        create_date
                                        ) 											
                                    VALUES 											
                                        (
                                        @company_tax_id,											
                                        @company_name_th,											
                                        @company_name_en,											
                                        @company_address,
                                        @company_city,
                                        @company_country,
                                        @company_province,
                                        @company_postal_code,
                                        @company_phone_no,
                                        @company_mobile_no,
                                        @company_fax_no,                                                                               
                                        @company_email,
                                        @company_url,
                                        @create_by,
                                        @create_date
                                        ) RETURNING company_id;";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {                   
                    cmd.Parameters.Add("@company_tax_id", NpgsqlDbType.Varchar).Value = model.company_tax_id;
                    cmd.Parameters.Add("@company_name_th", NpgsqlDbType.Varchar).Value = model.company_name_th;
                    cmd.Parameters.Add("@company_name_en", NpgsqlDbType.Varchar).Value = model.company_name_en;
                    cmd.Parameters.Add("@company_address", NpgsqlDbType.Varchar).Value = model.company_address;
                    cmd.Parameters.Add("@company_city", NpgsqlDbType.Varchar).Value = model.company_city;
                    cmd.Parameters.Add("@company_country", NpgsqlDbType.Varchar).Value = model.company_country;
                    cmd.Parameters.Add("@company_province", NpgsqlDbType.Varchar).Value = model.company_province;
                    cmd.Parameters.Add("@company_postal_code", NpgsqlDbType.Varchar).Value = model.company_postal_code;
                    cmd.Parameters.Add("@company_phone_no", NpgsqlDbType.Varchar).Value = model.company_phone_no;
                    cmd.Parameters.Add("@company_mobile_no", NpgsqlDbType.Varchar).Value = model.company_mobile_no;
                    cmd.Parameters.Add("@company_fax_no", NpgsqlDbType.Varchar).Value = model.company_fax_no;                    
                    cmd.Parameters.Add("@company_email", NpgsqlDbType.Varchar).Value = model.company_email;
                    cmd.Parameters.Add("@company_url", NpgsqlDbType.Varchar).Value = model.company_url;
                    cmd.Parameters.Add("@create_by", NpgsqlDbType.Varchar).Value = model.create_by;
                    cmd.Parameters.Add("@create_date", NpgsqlDbType.Timestamp).Value = DateTime.Now;
                    
                    if (transaction != null)
                    {
                        cmd.Transaction = transaction;
                    }

                    result = 0;
                    int.TryParse(cmd.ExecuteScalar().ToString(), out result);

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;


        }

        public int Update(tbm_company_info model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"UPDATE hr.tbm_company_info
                       SET 
                            company_tax_id = @company_tax_id,					
                            company_name_th	= @company_name_th,
                            company_name_en = @company_name_en,
                            company_address = @company_address,
                            company_city = @company_city,
                            company_country = @company_country,
                            company_province = @company_province,
                            company_postal_code = @company_postal_code,
                            company_phone_no = @company_phone_no,
                            company_mobile_no = @company_mobile_no,
                            company_fax_no = @company_fax_no,                            
                            company_email = @company_email,
                            company_url = @company_url,    
                            update_by = @update_by,
                            update_date = @update_date
                       WHERE company_id = @company_id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {                    
                    cmd.Parameters.Add("@company_tax_id", NpgsqlDbType.Varchar).Value = model.company_tax_id;
                    cmd.Parameters.Add("@company_name_th", NpgsqlDbType.Varchar).Value = model.company_name_th;
                    cmd.Parameters.Add("@company_name_en", NpgsqlDbType.Varchar).Value = model.company_name_en;
                    cmd.Parameters.Add("@company_address", NpgsqlDbType.Varchar).Value = model.company_address;
                    cmd.Parameters.Add("@company_city", NpgsqlDbType.Varchar).Value = model.company_city;
                    cmd.Parameters.Add("@company_country", NpgsqlDbType.Varchar).Value = model.company_country;
                    cmd.Parameters.Add("@company_province", NpgsqlDbType.Varchar).Value = model.company_province;
                    cmd.Parameters.Add("@company_postal_code", NpgsqlDbType.Varchar).Value = model.company_postal_code;
                    cmd.Parameters.Add("@company_phone_no", NpgsqlDbType.Varchar).Value = model.company_phone_no;
                    cmd.Parameters.Add("@company_mobile_no", NpgsqlDbType.Varchar).Value = model.company_mobile_no;
                    cmd.Parameters.Add("@company_fax_no", NpgsqlDbType.Varchar).Value = model.company_fax_no;                    
                    cmd.Parameters.Add("@company_email", NpgsqlDbType.Varchar).Value = model.company_email;
                    cmd.Parameters.Add("@company_url", NpgsqlDbType.Varchar).Value = model.company_url;
                    cmd.Parameters.Add("@update_by", NpgsqlDbType.Varchar).Value = model.update_by;
                    cmd.Parameters.Add("@company_id", NpgsqlDbType.Bigint).Value = model.company_id;
                    cmd.Parameters.Add("@update_date", NpgsqlDbType.Timestamp).Value = DateTime.Now;

                    if (transaction != null)
                    {
                        cmd.Transaction = transaction;
                    }

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }       
    }
}
