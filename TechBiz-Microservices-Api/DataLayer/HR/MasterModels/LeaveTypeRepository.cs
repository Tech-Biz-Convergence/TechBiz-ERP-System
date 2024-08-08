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
    public class LeaveTypeRepository : IDataRepository<tbm_leave_type>
    {
        public int Delete(int Key, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"DELETE FROM hr.tbm_leave_type WHERE leave_type_id = @leave_type_id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@leave_type_id", NpgsqlDbType.Integer).Value = Key;

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
                String from = @" FROM  hr.tbm_leave_type  ";


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
                String from   = @" FROM  hr.tbm_leave_type  ";
                String where  = @" WHERE  leave_type_id = @key  ";

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

        public int Insert(tbm_leave_type model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                // Check if interview_quest is not empty
                if (string.IsNullOrEmpty(model.leave_type_name))
                {
                    throw new Exception("Leave Type name cannot be empty. Please provide a valid name.");
                }
                if (model.leave_max_days == null)
                {
                    throw new Exception("Leave Max cannot be empty. Please provide a valid name.");
                }

                // Check if leave_type_name, leave_max_days and interview_quest duplicate
                string checkSql = @"SELECT COUNT(*) 
                            FROM hr.tbm_leave_type 
                            WHERE leave_type_name = @leave_type_name
                            AND leave_max_days = @leave_max_days";

                using (var checkCmd = new NpgsqlCommand(checkSql, conn))
                {
                    checkCmd.Parameters.AddWithValue("@leave_type_name", model.leave_type_name);
                    checkCmd.Parameters.AddWithValue("@leave_max_days", model.leave_max_days); 

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        throw new Exception("Duplicate");
                    }
                }

                // Insert into tbm_leave_type
                string sql = @"INSERT INTO hr.tbm_leave_type 											
                                (created_by,
                                leave_type_name,
                                leave_max_days,
                                leave_type_comment,
                                leave_type_status) 											
                            VALUES 											
                                (@created_by,
                                @leave_type_name,
                                @leave_max_days,
                                @leave_type_comment,									
                                @leave_type_status) 
                            RETURNING leave_type_id;";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@created_by", NpgsqlDbType.Varchar).Value = model.created_by;
                    cmd.Parameters.Add("@leave_type_name", NpgsqlDbType.Varchar).Value = model.leave_type_name;
                    cmd.Parameters.Add("@leave_max_days", NpgsqlDbType.Integer).Value = model.leave_max_days;
                    cmd.Parameters.Add("@leave_type_comment", NpgsqlDbType.Varchar).Value = model.leave_type_comment;
                    cmd.Parameters.Add("@leave_type_status", NpgsqlDbType.Varchar).Value = model.leave_type_status;

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
                throw new Exception($"Error occurred while inserting department: {ex.Message}",ex);
            }
            return result;
        }


        public int Update(tbm_leave_type model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                // Check if leave_type_name, leave_max_days, leave_type_comment is not empty
                if (string.IsNullOrEmpty(model.leave_type_name))
                {
                    throw new Exception("Leave Type name cannot be empty. Please provide a valid name.");
                }
                if (model.leave_max_days == null)
                {
                    throw new Exception("Leave Max cannot be empty. Please provide a valid name.");
                }

                // Check if leave_type_name, leave_max_days and leave_type_id duplicate for a different dept_id
                string checkSql = @"SELECT COUNT(*) 
                            FROM hr.tbm_leave_type 
                            WHERE leave_type_name = @leave_type_name
                            AND leave_max_days = @leave_max_days
                            AND leave_type_id != @leave_type_id";

                using (var checkCmd = new NpgsqlCommand(checkSql, conn))
                {
                    checkCmd.Parameters.AddWithValue("@leave_type_name", model.leave_type_name);
                    checkCmd.Parameters.AddWithValue("@leave_max_days", model.leave_max_days);
                    checkCmd.Parameters.AddWithValue("@leave_type_id", model.leave_type_id);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        throw new Exception("Duplicate");
                    }
                }

                // Update tbm_leave_type
                string sql = @"UPDATE hr.tbm_leave_type
                       SET  updated_by = @updated_by,
                            leave_type_name = @leave_type_name,
                            leave_max_days = @leave_max_days,
                            leave_type_comment = @leave_type_comment
                       WHERE leave_type_id = @leave_type_id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@updated_by", NpgsqlDbType.Varchar).Value = model.updated_by;
                    cmd.Parameters.Add("@leave_type_name", NpgsqlDbType.Varchar).Value = model.leave_type_name;
                    cmd.Parameters.Add("@leave_max_days", NpgsqlDbType.Integer).Value = model.leave_max_days;
                    cmd.Parameters.Add("@leave_type_comment", NpgsqlDbType.Varchar).Value = model.leave_type_comment;
                    cmd.Parameters.Add("@leave_type_id", NpgsqlDbType.Bigint).Value = model.leave_type_id;


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
                String select = @" SELECT * ";
                String from = @"   FROM  hr.tbm_leave_type ";
                String where = @" WHERE leave_type_name ILIKE '%' || @searchValue || '%'
                    OR CAST(leave_max_days AS TEXT)ILIKE '%' || @searchValue || '%'
                    OR leave_type_comment ILIKE '%' || @searchValue || '%' ";
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

        //public DataTable GetLeaveType(NpgsqlConnection conn)
        //{
        //    try
        //    {
        //        NpgsqlCommand sqlCommand = new NpgsqlCommand();
        //        DataTable dataTable = new DataTable();

        //        string sql = @" SELECT leave_type_id, leave_type_name
        //                    	FROM hr.tbm_leave_type WHERE leave_type_status = 'ACTIVE'";

        //        sqlCommand.CommandText = sql;
        //        sqlCommand.Connection = conn;

        //        NpgsqlDataReader reader = sqlCommand.ExecuteReader();
        //        dataTable.Load(reader);
        //        return dataTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error retrieving Leave Type data", ex);
        //    }
        //}
    }
}
