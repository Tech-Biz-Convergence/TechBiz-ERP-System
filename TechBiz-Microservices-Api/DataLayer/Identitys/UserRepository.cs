using BusinessEntities.Identity;
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
using Utilities;

namespace DataLayer.Identitys
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
            NpgsqlCommand sqlCommand = new NpgsqlCommand();
            tbm_user_info ret = new tbm_user_info();
            DataTable dt = new DataTable();
            string select = @"
                            SELECT *
                        ";
            string from = @"   FROM authentication.tbm_user_info
                        ";
            string where = @"   WHERE user_id = @user_id
                            LIMIT 1
                        ";


            sqlCommand.Parameters.Add(new NpgsqlParameter("@user_id", SqlDbType.VarChar)).Value = Key;

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
                string sql = @"INSERT INTO authentication.tbm_user_info 											
                                    (											
                                    create_date, 
                                    create_by,
                                    user_code, 
                                    user_name, 
                                    user_mobile_no,
                                    user_email, 
                                    user_type, 
                                    user_password, 
                                    user_status,
                                    line_token, 
                                    role_id, 
                                    permiss_id,
                                    company_id,
                                    user_lang_def,
                                    salt
                                    ) 											
                                VALUES 											
                                    ( @create_date, 
                                    @create_by,
                                    @user_code, 
                                    @user_name, 
                                    @user_mobile_no,
                                    @user_email, 
                                    @user_type, 
                                    @user_password, 
                                    @user_status,
                                    @line_token, 
                                    @role_id, 
                                    @permiss_id,
                                    @company_id,
                                    @user_lang_def,
                                    @salt
                                    ) RETURNING user_id;";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {

                    cmd.Parameters.Add(new NpgsqlParameter("@create_date", NpgsqlDbType.Date) { Value = model.create_date });
                    cmd.Parameters.Add(new NpgsqlParameter("@create_by", NpgsqlDbType.Varchar) { Value = model.create_by });
                    cmd.Parameters.Add(new NpgsqlParameter("@user_code", NpgsqlDbType.Varchar) { Value = model.user_code });
                    cmd.Parameters.Add(new NpgsqlParameter("@user_name", NpgsqlDbType.Varchar) { Value = model.user_name });
                    cmd.Parameters.Add(new NpgsqlParameter("@user_mobile_no", NpgsqlDbType.Varchar) { Value = model.user_mobile_no });
                    cmd.Parameters.Add(new NpgsqlParameter("@user_email", NpgsqlDbType.Varchar) { Value = model.user_email });
                    cmd.Parameters.Add(new NpgsqlParameter("@user_type", NpgsqlDbType.Varchar) { Value = model.user_type });
                    cmd.Parameters.Add(new NpgsqlParameter("@user_password", NpgsqlDbType.Varchar) { Value = model.user_password });
                    cmd.Parameters.Add(new NpgsqlParameter("@user_status", NpgsqlDbType.Varchar) { Value = model.user_status });
                    cmd.Parameters.Add(new NpgsqlParameter("@line_token", NpgsqlDbType.Varchar) { Value = model.line_token });
                    cmd.Parameters.Add(new NpgsqlParameter("@role_id", NpgsqlDbType.Bigint) { Value = model.role_id });
                    cmd.Parameters.Add(new NpgsqlParameter("@permiss_id", NpgsqlDbType.Bigint) { Value = model.permiss_id });
                    cmd.Parameters.Add(new NpgsqlParameter("@company_id", NpgsqlDbType.Bigint) { Value = model.company_id });
                    cmd.Parameters.Add(new NpgsqlParameter("@user_lang_def", NpgsqlDbType.Varchar) { Value = model.user_lang_def });
                    cmd.Parameters.Add(new NpgsqlParameter("@salt", NpgsqlDbType.Varchar) { Value = model.salt });

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

        //Custom function 
        public tbm_user_info? GetUser(string user_name, NpgsqlConnection conn)
        {

            NpgsqlCommand sqlCommand = new NpgsqlCommand();
            tbm_user_info ret = new tbm_user_info();
            DataTable dt = new DataTable();
            string select = @"
                            SELECT *
                        ";
            string from = @"   FROM authentication.tbm_user_info
                        ";
            string where = @"   WHERE user_name = @user_name
                            LIMIT 1
                        ";


            sqlCommand.Parameters.Add(new NpgsqlParameter("@user_name", SqlDbType.VarChar)).Value = user_name;

            sqlCommand.Connection = conn;


            //get data
            sqlCommand.CommandText = select + from + where;
            NpgsqlDataReader reader = sqlCommand.ExecuteReader();
            dt.Load(reader);
            return  dt.DataTableToList<tbm_user_info>().FirstOrDefault() ;

        }
    }
}
