using BusinessEntities.HR.MasterModels;
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

namespace DataLayer.HR.MasterModels
{
    public class UserTypeRepository : IDataRepository<tbm_user_type>
    {
        public int Delete(int Key, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"DELETE FROM hr.tbm_user_type WHERE user_type_id = @user_type_id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@user_type_id", NpgsqlDbType.Integer).Value = Key;

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
                String from = @" FROM  hr.tbm_user_type  ";


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

                String select = @"  SELECT * ";
                String from = @"    FROM hr.tbm_user_type  ";
                String where = @"   WHERE user_type_id = @key  ";

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

        public int Insert(tbm_user_type model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"INSERT INTO hr.tbm_user_type 											
                                (create_by,
                                 update_by,
                                user_type_name,
                                user_type_status) 											
                            VALUES 											
                                (@create_by,
                                @update_by,
                                @user_type_name,
                                @user_type_status) 
                            RETURNING user_type_id;";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@create_by", NpgsqlDbType.Varchar).Value = model.create_by;
                    cmd.Parameters.Add("@update_by", NpgsqlDbType.Varchar).Value = model.create_by;
                    cmd.Parameters.Add("@user_type_name", NpgsqlDbType.Varchar).Value = model.user_type_name;
                    cmd.Parameters.Add("@user_type_status", NpgsqlDbType.Varchar).Value = model.user_type_status;

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

        public int Update(tbm_user_type model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"UPDATE hr.tbm_user_type
                       SET  update_by = @update_by,
                            user_type_name = @user_type_name
                            user_type_status = @user_type_status,
                       WHERE user_type_id = @user_type_id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@update_by", NpgsqlDbType.Varchar).Value = model.update_by;
                    cmd.Parameters.Add("@user_type_name", NpgsqlDbType.Varchar).Value = model.user_type_name;
                    cmd.Parameters.Add("@user_type_status", NpgsqlDbType.Varchar).Value = model.user_type_status;
                    cmd.Parameters.Add("@user_type_id", NpgsqlDbType.Bigint).Value = model.user_type_id;

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
        public DataTable GetActive(NpgsqlConnection conn)
        {
            try
            {
                NpgsqlCommand sqlCommand = new NpgsqlCommand();
                DataTable dataTable = new DataTable();

                String select = @" SELECT * ";
                String from = @" FROM  hr.tbm_user_type  ";
                String where = @" WHERE  user_type_status = 'ACTIVE'  ";

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
        public DataTable GetAllPagination(QueryParameter queryParameter, out int total, NpgsqlConnection conn)
        {
            try
            {
                NpgsqlCommand sqlCommand = new NpgsqlCommand();
                DataTable dt = new DataTable();

                string selectCount = @"SELECT count(1) ";
                String select = @"  SELECT *  ";
                String from = @" FROM hr.tbm_user_type  ";
                String where = @" WHERE user_type_name ILIKE '%' || @searchValue || '%'
                    AND user_type_status = 'ACTIVE'  ";
                String orderBy = @" ORDER BY user_type_name asc 
                              OFFSET (@page - 1) * @limit 
                              FETCH NEXT @limit ROWS ONLY ";

                if (queryParameter.sortBy == null || queryParameter.sortType == null)
                {
                    orderBy = @" ORDER BY user_type_name  ASC ";
                }


                if (queryParameter.searchValue == null || queryParameter.searchValue.Trim().Length == 0)
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


    }
}
