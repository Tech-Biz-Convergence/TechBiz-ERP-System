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
    public class JobRepository : IDataRepository<tbm_hr_job>
    {
        public int Delete(int Key, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"DELETE FROM hr.tbm_hr_job WHERE hr_job_id = @id";

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
                String from = @" FROM  hr.tbm_hr_job  ";


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

                String select = @" SELECT tbJob.*, tbDept.dept_name ";
                String from   = @" FROM  hr.tbm_hr_job tbJob 
                                   INNER JOIN hr.tbm_dept_info tbDept 
                                   ON tbJob.dept_id = tbDept.dept_id ";
                String where  = @" WHERE  hr_job_id = @key  ";

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

        public int Insert(tbm_hr_job model, NpgsqlConnection conn, NpgsqlTransaction transaction = null) 
        {
            int result = 0;
            try
            {
                string sql = @"INSERT INTO hr.tbm_hr_job 											
                                        (											
                                        hr_job_title,											
                                        dept_id,
                                        hr_job_types,
                                        hr_job_start_date,
                                        hr_job_expire_date,
                                        hr_job_status,
                                        create_by,
                                        create_date
                                        ) 											
                                    VALUES 											
                                        (@hr_job_title,											
                                        @dept_id,		
                                        @hr_job_types,
                                        @hr_job_start_date,
                                        @hr_job_expire_date,
                                        @hr_job_status,
                                        @create_by,
                                        @create_date
                                        ) RETURNING hr_job_id;";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    
                    cmd.Parameters.Add("@hr_job_title", NpgsqlDbType.Varchar).Value = model.hr_job_title;
                    cmd.Parameters.Add("@dept_id", NpgsqlDbType.Bigint).Value = model.dept_id;
                    cmd.Parameters.Add("@hr_job_types", NpgsqlDbType.Varchar).Value = model.hr_job_types;
                    cmd.Parameters.Add("@hr_job_start_date", NpgsqlDbType.Date).Value = model.hr_job_start_date;
                    cmd.Parameters.Add("@hr_job_expire_date", NpgsqlDbType.Date).Value = model.hr_job_expire_date;
                    cmd.Parameters.Add("@hr_job_status", NpgsqlDbType.Varchar).Value = model.hr_job_status;
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

        public int Update(tbm_hr_job model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"UPDATE hr.tbm_hr_job
                       SET hr_job_title = @hr_job_title,
                           dept_id = @dept_id,
                           hr_job_types = @hr_job_types,
                           hr_job_start_date = @hr_job_start_date,
                           hr_job_expire_date = @hr_job_expire_date,
                           hr_job_status = @hr_job_status,
                           update_by = @update_by,
                           update_date = @update_date
                       WHERE hr_job_id = @hr_job_id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@hr_job_title", NpgsqlDbType.Varchar).Value = model.hr_job_title;
                    cmd.Parameters.Add("@dept_id", NpgsqlDbType.Bigint).Value = model.dept_id;
                    cmd.Parameters.Add("@hr_job_types", NpgsqlDbType.Varchar).Value = model.hr_job_types;
                    cmd.Parameters.Add("@hr_job_start_date", NpgsqlDbType.Date).Value = model.hr_job_start_date;
                    cmd.Parameters.Add("@hr_job_expire_date", NpgsqlDbType.Date).Value = model.hr_job_expire_date;
                    cmd.Parameters.Add("@hr_job_status", NpgsqlDbType.Varchar).Value = model.hr_job_status;                    
                    cmd.Parameters.Add("@update_by", NpgsqlDbType.Varchar).Value = model.update_by;
                    cmd.Parameters.Add("@update_date", NpgsqlDbType.Timestamp).Value = DateTime.Now;
                    cmd.Parameters.Add("@hr_job_id", NpgsqlDbType.Bigint).Value = model.hr_job_id;


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
                String select = @" SELECT tbJob.*, tbDept.dept_name ";
                String from = @"   FROM  hr.tbm_hr_job tbJob 
                                   INNER JOIN hr.tbm_dept_info tbDept 
                                   ON tbJob.dept_id = tbDept.dept_id ";
                String where = @" WHERE hr_job_title ILIKE '%' || @searchValue || '%'";

                string orderBy = string.Empty;

                if (queryParameter.sortBy != null)
                {
                    orderBy = @" ORDER BY " + queryParameter.sortBy + " " + queryParameter.sortType + @"
                              OFFSET (@page - 1) * @limit 
                              FETCH NEXT @limit ROWS ONLY ";
                }
                else
                {
                    orderBy = @" ORDER BY hr_job_id asc " + queryParameter.sortType + @"
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

        public int UpdateActive(int id, string user_name, string status, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"UPDATE 											
                                        hr.tbm_hr_job											
                                    SET 											
                                        hr_job_status = @status														
                                    WHERE  											
                                        hr_job_id = @id";

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
