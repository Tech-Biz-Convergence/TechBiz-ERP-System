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
    public class DepartmentRepository : IDataRepository<tbm_dept_info>
    {
        public int Delete(int Key, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"DELETE FROM hr.tbm_dept_info WHERE dept_id = @dept_id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@dept_id", NpgsqlDbType.Integer).Value = Key;

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
                String from = @" FROM  hr.tbm_dept_info  ";


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

                String select = @"  SELECT dept.dept_id, dept.dept_name,
	                                emp.emp_code  || '-' || emp.emp_firstname || ' ' || emp.emp_lastname AS emp_name,
	                                dept.emp_id, dept.dept_status, dept.create_date, dept.create_by, dept.update_date, dept.update_by ";
                String from = @"    FROM hr.tbm_dept_info dept LEFT join hr.tbm_employee_info emp ON emp.emp_id = dept.emp_id  ";
                String where = @"   WHERE dept.dept_id = @key  ";

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

        public int Insert(tbm_dept_info model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"INSERT INTO hr.tbm_dept_info 											
                                (create_by,
                                dept_name,
                                dept_status) 											
                            VALUES 											
                                (@create_by,
                                @dept_name,
                                @dept_status) 
                            RETURNING dept_id;";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@create_by", NpgsqlDbType.Varchar).Value = model.create_by;
                    cmd.Parameters.Add("@dept_name", NpgsqlDbType.Varchar).Value = model.dept_name;
                    cmd.Parameters.Add("@dept_status", NpgsqlDbType.Varchar).Value = model.dept_status;

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

        public int Update(tbm_dept_info model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"UPDATE hr.tbm_dept_info
                       SET  update_by = @update_by,
                            dept_name = @dept_name,
                            dept_status = @dept_status
                       WHERE dept_id = @dept_id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@update_by", NpgsqlDbType.Varchar).Value = model.update_by;
                    cmd.Parameters.Add("@dept_name", NpgsqlDbType.Varchar).Value = model.dept_name;
                    cmd.Parameters.Add("@dept_status", NpgsqlDbType.Varchar).Value = model.dept_status;
                    cmd.Parameters.Add("@dept_id", NpgsqlDbType.Bigint).Value = model.dept_id;

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
                String from = @" FROM  hr.tbm_dept_info  ";
                String where = @" WHERE  dept_status = 'ACTIVE'  ";

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
                String select = @"  SELECT dept.dept_id, dept.dept_name,
	                                emp.emp_code  || '-' || emp.emp_firstname || ' ' || emp.emp_lastname AS emp_name,
	                                dept.emp_id, dept.dept_status, dept.create_date, dept.create_by, dept.update_date, dept.update_by  ";
                String from = @" FROM hr.tbm_dept_info dept LEFT join hr.tbm_employee_info emp ON emp.emp_id = dept.emp_id ";
                String where = @" WHERE dept.dept_name ILIKE '%' || @searchValue || '%'
                    AND dept_status = 'ACTIVE'  ";
                String orderBy = @" ORDER BY dept_name asc 
                              OFFSET (@page - 1) * @limit 
                              FETCH NEXT @limit ROWS ONLY ";

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
