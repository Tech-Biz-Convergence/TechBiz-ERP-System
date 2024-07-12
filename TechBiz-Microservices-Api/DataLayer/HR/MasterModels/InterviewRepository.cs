﻿using BusinessEntities.HR.MasterModels;
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
    public class InterviewRepository : IDataRepository<tbm_interview>
    {
        public int Delete(int Key, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"DELETE FROM hr.tbm_interview WHERE interview_id = @interview_id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@interview_id", NpgsqlDbType.Integer).Value = Key;

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
                String from = @" FROM  hr.tbm_interview  ";


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
                String from   = @" FROM  hr.tbm_interview  ";
                String where  = @" WHERE  interview_id = @key  ";

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

        public int Insert(tbm_interview model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"INSERT INTO hr.tbm_interview 											
                                (created_by,
                                job_id,
                                interview_quest,
                                interview_status) 											
                            VALUES 											
                                (@created_by,
                                @job_id,
                                @interview_quest,
                                @interview_status) 
                            RETURNING interview_id;";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@created_by", NpgsqlDbType.Varchar).Value = model.created_by;
                    cmd.Parameters.Add("@job_id", NpgsqlDbType.Bigint).Value = model.job_id;
                    cmd.Parameters.Add("@interview_quest", NpgsqlDbType.Varchar).Value = model.interview_quest;
                    cmd.Parameters.Add("@interview_status", NpgsqlDbType.Varchar).Value = model.interview_status;

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


        public int Update(tbm_interview model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"UPDATE hr.tbm_interview
                       SET  updated_by = @updated_by,
                            job_id = @job_id,
                            interview_quest = @interview_quest,
                            interview_status = @interview_status
                       WHERE interview_id = @interview_id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@updated_by", NpgsqlDbType.Varchar).Value = model.updated_by;
                    cmd.Parameters.Add("@job_id", NpgsqlDbType.Bigint).Value = model.job_id;
                    cmd.Parameters.Add("@interview_quest", NpgsqlDbType.Varchar).Value = model.interview_quest;
                    cmd.Parameters.Add("@interview_status", NpgsqlDbType.Varchar).Value = model.interview_status;
                    cmd.Parameters.Add("@interview_id", NpgsqlDbType.Bigint).Value = model.interview_id;


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
                String select = @" SELECT * ";
                String from = @"   FROM  hr.tbm_interview ";
                String where = @" WHERE interview_quest ILIKE '%' || @searchValue || '%'
                    OR interview_status ILIKE '%' || @searchValue || '%'
                    OR CAST(job_id AS TEXT) ILIKE '%' || @searchValue || '%' ";
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

        /*
        public int UpdateActive(int id, int user_id, bool is_active, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            int result = 0;
            try
            {
                string sql = @"UPDATE 											
                                        tm_employee_info											
                                    SET 											
                                        isActive = @isActive														
                                    WHERE  											
                                        id = @id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;
                    cmd.Parameters.Add("@is_active", NpgsqlDbType.Boolean).Value = is_active;// model.isActive;											
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
        */

       

    }
}
