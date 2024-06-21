using BusinessEntities.HR.MasterModels;
using DataLayer.Core;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.HR.MasterModels
{
    public class UserRepository : IDataRepository<tbm_user_info>
    {
        public int Delete(int Key, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public DataTable GetAll(NpgsqlConnection conn)
        {
            throw new NotImplementedException();
        }

        public DataTable GetByKey(int Key, NpgsqlConnection conn)
        {
            throw new NotImplementedException();
        }

        public DataTable GetUser(string user_name, NpgsqlConnection conn)
        {
           
            NpgsqlCommand sqlCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            String select = @"
                            SELECT *
                        ";
            String from = @"   FROM tbm_user_info
                        ";
            String where = @"   WHERE user_name = @user_name
                            LIMIT 1
                        ";


            sqlCommand.Parameters.Add(new NpgsqlParameter("@user_name", SqlDbType.VarChar)).Value = user_name;

            sqlCommand.Connection = conn;


            //get data
            sqlCommand.CommandText = select + from + where;
            NpgsqlDataReader reader = sqlCommand.ExecuteReader();
            dt.Load(reader);
            return dt;

        }

        public int Insert(tbm_user_info model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"INSERT INTO tbm_user_info 											
                                    (											
                                    create_date, 
                                    create_by,
                                    user_id, 
                                    user_code, 
                                    user_name, 
                                    user_mobile_no,
                                    user_Email, 
                                    role_id, 
                                    permis_id,
                                    user_type, 
                                    password, 
                                    line_token, 
                                    com_id								
                                    ) 											
                                VALUES 											
                                    ( @create_date,
                                        @create_by,
                                        @user_id,
                                        @user_code,
                                        @user_name,
                                        @user_mobile_no,
                                        @user_Email,
                                        @role_id,
                                        @permis_id,
                                        @user_type,
                                        @password,
                                        @line_token,
                                        @com_id								
                                    ) RETURNING user_id;";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {

                    cmd.Parameters.Add(new NpgsqlParameter("@create_date", NpgsqlDbType.Date) { Value = model.create_date });
                    cmd.Parameters.Add(new NpgsqlParameter("@create_by", NpgsqlDbType.Varchar) { Value = model.create_by });
                    cmd.Parameters.Add(new NpgsqlParameter("@user_id", NpgsqlDbType.Integer) { Value = model.user_id });
                    cmd.Parameters.Add(new NpgsqlParameter("@user_code", NpgsqlDbType.Varchar) { Value = model.user_code });
                    cmd.Parameters.Add(new NpgsqlParameter("@user_name", NpgsqlDbType.Varchar) { Value = model.user_name });
                    cmd.Parameters.Add(new NpgsqlParameter("@user_mobile_no", NpgsqlDbType.Varchar) { Value = model.user_mobile_no });
                    cmd.Parameters.Add(new NpgsqlParameter("@user_Email", NpgsqlDbType.Varchar) { Value = model.user_Email });
                    cmd.Parameters.Add(new NpgsqlParameter("@role_id", NpgsqlDbType.Integer) { Value = model.role_id });
                    cmd.Parameters.Add(new NpgsqlParameter("@permis_id", NpgsqlDbType.Varchar) { Value = model.permis_id });
                    cmd.Parameters.Add(new NpgsqlParameter("@user_type", NpgsqlDbType.Varchar) { Value = model.user_type });
                    cmd.Parameters.Add(new NpgsqlParameter("@password", NpgsqlDbType.Varchar) { Value = model.password });
                    cmd.Parameters.Add(new NpgsqlParameter("@line_token", NpgsqlDbType.Varchar) { Value = model.line_token });
                    cmd.Parameters.Add(new NpgsqlParameter("@com_id", NpgsqlDbType.Integer) { Value = model.com_id });

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

        public int Update(tbm_user_info Model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            throw new NotImplementedException();
        }
    }
}
