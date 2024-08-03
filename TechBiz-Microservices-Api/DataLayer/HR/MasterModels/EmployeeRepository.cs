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
    public class EmployeeRepository : IDataRepository<tbm_employee_info>
    {
        public int Delete(int Key, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"DELETE FROM hr.tbm_employee_info WHERE emp_id = @id";

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
                String from = @" FROM  hr.tbm_employee_info ";


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

                String select = @" SELECT tbEmp.*,tbPos.position_name ";
                String from   = @" FROM  hr.tbm_employee_info  tbEmp
                                 INNER JOIN hr.tbm_position tbPos 
                                 ON tbEmp.position_id = tbPos.position_id ";
                String where  = @" WHERE  emp_id = @key  ";
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

        public int Insert(tbm_employee_info model, NpgsqlConnection conn, NpgsqlTransaction transaction = null) 
        {
            int result = 0;
            try
            {
                string sql = @"INSERT INTO hr.tbm_employee_info 											
                                        (													
                                        create_by,	
                                        create_date,
                                        emp_code,
                                        emp_firstname,
                                        emp_lastname,
                                        emp_mobile_no,
                                        emp_status,
                                        start_date,
                                        end_date,
                                        position_id
                                        ) 											
                                    VALUES 											
                                        (										
                                        @create_by,	
                                        now(),
                                        @emp_code,
                                        @emp_firstname,
                                        @emp_lastname,
                                        @emp_mobile_no,
                                        @emp_status,
                                        @start_date,
                                        @end_date,
                                        @position_id
                                        ) RETURNING emp_id;";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@create_by", NpgsqlDbType.Varchar).Value = model.create_by;
                    cmd.Parameters.Add("@emp_code", NpgsqlDbType.Varchar).Value = model.emp_code;
                    cmd.Parameters.Add("@emp_firstname", NpgsqlDbType.Varchar).Value = model.emp_firstname;
                    cmd.Parameters.Add("@emp_lastname", NpgsqlDbType.Varchar).Value = model.emp_lastname;
                    cmd.Parameters.Add("@emp_mobile_no", NpgsqlDbType.Varchar).Value = model.emp_mobile_no;
                    cmd.Parameters.Add("@emp_status", NpgsqlDbType.Varchar).Value = model.emp_status;
                    cmd.Parameters.Add("@start_date", NpgsqlDbType.Timestamp).Value = model.start_date;
                    cmd.Parameters.Add("@end_date", NpgsqlDbType.Timestamp).Value = model.end_date ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@position_id", NpgsqlDbType.Bigint).Value = model.position_id;
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

        public int Update(tbm_employee_info model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"UPDATE hr.tbm_employee_info
                       SET 	
                           update_by = @update_by,
                           update_date = now(),
                           emp_code = @emp_code,
                           emp_firstname = @emp_firstname,
                           emp_lastname = @emp_lastname,
                           emp_mobile_no = @emp_mobile_no,
                           emp_status = @emp_status,
                           start_date = @start_date,
                           end_date =  @end_date,
                           position_id = @position_id
                       WHERE emp_id = @emp_id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@update_by", NpgsqlDbType.Varchar).Value = model.update_by;
                    cmd.Parameters.Add("@emp_code", NpgsqlDbType.Varchar).Value = model.emp_code;
                    cmd.Parameters.Add("@emp_firstname", NpgsqlDbType.Varchar).Value = model.emp_firstname;
                    cmd.Parameters.Add("@emp_lastname", NpgsqlDbType.Varchar).Value = model.emp_lastname;
                    cmd.Parameters.Add("@emp_mobile_no", NpgsqlDbType.Varchar).Value = model.emp_mobile_no;
                    cmd.Parameters.Add("@emp_status", NpgsqlDbType.Varchar).Value = model.emp_status;
                    cmd.Parameters.Add("@start_date", NpgsqlDbType.Timestamp).Value = model.start_date;
                    cmd.Parameters.Add("@end_date", NpgsqlDbType.Timestamp).Value = model.end_date;
                    cmd.Parameters.Add("@position_id", NpgsqlDbType.Bigint).Value = model.position_id;
                    cmd.Parameters.Add("@emp_id", NpgsqlDbType.Integer).Value = model.emp_id;
                    

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
                String select = @" SELECT 
                                    tbEmp.*,
                                    tbPos.position_name ";
                String from   = @" FROM 
                                    hr.tbm_employee_info tbEmp
                                INNER JOIN 
                                    hr.tbm_position tbPos ON tbEmp.position_id = tbPos.position_id ";
                String where = @" WHERE emp_code ILIKE '%' || @searchValue || '%'
                    OR emp_firstname ILIKE '%' || @searchValue || '%'
                    OR emp_lastname ILIKE '%' || @searchValue || '%'
                    OR emp_mobile_no ILIKE '%' || @searchValue || '%'
                    OR position_name ILIKE '%' || @searchValue || '%'  ";
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
                                        hr.tbm_employee_info											
                                    SET 											
                                        emp_status = @status														
                                    WHERE  											
                                        emp_id = @id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@id", NpgsqlDbType.Bigint).Value = id;
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
