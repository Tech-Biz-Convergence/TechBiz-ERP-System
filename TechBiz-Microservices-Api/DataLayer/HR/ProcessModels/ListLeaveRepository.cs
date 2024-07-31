using BusinessEntities.HR.ProcessModels;
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

namespace DataLayer.HR.ProcessModels
{
    public class ListLeaveRepository : IDataRepository<tbm_leave>
    {
        public int Delete(int Key, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"DELETE FROM hr.tbm_leave WHERE leave_id = @leave_id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@leave_id", NpgsqlDbType.Integer).Value = Key;

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

                String sql = @" SELECT 
                                emp.emp_code, 
                                emp.emp_firstname || ' ' || emp.emp_lastname AS full_name,
                                lev.leave_start_date,
                                lev.leave_end_date,
                                TO_CHAR(EXTRACT(EPOCH FROM (lev.leave_end_date - lev.leave_start_date)) / 86400, 'FM9999999999999999') AS number_leave_date,
                                TO_CHAR(levty.leave_max_days - (EXTRACT(EPOCH FROM (lev.leave_end_date - lev.leave_start_date)) / 86400), 'FM9999999999999999') AS remain_leave_date,
                                lev.leave_reason
                            FROM 
                                hr.tbm_leave lev 
                            INNER JOIN 
                                hr.tbm_employee_info emp ON lev.emp_code = emp.emp_code
                            INNER JOIN 
                                hr.tbm_leave_type levty ON levty.leave_type_id = lev.leave_type_id ";


                sqlCommand.Connection = conn;

                //get data
                sqlCommand.CommandText = sql; 
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

                String sql = @" SELECT 
                                lev.leave_id,
                                emp.emp_code, 
                                emp.emp_firstname || ' ' || emp.emp_lastname AS full_name,
                                lev.leave_start_date,
                                lev.leave_end_date,
                                TO_CHAR(EXTRACT(EPOCH FROM (lev.leave_end_date - lev.leave_start_date)) / 86400, 'FM9999999999999999') AS number_leave_date,
                                TO_CHAR(levty.leave_max_days - (EXTRACT(EPOCH FROM (lev.leave_end_date - lev.leave_start_date)) / 86400), 'FM9999999999999999') AS remain_leave_date,
                                lev.leave_reason
                            FROM 
                                hr.tbm_leave lev 
                            INNER JOIN 
                                hr.tbm_employee_info emp ON lev.emp_code = emp.emp_code
                            INNER JOIN 
                                hr.tbm_leave_type levty ON levty.leave_type_id = lev.leave_type_id
                            WHERE lev.leave_id = @key ";

                sqlCommand.Parameters.Add(new NpgsqlParameter("@key", NpgsqlDbType.Integer)).Value = Key;


                sqlCommand.Connection = conn;

                //get data
                sqlCommand.CommandText = sql;
                NpgsqlDataReader reader = sqlCommand.ExecuteReader();
                dataTable.Load(reader);
                return dataTable;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int Insert(tbm_leave model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"INSERT INTO hr.tbm_leave 											
                                (created_by,
                                emp_code,
                                leave_type_id,
                                leave_start_date,
                                leave_end_date,
                                leave_reason,
                                leave_status) 											
                            VALUES 											
                                (@created_by,
                                @emp_code,
                                @leave_type_id,
                                @leave_start_date,									
                                @leave_end_date,
                                @leave_reason,
                                @leave_status) 
                            RETURNING leave_id;";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@created_by", NpgsqlDbType.Varchar).Value = model.created_by;
                    cmd.Parameters.Add("@emp_code", NpgsqlDbType.Varchar).Value = model.emp_code;
                    cmd.Parameters.Add("@leave_type_id", NpgsqlDbType.Integer).Value = model.leave_type_id;
                    cmd.Parameters.Add("@leave_start_date", NpgsqlDbType.Date).Value = model.leave_start_date;
                    cmd.Parameters.Add("@leave_end_date", NpgsqlDbType.Date).Value = model.leave_end_date;
                    cmd.Parameters.Add("@leave_reason", NpgsqlDbType.Varchar).Value = model.leave_reason;
                    cmd.Parameters.Add("@leave_status", NpgsqlDbType.Varchar).Value = model.leave_status; 

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


        public int Update(tbm_leave model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"UPDATE hr.tbm_leave
                       SET  updated_by = @updated_by,
                            emp_code = @emp_code,
                            leave_type_id = @leave_type_id,
                            leave_start_date = @leave_start_date,
                            leave_end_date = @leave_end_date,
                            leave_reason = @leave_reason,
                            leave_status = @leave_status
                       WHERE leave_id = @leave_id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@updated_by", NpgsqlDbType.Varchar).Value = model.updated_by;
                    cmd.Parameters.Add("@emp_code", NpgsqlDbType.Varchar).Value = model.emp_code;
                    cmd.Parameters.Add("@leave_type_id", NpgsqlDbType.Integer).Value = model.leave_type_id;
                    cmd.Parameters.Add("@leave_start_date", NpgsqlDbType.Date).Value = model.leave_start_date;
                    cmd.Parameters.Add("@leave_end_date", NpgsqlDbType.Date).Value = model.leave_end_date;
                    cmd.Parameters.Add("@leave_reason", NpgsqlDbType.Varchar).Value = model.leave_reason;
                    cmd.Parameters.Add("@leave_status", NpgsqlDbType.Varchar).Value = model.leave_status;
                    cmd.Parameters.Add("@leave_id", NpgsqlDbType.Bigint).Value = model.leave_id;


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

        public DataTable GetAllPagination(QueryParameter queryParameter, out int total, NpgsqlConnection conn)
        {
            try
            {
                NpgsqlCommand sqlCommand = new NpgsqlCommand();
                DataTable dt = new DataTable();

                // Select statement for total count
                string selectCount = @"SELECT count(1) ";
                String fromCount = @"FROM hr.tbm_leave lev 
                            INNER JOIN hr.tbm_employee_info emp ON lev.emp_code = emp.emp_code
                            INNER JOIN hr.tbm_leave_type levty ON levty.leave_type_id = lev.leave_type_id";
                String whereCount = @" WHERE leave_type_name ILIKE '%' || @searchValue || '%'
                              OR CAST(leave_max_days AS TEXT) ILIKE '%' || @searchValue || '%'
                              OR leave_type_comment ILIKE '%' || @searchValue || '%' ";

                // Main select statement
                String select = @"SELECT 
                            lev.leave_id,
                            emp.emp_code, 
                            emp.emp_firstname || ' ' || emp.emp_lastname AS full_name,
                            lev.leave_start_date,
                            lev.leave_end_date,
                            TO_CHAR(EXTRACT(EPOCH FROM (lev.leave_end_date - lev.leave_start_date)) / 86400, 'FM9999999999999999') AS number_leave_date,
                            TO_CHAR(levty.leave_max_days - (EXTRACT(EPOCH FROM (lev.leave_end_date - lev.leave_start_date)) / 86400), 'FM9999999999999999') AS remain_leave_date,
                            lev.leave_reason ";
                String from = @" FROM hr.tbm_leave lev 
                        INNER JOIN hr.tbm_employee_info emp ON lev.emp_code = emp.emp_code
                        INNER JOIN hr.tbm_leave_type levty ON levty.leave_type_id = lev.leave_type_id";
                String where = @" WHERE leave_type_name ILIKE '%' || @searchValue || '%'
                          OR CAST(leave_max_days AS TEXT) ILIKE '%' || @searchValue || '%'
                          OR leave_type_comment ILIKE '%' || @searchValue || '%' ";
                String orderBy = @" ORDER BY " + queryParameter.sortBy + " " + queryParameter.sortType + @"
                          OFFSET (@page - 1) * @limit 
                          FETCH NEXT @limit ROWS ONLY ";

                if (string.IsNullOrWhiteSpace(queryParameter.searchValue))
                {
                    whereCount = "";
                    where = "";
                }
                else
                {
                    sqlCommand.Parameters.Add(new NpgsqlParameter("@searchValue", NpgsqlDbType.Varchar)).Value = queryParameter.searchValue;
                }

                sqlCommand.Parameters.Add(new NpgsqlParameter("@page", NpgsqlDbType.Integer)).Value = queryParameter.page;
                sqlCommand.Parameters.Add(new NpgsqlParameter("@limit", NpgsqlDbType.Integer)).Value = queryParameter.limit;

                sqlCommand.Connection = conn;

                // get total
                sqlCommand.CommandText = selectCount + fromCount + whereCount;
                total = Convert.ToInt32(sqlCommand.ExecuteScalar());

                // get data
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

        public DataTable GetLeaveTypes(NpgsqlConnection conn)
        {
            try
            {
                NpgsqlCommand sqlCommand = new NpgsqlCommand();
                DataTable dataTable = new DataTable();

                String sql = @" SELECT leave_type_id, leave_type_name FROM hr.tbm_leave_type ";


                sqlCommand.Connection = conn;

                //get data
                sqlCommand.CommandText = sql;
                NpgsqlDataReader reader = sqlCommand.ExecuteReader();
                dataTable.Load(reader);
                return dataTable;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public DataTable List<tbm_leave_type> GetLeaveTypes(NpgsqlConnection conn)
        //{
        //    try
        //    {
        //        List<tbm_leave_type> leaveTypes = new List<tbm_leave_type>();
        //        NpgsqlCommand sqlCommand = new NpgsqlCommand("SELECT leave_type_id, leave_type_name FROM hr.tbm_leave_type", conn);

        //        NpgsqlDataReader reader = sqlCommand.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            LeaveType leaveType = new LeaveType
        //            {
        //                LeaveTypeId = reader.GetInt32(0),
        //                LeaveTypeName = reader.GetString(1)
        //            };
        //            leaveTypes.Add(leaveType);
        //        }
        //        reader.Close();
        //        return leaveTypes;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}



        //public DataTable GetAllPagination(QueryParameter queryParameter, out int total,NpgsqlConnection conn)
        //{
        //    try
        //    {
        //        NpgsqlCommand sqlCommand = new NpgsqlCommand();
        //        DataTable dt = new DataTable();

        //        string selectCount = @"SELECT count(1) ";
        //        String select = @" SELECT * ";
        //        String from = @"   FROM  hr.tbm_leave ";
        //        String where = @" WHERE leave_type_name ILIKE '%' || @searchValue || '%'
        //            OR CAST(leave_max_days AS TEXT)ILIKE '%' || @searchValue || '%'
        //            OR leave_type_comment ILIKE '%' || @searchValue || '%' ";
        //        String orderBy = @" ORDER BY " + queryParameter.sortBy + " " + queryParameter.sortType + @"
        //                      OFFSET (@page - 1) * @limit 
        //                      FETCH NEXT @limit ROWS ONLY ";

        //        if(queryParameter.searchValue == null || queryParameter.searchValue.Trim().Length == 0)
        //        {
        //            where = "";
        //        }
        //        else
        //        {
        //            sqlCommand.Parameters.Add(new NpgsqlParameter("@searchValue", NpgsqlDbType.Varchar)).Value = queryParameter.searchValue;
        //        }

        //        sqlCommand.Parameters.Add(new NpgsqlParameter("@page", NpgsqlDbType.Integer)).Value = queryParameter.page;
        //        sqlCommand.Parameters.Add(new NpgsqlParameter("@limit", NpgsqlDbType.Integer)).Value = queryParameter.limit;


        //        sqlCommand.Connection = conn;
        //        //get total
        //        sqlCommand.CommandText = selectCount + from + where;
        //        total = Convert.ToInt32(sqlCommand.ExecuteScalar());
        //        Console.WriteLine(select + from + where + orderBy);

        //        //get data
        //        sqlCommand.CommandText = select + from+ where+orderBy;
        //        NpgsqlDataReader reader = sqlCommand.ExecuteReader();
        //        dt.Load(reader);
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}       

    }
}
