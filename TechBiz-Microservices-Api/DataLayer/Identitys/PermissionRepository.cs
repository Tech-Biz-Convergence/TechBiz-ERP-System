using BusinessEntities.Identity;
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

namespace DataLayer.Identitys
{
    public class PermissionRepository : IDataRepository<tbm_permission>
    {
        public int Delete(int Key, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"DELETE FROM authentication.tbm_permission WHERE permiss_id = @id";

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
                String from = @" FROM  authentication.tbm_permission  ";


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
                String from = @" FROM  authentication.tbm_permission  ";
                String where = @" WHERE  permiss_id = @key  ";

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

        public int Insert(tbm_permission model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"INSERT INTO authentication.tbm_permission 											
                                    (											
                                    create_date, 
                                    create_by,
                                    permiss_read_status, 
                                    company_license, 
                                    dept_id,
                                    app_id, 
                                    permiss_edit_status, 
                                    permiss_delete_status, 
                                    permiss_add_status,
                                    permiss_upload_status, 
                                    permiss_download_status
                                    ) 											
                                VALUES 											
                                    ( @create_date, 
                                    @create_by,
                                    @permiss_read_status, 
                                    @company_license, 
                                    @dept_id,
                                    @app_id, 
                                    @permiss_edit_status, 
                                    @permiss_delete_status, 
                                    @permiss_add_status,
                                    @permiss_upload_status, 
                                    @permiss_download_status
                                    ) RETURNING user_id;";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {

                    cmd.Parameters.Add(new NpgsqlParameter("@create_date", NpgsqlDbType.Date) { Value = model.create_date });
                    cmd.Parameters.Add(new NpgsqlParameter("@create_by", NpgsqlDbType.Varchar) { Value = model.create_by });
                    cmd.Parameters.Add(new NpgsqlParameter("@permiss_read_status", NpgsqlDbType.Varchar) { Value = model.permiss_read_status });
                    cmd.Parameters.Add(new NpgsqlParameter("@company_license", NpgsqlDbType.Varchar) { Value = model.company_license });
                    cmd.Parameters.Add(new NpgsqlParameter("@dept_id", NpgsqlDbType.Bigint) { Value = model.dept_id });
                    cmd.Parameters.Add(new NpgsqlParameter("@app_id", NpgsqlDbType.Bigint) { Value = model.app_id });
                    cmd.Parameters.Add(new NpgsqlParameter("@permiss_edit_status", NpgsqlDbType.Varchar) { Value = model.permiss_edit_status });
                    cmd.Parameters.Add(new NpgsqlParameter("@permiss_delete_status", NpgsqlDbType.Varchar) { Value = model.permiss_delete_status });
                    cmd.Parameters.Add(new NpgsqlParameter("@permiss_add_status", NpgsqlDbType.Varchar) { Value = model.permiss_add_status });
                    cmd.Parameters.Add(new NpgsqlParameter("@permiss_upload_status", NpgsqlDbType.Varchar) { Value = model.permiss_upload_status });
                    cmd.Parameters.Add(new NpgsqlParameter("@permiss_download_status", NpgsqlDbType.Bigint) { Value = model.permiss_download_status });
                    

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

        public int Update(tbm_permission model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            throw new NotImplementedException();
        }
       

    }
}
