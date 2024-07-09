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
    public class RoleRepository
    {
        public int Delete(int Key, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"DELETE FROM authentication.tbm_role WHERE role_id = @key";

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
                String from = @" FROM  authentication.tbm_role  ";


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
                String from = @" FROM  authentication.tbm_role  ";
                String where = @" WHERE  role_id = @key  ";

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

        public int Insert(tbm_role model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"INSERT INTO authentication.tbm_role 											
                                        (				
                                        role_name,
                                        role_status,
                                        app_grp_id,
                                        dept_id,
                                        create_by,
                                        create_date
                                        ) 											
                                    VALUES 											
                                        (
                                        @role_name,
                                        @role_status,
                                        @app_grp_id,
                                        @dept_id,
                                        @create_by,
                                        @create_date
                                        ) RETURNING role_id;";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {                    
                    cmd.Parameters.Add("@role_name", NpgsqlDbType.Varchar).Value = model.role_name;
                    cmd.Parameters.Add("@role_status", NpgsqlDbType.Varchar).Value = model.role_status;
                    cmd.Parameters.Add("@app_grp_id", NpgsqlDbType.Varchar).Value = model.app_grp_id;
                    cmd.Parameters.Add("@dept_id", NpgsqlDbType.Varchar).Value = model.dept_id;
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

        public int Update(tbm_role model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"UPDATE authentication.tbm_role
                       SET 
                            role_name = @role_name,
                            role_status = @role_status,
                            app_grp_id = @app_grp_id,
                            dept_id = @dept_id,
                            update_by = @update_by,
                            update_date = @update_date
                       WHERE role_id = @role_id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@role_name", NpgsqlDbType.Varchar).Value = model.role_name;
                    cmd.Parameters.Add("@role_status", NpgsqlDbType.Varchar).Value = model.role_status;
                    cmd.Parameters.Add("@app_grp_id", NpgsqlDbType.Varchar).Value = model.app_grp_id;
                    cmd.Parameters.Add("@dept_id", NpgsqlDbType.Varchar).Value = model.dept_id;
                    cmd.Parameters.Add("@update_by", NpgsqlDbType.Varchar).Value = model.update_by;
                    cmd.Parameters.Add("@role_id", NpgsqlDbType.Bigint).Value = model.role_id;
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
