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
    public class ScheduleRepository : IDataRepository<tbm_hr_schedule>
    {
        public int Delete(int Key, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"DELETE FROM hr.tbm_hr_schedule WHERE schedule_id = @id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@id", NpgsqlDbType.Integer).Value = Key;

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
                String from = @" FROM  hr.tbm_hr_schedule  ";


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
                String from   = @" FROM  hr.tbm_hr_schedule  ";
                String where  = @" WHERE  schedule_id = @key  ";

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

        public int Insert(tbm_hr_schedule model, NpgsqlConnection conn, NpgsqlTransaction transaction = null) 
        {
            int result = 0;
            try
            {
                string sql = @"INSERT INTO hr.tbm_hr_schedule 											
                                        (											
                                        candidate_id,											                                        
                                        user_interview_id,                                        
                                        available_time_slots,
                                        schedule_status,
                                        create_by,
                                        create_date
                                        ) 											
                                    VALUES 											
                                        (@candidate_id,											
                                        @user_interview_id,		
                                        @available_time_slots,                                       
                                        @schedule_status,
                                        @create_by,
                                        @create_date
                                        ) RETURNING schedule_id;";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    
                    cmd.Parameters.Add("@candidate_id", NpgsqlDbType.Bigint).Value = model.candidate_id;
                    cmd.Parameters.Add("@user_interview_id", NpgsqlDbType.Bigint).Value = model.user_interview_id;
                    cmd.Parameters.Add("@available_time_slots", NpgsqlDbType.Varchar).Value = model.available_time_slots;                    
                    cmd.Parameters.Add("@schedule_status", NpgsqlDbType.Varchar).Value = model.schedule_status;
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

        public int Update(tbm_hr_schedule model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"UPDATE hr.tbm_hr_schedule
                       SET candidate_id = @candidate_id,
                           user_interview_id = @user_interview_id, 
                           available_time_slots = @available_time_slots,
                           schedule_status = @schedule_status,
                           update_by = @update_by,
                           update_date = @update_date
                       WHERE schedule_id = @schedule_id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {                    
                    cmd.Parameters.Add("@candidate_id", NpgsqlDbType.Bigint).Value = model.candidate_id;
                    cmd.Parameters.Add("@user_interview_id", NpgsqlDbType.Bigint).Value = model.user_interview_id;
                    cmd.Parameters.Add("@available_time_slots", NpgsqlDbType.Varchar).Value = model.available_time_slots;                        
                    cmd.Parameters.Add("@schedule_status", NpgsqlDbType.Varchar).Value = model.schedule_status;                   
                    cmd.Parameters.Add("@update_by", NpgsqlDbType.Varchar).Value = model.update_by;
                    cmd.Parameters.Add("@update_date", NpgsqlDbType.Timestamp).Value = DateTime.Now;
                    cmd.Parameters.Add("@schedule_id", NpgsqlDbType.Bigint).Value = model.schedule_id;

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
                String select = @" SELECT tbSch.*, tbCan.hr_candidate_name, tbJob.hr_job_title ";
                String from = @" FROM  hr.tbm_hr_schedule tbSch 
                                 LEFT JOIN hr.tbm_hr_candidates tbCan ON tbCan.hr_candidate_id = tbSch.hr_candidate_id 
                                 LEFT JOIN hr.tbm_interview tbInt ON tbInt.interview_id = tbSch.interview_id
                                 LEFT JOIN hr.tbm_hr_job tbJob ON tbJob.hr_job_id = tbInt.job_id ";
                String where = @" WHERE schedule_id ILIKE '%' || @searchValue || '%'";                    
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

        public int UpdateActive(int id, string user_name, string status, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"UPDATE 											
                                        hr.tbm_hr_schedule											
                                    SET 											
                                        schedule_status = @status														
                                    WHERE  											
                                        schedule_id = @id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;
                    cmd.Parameters.Add("@status", NpgsqlDbType.Varchar).Value = status;// model.isActive;											
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

       

    }
}
