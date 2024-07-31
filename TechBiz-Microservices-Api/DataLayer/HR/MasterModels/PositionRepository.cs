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
    public class PositionRepository : IDataRepository<tbm_position>
    {
        public int Delete(int Key, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"DELETE FROM hr.tbm_position WHERE position_id = @position_id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@position_id", NpgsqlDbType.Integer).Value = Key;

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
                String from = @" FROM  hr.tbm_position  ";


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
                String from   = @" FROM  hr.tbm_position  ";
                String where  = @" WHERE  position_id = @key  ";

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

        //public int Insert(tbm_position model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        //{
        //    long result = 0;
        //    try
        //    {
        //        string sql = @"INSERT INTO hr.tbm_position 											
        //                (create_by,
        //                status,
        //                dept_id,
        //                position_name,
        //                level) 											
        //            VALUES 											
        //                (@create_by,
        //                @status,
        //                @dept_id,
        //                @position_name,									
        //                @level) 
        //            RETURNING position_id;";

        //        using (var cmd = new NpgsqlCommand(sql, conn))
        //        {
        //            cmd.Parameters.Add("@create_by", NpgsqlDbType.Varchar).Value = model.create_by;
        //            cmd.Parameters.Add("@status", NpgsqlDbType.Varchar).Value = model.status;
        //            cmd.Parameters.Add("@dept_id", NpgsqlDbType.Bigint).Value = model.dept_id;
        //            cmd.Parameters.Add("@position_name", NpgsqlDbType.Varchar).Value = model.position_name;
        //            cmd.Parameters.Add("@level", NpgsqlDbType.Integer).Value = model.level;

        //            if (transaction != null)
        //            {
        //                cmd.Transaction = transaction;
        //            }

        //            result = Convert.ToInt32(cmd.ExecuteScalar());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("An error occurred while inserting the position", ex);
        //    }
        //    return result;
        //}

        public int Insert(tbm_position model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"INSERT INTO hr.tbm_position 											
                                        (create_by,
                                        status,
                                        dept_id,
                                        position_name,
                                        level) 											
                                    VALUES 											
                                        (@create_by,
                                        @status,
                                        @dept_id,
                                        @position_name,									
                                        @level) 
                                    RETURNING position_id;";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@create_by", NpgsqlDbType.Varchar).Value = model.create_by;
                    cmd.Parameters.Add("@status", NpgsqlDbType.Varchar).Value = model.status;
                    cmd.Parameters.Add("@dept_id", NpgsqlDbType.Bigint).Value = model.dept_id;
                    cmd.Parameters.Add("@position_name", NpgsqlDbType.Varchar).Value = model.position_name;
                    cmd.Parameters.Add("@level", NpgsqlDbType.Bigint).Value = model.level;

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

        public int Update(tbm_position model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"UPDATE hr.tbm_position
                               SET  update_by = @update_by,
                                    status = @status,
                                    dept_id = @dept_id,
                                    position_name = @position_name,
                                    level = @level
                               WHERE position_id = @position_id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@update_by", NpgsqlDbType.Varchar).Value = model.update_by;
                    cmd.Parameters.Add("@status", NpgsqlDbType.Varchar).Value = model.status;
                    cmd.Parameters.Add("@dept_id", NpgsqlDbType.Bigint).Value = model.dept_id;
                    cmd.Parameters.Add("@position_name", NpgsqlDbType.Varchar).Value = model.position_name;
                    cmd.Parameters.Add("@level", NpgsqlDbType.Bigint).Value = model.level;
                    cmd.Parameters.Add("@position_id", NpgsqlDbType.Bigint).Value = model.position_id;


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
                String select = @" SELECT posi.position_id, posi.position_name, dept.dept_id, dept.dept_name, posi.level ";
                String from = @"   FROM hr.tbm_position posi LEFT JOIN hr.tbm_dept_info dept ON dept.dept_id = posi.dept_id ";
                String where = @" WHERE posi.position_name ILIKE '%' || @searchValue || '%'
                    OR dept.dept_name ILIKE '%' || @searchValue || '%'
                    OR CAST(level AS TEXT) ILIKE '%' || @searchValue || '%' ";
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
       
    }
}
