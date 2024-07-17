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
    public class PermissionRoleMappingRepository : IBigIntDataRepository<tbm_permission_role_mapping>
    {
        public int Delete(Int64 Key, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"DELETE FROM authentication.tbm_permission_role_mapping WHERE permission_id = @id";

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
                String from = @" FROM  authentication.tbm_permission_role_mapping  ";


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

        public DataTable GetByKey(Int64 Key, NpgsqlConnection conn)
        {
            try
            {
                NpgsqlCommand sqlCommand = new NpgsqlCommand();
                DataTable dataTable = new DataTable();

                String select = @"SELECT tbPerRoleMapping.*,tbMenu.program_code,tbMenu.menu_name   ";
                String from = @" FROM  authentication.tbm_permission_role_mapping tbPerRoleMapping
                                INNER JOIN authentication.tbm_menu tbMenu
                                ON tbPerRoleMapping.menu_id = tbMenu.menu_id ";
                String where = @" WHERE tbperrolemapping.permission_id  = @key  ";

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

        public Int64 Insert(tbm_permission_role_mapping model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"INSERT INTO authentication.tbm_permission_role_mapping 											
                                    (											
                                    create_date, 
                                    create_by,
                                    dept_id,
                                    role_id, 
                                    user_id,
                                    menu_id,
                                    menu_parent_id,
                                    menu_seq_no,
                                    permiss_add_status,
                                    permiss_edit_status, 
                                    permiss_delete_status, 
                                    permiss_view_status,
                                    permiss_upload_status, 
                                    permiss_download_status
                                    ) 											
                                VALUES 											
                                    ( @create_date, 
                                    @create_by,
                                    @dept_id,
                                    @role_id, 
                                    @user_id,
                                    @menu_id,
                                    @menu_parent_id,
                                    @menu_seq_no,
                                    @permiss_add_status,
                                    @permiss_edit_status, 
                                    @permiss_delete_status, 
                                    @permiss_view_status,
                                    @permiss_upload_status, 
                                    @permiss_download_status
                                    ) RETURNING user_id;";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {

                    cmd.Parameters.Add(new NpgsqlParameter("@create_date", NpgsqlDbType.Date) { Value = model.create_date });
                    cmd.Parameters.Add(new NpgsqlParameter("@create_by", NpgsqlDbType.Varchar) { Value = model.create_by });
                    cmd.Parameters.Add(new NpgsqlParameter("@dept_id", NpgsqlDbType.Bigint) { Value = model.dept_id });
                    cmd.Parameters.Add(new NpgsqlParameter("@role_id", NpgsqlDbType.Bigint) { Value = model.role_id });
                    cmd.Parameters.Add(new NpgsqlParameter("@user_id", NpgsqlDbType.Bigint) { Value = model.user_id });
                    cmd.Parameters.Add(new NpgsqlParameter("@menu_id", NpgsqlDbType.Bigint) { Value = model.menu_id });
                    cmd.Parameters.Add(new NpgsqlParameter("@menu_parent_id", NpgsqlDbType.Bigint) { Value = model.menu_parent_id });
                    cmd.Parameters.Add(new NpgsqlParameter("@menu_seq_no", NpgsqlDbType.Integer) { Value = model.menu_seq_no });
                    cmd.Parameters.Add(new NpgsqlParameter("@permiss_add_status", NpgsqlDbType.Varchar) { Value = model.permiss_add_status });
                    cmd.Parameters.Add(new NpgsqlParameter("@permiss_edit_status", NpgsqlDbType.Varchar) { Value = model.permiss_edit_status });
                    cmd.Parameters.Add(new NpgsqlParameter("@permiss_delete_status", NpgsqlDbType.Varchar) { Value = model.permiss_delete_status });
                    cmd.Parameters.Add(new NpgsqlParameter("@permiss_view_status", NpgsqlDbType.Varchar) { Value = model.permiss_view_status });
                    cmd.Parameters.Add(new NpgsqlParameter("@permiss_upload_status", NpgsqlDbType.Varchar) { Value = model.permiss_upload_status });
                    cmd.Parameters.Add(new NpgsqlParameter("@permiss_download_status", NpgsqlDbType.Varchar) { Value = model.permiss_download_status });
                    

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

        public int Update(tbm_permission_role_mapping model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            throw new NotImplementedException();
        }
       

    }
}
