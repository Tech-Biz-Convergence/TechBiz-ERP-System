using BusinessEntities.HR.MasterModels;
using DataLayer.Core;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.HR.MasterModels
{
    public class EmployeeRepository : IDataRepository<tm_employee_info>
    {
        public int Delete(int Key, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public DataTable GetAll(NpgsqlConnection conn)
        {
            try
            {
                NpgsqlCommand sqlCommand = new NpgsqlCommand();
                DataTable dataTable = new DataTable();

                String select = @" SELECT * ";
                String from = @" FROM  tm_employee_info  ";


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
                String from   = @" FROM  tm_employee_info  ";
                String where  = @" WHERE  id = @key  ";

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

        public int Insert(tm_employee_info model, NpgsqlConnection conn, NpgsqlTransaction transaction = null) 
        {
            int result = 0;
            try
            {
                string sql = @"INSERT INTO tm_employee_info 											
                                        (											
                                        name,											
                                        position,											
                                        department,											
                                        salary											
                                        ) 											
                                    VALUES 											
                                        (@name,											
                                        @position,											
                                        @department,											
                                        @salary										
                                        ) RETURNING id;";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    
                    cmd.Parameters.Add("@name", NpgsqlDbType.Varchar).Value = model.name;
                    cmd.Parameters.Add("@position", NpgsqlDbType.Varchar).Value = model.position;
                    cmd.Parameters.Add("@department", NpgsqlDbType.Varchar).Value = model.department;
                    cmd.Parameters.Add("@salary", NpgsqlDbType.Double).Value = model.salary;
 
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

        public int Update(tm_employee_info Model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            throw new NotImplementedException();
        }
    }
}
