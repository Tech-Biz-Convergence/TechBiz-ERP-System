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
using static MongoDB.Driver.WriteConcern;

namespace DataLayer.Identitys
{
    public class UserRepository : IBigIntDataRepository<tbm_user_info>
    {
        public int Delete(long Key, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public DataTable GetAll(NpgsqlConnection conn)
        {
            throw new NotImplementedException();
        }

        public DataTable GetByKey(long Key, NpgsqlConnection conn)
        {
            NpgsqlCommand sqlCommand = new NpgsqlCommand();
            tbm_user_info ret = new tbm_user_info();
            DataTable dt = new DataTable();
            string select = @" SELECT tbUser.*,
                                    tbRole.role_name role,
                                    tbEmp.emp_firstname,
                                    tbEmp.emp_lastname,
                                    tbUserType.user_type_name  ";
            string from = @"   FROM  authentication.tbm_user_info tbUser
                                    left join authentication.tbm_role tbRole on tbuser.role_id = tbRole.role_id 
                                    left join hr.tbm_user_type tbUserType  on tbuser.user_type_id  = tbUserType.user_type_id 
                                    left join hr.tbm_employee_info tbEmp on tbuser.emp_id  = tbemp.emp_id  ";
            string where = @"   WHERE tbUser.user_id = @user_id
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

       

        public long Insert(tbm_user_info model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public int Update(tbm_user_info model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"UPDATE authentication.tbm_user_info
                   SET
                       update_date = now(),
                       update_by = @update_by,
                       user_mobile_no = @user_mobile_no,
                       user_email = @user_email,
                       user_status = @user_status,
                       line_token = @line_token
                   WHERE
                       user_id = @user_id ";


                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@update_by", NpgsqlDbType.Varchar).Value = model.update_by;
                    cmd.Parameters.Add("@user_mobile_no", NpgsqlDbType.Varchar).Value = model.user_mobile_no;
                    cmd.Parameters.Add("@user_email", NpgsqlDbType.Varchar).Value = model.user_email;
                    cmd.Parameters.Add("@user_status", NpgsqlDbType.Varchar).Value = model.user_status;
                    cmd.Parameters.Add("@line_token", NpgsqlDbType.Varchar).Value = model.line_token;
                    cmd.Parameters.Add("@user_id", NpgsqlDbType.Integer).Value = model.user_id;


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
        public DataTable GetAllPagination(QueryParameter queryParameter, out int total, NpgsqlConnection conn)
        {
            try
            {
                NpgsqlCommand sqlCommand = new NpgsqlCommand();
                DataTable dt = new DataTable();

                string selectCount = @"SELECT count(1) ";
                string select = @" SELECT tbUser.*,
                                    tbRole.role_name role,
                                    tbEmp.emp_firstname,
                                    tbEmp.emp_lastname,
                                    tbUserType.user_type_name  ";
                string from = @"   FROM  authentication.tbm_user_info tbUser
                                    left join authentication.tbm_role tbRole on tbuser.role_id = tbRole.role_id 
                                    left join hr.tbm_user_type tbUserType  on tbuser.user_type_id  = tbUserType.user_type_id 
                                    left join hr.tbm_employee_info tbEmp on tbuser.emp_id  = tbemp.emp_id ";

                String where = @" WHERE tbUser.user_name ILIKE '%' || @searchValue || '%'
                    OR tbUser.user_mobile_no ILIKE '%' || @searchValue || '%'
                    OR tbUser.user_email ILIKE '%' || @searchValue || '%'
                    OR tbUser.line_token ILIKE '%' || @searchValue || '%'
                    OR tbEmp.emp_firstname ILIKE '%' || @searchValue || '%'
                    OR tbEmp.emp_lastname ILIKE '%' || @searchValue || '%'
                    OR tbUserType.user_type_name ILIKE '%' || @searchValue || '%'
                    OR tbRole.role_name ILIKE '%' || @searchValue || '%'";

                string orderBy = @" ORDER BY " + queryParameter.sortBy + " " + queryParameter.sortType + @"
                              OFFSET (@page - 1) * @limit 
                              FETCH NEXT @limit ROWS ONLY ";


                if (queryParameter.sortBy == null || queryParameter.sortType == null)
                {
                    orderBy = @" ORDER BY tbUser.user_name  ASC ";
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

        public int UpdateActive(int id, string user_name, string status, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"UPDATE 											
                                       authentication.tbm_user_info									
                                    SET 											
                                        user_status = @status														
                                    WHERE  											
                                        user_id = @id";

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
