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
    public class HolidayRepository : IDataRepository<tbm_holiday_info>
    {
        public int Delete(int Key, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"DELETE FROM hr.tbm_holiday_info WHERE holiday_id = @holiday_id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@holiday_id", NpgsqlDbType.Integer).Value = Key;

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
                String from = @" FROM  hr.tbm_holiday_info  ";


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
                String from   = @" FROM  hr.tbm_holiday_info  ";
                String where  = @" WHERE  holiday_id = @key  ";

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

        public int Insert(tbm_holiday_info model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                // Check if holiday_year, holiday_name, and holiday_day are not empty
                if (string.IsNullOrEmpty(model.holiday_year))
                {
                    throw new Exception("Holiday year Null. Please Enter Holiday year.");
                }
                if (string.IsNullOrEmpty(model.holiday_name))
                {
                    throw new Exception("Holiday name Null. Please Enter Holiday name.");
                }
                if (model.holiday_day == DateTime.MinValue)
                {
                    throw new Exception("Holiday day Null. Please Enter Holiday day.");
                }

                // Check if holiday_year, holiday_name, and holiday_day duplicate
                string checkSql = @"SELECT COUNT(*) 
                            FROM hr.tbm_holiday_info 
                            WHERE holiday_year = @holiday_year
                            AND holiday_name = @holiday_name
                            AND holiday_day = @holiday_day";

                using (var checkCmd = new NpgsqlCommand(checkSql, conn))
                {
                    checkCmd.Parameters.AddWithValue("@holiday_year", model.holiday_year);
                    checkCmd.Parameters.AddWithValue("@holiday_name", model.holiday_name);
                    checkCmd.Parameters.AddWithValue("@holiday_day", model.holiday_day);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        throw new Exception("Data is Duplicate");
                    }
                }

                // Insert tbm_holiday_info
                string sql = @"INSERT INTO hr.tbm_holiday_info 											
                                (created_by,
                                holiday_year,
                                holiday_name,
                                holiday_day,
                                holiday_status) 											
                            VALUES 											
                                (@created_by,
                                @holiday_year,
                                @holiday_name,
                                @holiday_day,									
                                @holiday_status) 
                            RETURNING holiday_id;";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@created_by", NpgsqlDbType.Varchar).Value = model.created_by;
                    cmd.Parameters.Add("@holiday_year", NpgsqlDbType.Varchar).Value = model.holiday_year;
                    cmd.Parameters.Add("@holiday_name", NpgsqlDbType.Varchar).Value = model.holiday_name;
                    cmd.Parameters.Add("@holiday_day", NpgsqlDbType.Date).Value = model.holiday_day;
                    cmd.Parameters.Add("@holiday_status", NpgsqlDbType.Varchar).Value = model.holiday_status;

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
                throw new Exception($"Error occurred while inserting holiday: {ex.Message}");
            }
            return result;
        }


        public int Update(tbm_holiday_info model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                // Check if holiday_year, holiday_name, and holiday_day are not empty
                if (string.IsNullOrEmpty(model.holiday_year))
                {
                    throw new Exception("Holiday year Null. Please Enter Holiday year.");
                }
                if (string.IsNullOrEmpty(model.holiday_name))
                {
                    throw new Exception("Holiday name Null. Please Enter Holiday name.");
                }
                if (model.holiday_day == DateTime.MinValue)
                {
                    throw new Exception("Holiday day Null. Please Enter Holiday day.");
                }

                // Check if holiday_name already exists for a different dept_id
                string checkSql = @"SELECT COUNT(*) 
                            FROM hr.tbm_holiday_info 
                            WHERE holiday_year = @holiday_year
                            AND holiday_name = @holiday_name
                            AND holiday_day = @holiday_day
                            AND holiday_id != @holiday_id";

                using (var checkCmd = new NpgsqlCommand(checkSql, conn))
                {
                    checkCmd.Parameters.AddWithValue("@holiday_year", model.holiday_year);
                    checkCmd.Parameters.AddWithValue("@holiday_day", model.holiday_day);
                    checkCmd.Parameters.AddWithValue("@holiday_name", model.holiday_name);
                    checkCmd.Parameters.AddWithValue("@holiday_id", model.holiday_id);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        throw new Exception("Data is Duplicate");
                    }
                }

                // Update tbm_holiday_info
                string sql = @"UPDATE hr.tbm_holiday_info
                       SET  updated_by = @updated_by,
                            holiday_year = @holiday_year,
                            holiday_name = @holiday_name,
                            holiday_day = @holiday_day,
                            holiday_status = @holiday_status
                       WHERE holiday_id = @holiday_id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@updated_by", NpgsqlDbType.Varchar).Value = model.updated_by;
                    cmd.Parameters.Add("@holiday_year", NpgsqlDbType.Varchar).Value = model.holiday_year;
                    cmd.Parameters.Add("@holiday_name", NpgsqlDbType.Varchar).Value = model.holiday_name;
                    cmd.Parameters.Add("@holiday_day", NpgsqlDbType.Date).Value = model.holiday_day;
                    cmd.Parameters.Add("@holiday_status", NpgsqlDbType.Varchar).Value = model.holiday_status;
                    cmd.Parameters.Add("@holiday_id", NpgsqlDbType.Bigint).Value = model.holiday_id;


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
                throw new Exception($"Error occurred while updating department: {ex.Message}",ex);
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
                String from = @"   FROM  hr.tbm_holiday_info ";
                String where = @" WHERE holiday_name ILIKE '%' || @searchValue || '%'
                    OR holiday_year ILIKE '%' || @searchValue || '%'
                    OR CAST(holiday_day AS TEXT) ILIKE '%' || @searchValue || '%'
                    OR holiday_status ILIKE '%' || @searchValue || '%' ";
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

       

    }
}
