using BusinessEntities.HR.MasterModels;
using DataLayer.Core;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.IdentityModel.Tokens;
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
    public class InterviewRepository : IDataRepository<tbm_interview>
    {
        public int Delete(int Key, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"DELETE FROM hr.tbm_interview WHERE interview_id = @interview_id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@interview_id", NpgsqlDbType.Integer).Value = Key;

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
                String from = @" FROM  hr.tbm_interview  ";


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
                String from   = @" FROM  hr.tbm_interview  ";
                String where  = @" WHERE  interview_id = @key  ";

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

        public int Insert(tbm_interview model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                // Check if interview_quest is not empty
                if (model.hr_job_id == null)
                {
                    throw new Exception("Holiday name cannot be empty. Please provide a valid name.");
                }
                if (string.IsNullOrEmpty(model.interview_quest))
                {
                    throw new Exception("Holiday name cannot be empty. Please provide a valid name.");
                }

                // Check if job_id and interview_quest duplicate
                string checkSql = @"SELECT COUNT(*) 
                            FROM hr.tbm_interview 
                            WHERE hr_job_id = @hr_job_id
                            AND interview_quest = @interview_quest";

                using (var checkCmd = new NpgsqlCommand(checkSql, conn))
                {
                    checkCmd.Parameters.AddWithValue("@hr_job_id", model.hr_job_id);
                    checkCmd.Parameters.AddWithValue("@interview_quest", model.interview_quest);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        throw new Exception("Duplicate");
                    }
                }

                // Insert tbm_interview
                string sql = @"INSERT INTO hr.tbm_interview 											
                                (created_by,
                                hr_job_id,
                                interview_quest,
                                interview_status) 											
                            VALUES 											
                                (@created_by,
                                @hr_job_id,
                                @interview_quest,
                                @interview_status) 
                            RETURNING interview_id;";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@created_by", NpgsqlDbType.Varchar).Value = model.created_by;
                    cmd.Parameters.Add("@hr_job_id", NpgsqlDbType.Bigint).Value = model.hr_job_id;
                    cmd.Parameters.Add("@interview_quest", NpgsqlDbType.Varchar).Value = model.interview_quest;
                    cmd.Parameters.Add("@interview_status", NpgsqlDbType.Varchar).Value = model.interview_status;

                    if (transaction != null)
                    {
                        cmd.Transaction = transaction;
                    }

                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                // You can log the error here if needed
                throw new Exception($"Error occurred while inserting holiday: {ex.Message}",ex);
            }
            return result;
        }


        public int Update(tbm_interview model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                // Check if interview_quest is not empty
                if (model.hr_job_id == null)
                {
                    throw new Exception("Holiday name cannot be empty. Please provide a valid name.");
                }
                if (string.IsNullOrEmpty(model.interview_quest))
                {
                    throw new Exception("Holiday name cannot be empty. Please provide a valid name.");
                }

                // Check if job_id and interview_quest duplicate
                string checkSql = @"SELECT COUNT(*) 
                            FROM hr.tbm_interview 
                            WHERE hr_job_id = @hr_job_id
                            AND interview_quest = @interview_quest
                            AND interview_id = @interview_id ";

                using (var checkCmd = new NpgsqlCommand(checkSql, conn))
                {
                    checkCmd.Parameters.AddWithValue("@hr_job_id", model.hr_job_id);
                    checkCmd.Parameters.AddWithValue("@interview_quest", model.interview_quest);
                    checkCmd.Parameters.AddWithValue("@interview_id", model.interview_id);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        throw new Exception("Duplicate");
                    }
                }

                // Update tbm_interview
                string sql = @"UPDATE hr.tbm_interview
                       SET  updated_by = @updated_by,
                            hr_job_id = @hr_job_id,
                            interview_quest = @interview_quest,
                            interview_status = @interview_status
                       WHERE interview_id = @interview_id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@updated_by", NpgsqlDbType.Varchar).Value = model.updated_by;
                    cmd.Parameters.Add("@hr_job_id", NpgsqlDbType.Bigint).Value = model.hr_job_id;
                    cmd.Parameters.Add("@interview_quest", NpgsqlDbType.Varchar).Value = model.interview_quest;
                    cmd.Parameters.Add("@interview_status", NpgsqlDbType.Varchar).Value = model.interview_status;
                    cmd.Parameters.Add("@interview_id", NpgsqlDbType.Bigint).Value = model.interview_id;


                    if (transaction != null)
                    {
                        cmd.Transaction = transaction;
                    }

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // You can log the error here if needed
                throw new Exception($"Error occurred while updating department: {ex.Message}");
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
                String select = @" SELECT job.hr_job_title, inv.interview_id, inv.interview_quest, inv.interview_status ";
                String from = @"   FROM  hr.tbm_interview inv LEFT JOIN hr.tbm_hr_job job ON job.hr_job_id = inv.hr_job_id ";
                String where = @" WHERE job.hr_job_title ILIKE '%' || @searchValue || '%'
                    OR inv.interview_quest ILIKE '%' || @searchValue || '%' ";
                String orderBy = @" ORDER BY " + queryParameter.sortBy + " " + queryParameter.sortType + @"
                              OFFSET (@page - 1) * @limit 
                              FETCH NEXT @limit ROWS ONLY ";

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
                Console.WriteLine(select + from + where + orderBy);

                //get data
                sqlCommand.CommandText = select + from+ where+orderBy;
                NpgsqlDataReader reader = sqlCommand.ExecuteReader();
                dt.Load(reader);
                return dt;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable GetJobName(NpgsqlConnection conn)
        {
            try
            {
                NpgsqlCommand sqlCommand = new NpgsqlCommand();
                DataTable dataTable = new DataTable();

                string sql = @" SELECT hr_job_id, hr_job_title
	                            FROM  hr.tbm_hr_job WHERE hr_job_status = 'ACTIVE'";

                sqlCommand.CommandText = sql;
                sqlCommand.Connection = conn;

                NpgsqlDataReader reader = sqlCommand.ExecuteReader();
                dataTable.Load(reader);
                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving Job data", ex);
            }
        }

    }
}
