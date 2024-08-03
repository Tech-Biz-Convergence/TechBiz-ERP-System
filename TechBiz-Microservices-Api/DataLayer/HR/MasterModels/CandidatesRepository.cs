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
    public class CandidatesRepository : IDataRepository<tbm_hr_candidates>
    {
        public int Delete(int Key, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"DELETE FROM hr.tbm_hr_candidates WHERE hr_candidate_id = @hr_candidate_id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@hr_candidate_id", NpgsqlDbType.Integer).Value = Key;

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
                String from = @" FROM  hr.tbm_hr_candidates  ";


                sqlCommand.Connection = conn;

                //get data
                sqlCommand.CommandText = select + from;
                NpgsqlDataReader reader = sqlCommand.ExecuteReader();
                dataTable.Load(reader);
                return dataTable;
            }catch (Exception ex) 
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
                String from   = @" FROM  hr.tbm_hr_candidates  ";
                String where  = @" WHERE  hr_candidate_id = @key  ";

                sqlCommand.Parameters.Add(new NpgsqlParameter("@key", NpgsqlDbType.Integer)).Value = Key;


                sqlCommand.Connection = conn;

                //get data
                sqlCommand.CommandText = select + from+ where;
                NpgsqlDataReader reader = sqlCommand.ExecuteReader();
                dataTable.Load(reader);
                return dataTable;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int Insert(tbm_hr_candidates model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"INSERT INTO hr.tbm_hr_candidates 											
                                (created_by,
                                hr_candidate_name,
                                hr_job_id,
                                mobile_number,
                                email,
                                hr_candidate_status) 											
                            VALUES 											
                                (@created_by,
                                @hr_candidate_name,
                                @hr_job_id,
                                @mobile_number,									
                                @email,
                                @hr_candidate_status) 
                            RETURNING hr_candidate_id;";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@created_by", NpgsqlDbType.Varchar).Value = model.created_by;
                    cmd.Parameters.Add("@hr_candidate_name", NpgsqlDbType.Varchar).Value = model.hr_candidate_name;
                    cmd.Parameters.Add("@hr_job_id", NpgsqlDbType.Bigint).Value = model.hr_job_id;
                    cmd.Parameters.Add("@mobile_number", NpgsqlDbType.Varchar).Value = model.mobile_number;
                    cmd.Parameters.Add("@email", NpgsqlDbType.Varchar).Value = model.email;
                    cmd.Parameters.Add("@hr_candidate_status", NpgsqlDbType.Varchar).Value = model.hr_candidate_status;

                    if (transaction != null)
                    {
                        cmd.Transaction = transaction;
                    }

                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }


        public int Update(tbm_hr_candidates model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"UPDATE hr.tbm_hr_candidates
                       SET  updated_by = @updated_by,
                            hr_candidate_name = @hr_candidate_name,
                            hr_job_id = @hr_job_id,
                            mobile_number = @mobile_number,
                            email = @email,
                            hr_candidate_status = @hr_candidate_status
                       WHERE hr_candidate_id = @hr_candidate_id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@updated_by", NpgsqlDbType.Varchar).Value = model.updated_by;
                    cmd.Parameters.Add("@hr_candidate_name", NpgsqlDbType.Varchar).Value = model.hr_candidate_name;
                    cmd.Parameters.Add("@hr_job_id", NpgsqlDbType.Bigint).Value = model.hr_job_id;
                    cmd.Parameters.Add("@mobile_number", NpgsqlDbType.Varchar).Value = model.mobile_number;
                    cmd.Parameters.Add("@email", NpgsqlDbType.Varchar).Value = model.email;
                    cmd.Parameters.Add("@hr_candidate_status", NpgsqlDbType.Varchar).Value = model.hr_candidate_status;
                    cmd.Parameters.Add("@hr_candidate_id", NpgsqlDbType.Bigint).Value = model.hr_candidate_id;


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
        public DataTable GetAllPagination(QueryParameter queryParameter, out int total,NpgsqlConnection conn)
        {
            try
            {
                NpgsqlCommand sqlCommand = new NpgsqlCommand();
                DataTable dt = new DataTable();

                string selectCount = @"SELECT count(1) ";
                String select = @" SELECT * ";
                String from = @"   FROM  hr.tbm_hr_candidates ";
                String where = @" WHERE hr_candidate_name ILIKE '%' || @searchValue || '%'
                    OR mobile_number ILIKE '%' || @searchValue || '%'
                    OR CAST(hr_job_id AS TEXT) ILIKE '%' || @searchValue || '%'
                    OR email ILIKE '%' || @searchValue || '%' 
                    OR hr_candidate_status ILIKE '%' || @searchValue || '%' ";

                string orderBy = string.Empty;

                if(queryParameter.sortBy != null)
                {
                    orderBy = @" ORDER BY " + queryParameter.sortBy + " " + queryParameter.sortType + @"
                              OFFSET (@page - 1) * @limit 
                              FETCH NEXT @limit ROWS ONLY ";
                }
                else
                {
                    orderBy = @" ORDER BY hr_candidate_id asc " + queryParameter.sortType + @"
                              OFFSET (@page - 1) * @limit 
                              FETCH NEXT @limit ROWS ONLY ";
                }
                

                if(queryParameter.searchValue == null || queryParameter.searchValue.Trim().Length == 0)
                {
                    where = "";
                }
                else
                {
                    sqlCommand.Parameters.Add(new NpgsqlParameter("@searchValue", NpgsqlDbType.Varchar)).Value = queryParameter.searchValue;
                }

                sqlCommand.Parameters.Add(new NpgsqlParameter("@page", NpgsqlDbType.Integer)).Value = queryParameter.page;
                sqlCommand.Parameters.Add(new NpgsqlParameter("@limit", NpgsqlDbType.Integer)).Value = queryParameter.limit;
               

                sqlCommand.Connection = conn;
                //get total
                sqlCommand.CommandText = selectCount + from + where;
                total = Convert.ToInt32(sqlCommand.ExecuteScalar());
               
                //get data
                sqlCommand.CommandText = select + from + where + orderBy;
                NpgsqlDataReader reader = sqlCommand.ExecuteReader();
                dt.Load(reader);
                return dt;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /*
        public int UpdateActive(int id, int user_id, bool is_active, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"UPDATE 											
                                        tm_employee_info											
                                    SET 											
                                        isActive = @isActive														
                                    WHERE  											
                                        id = @id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;
                    cmd.Parameters.Add("@is_active", NpgsqlDbType.Boolean).Value = is_active;// model.isActive;											
                  //  cmd.Parameters.Add("@update_by", SqlDbType.Int).Value = user_id;

                    result = 0;
                    if (transaction != null)
                    {
                        cmd.Transaction = transaction;
                    }
                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        */

        public DataTable GetCandidatesName(NpgsqlConnection conn)
        {
            try
            {
                NpgsqlCommand sqlCommand = new NpgsqlCommand();
                DataTable dataTable = new DataTable();

                string sql = @" SELECT hr_candidate_id, hr_candidate_title
                         FROM  hr.tbm_hr_candidate WHERE hr_candidate_status = 'ACTIVE'";

                sqlCommand.CommandText = sql;
                sqlCommand.Connection = conn;

                NpgsqlDataReader reader = sqlCommand.ExecuteReader();
                dataTable.Load(reader);
                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving Candidate data", ex);
            }

        }

    }
}
