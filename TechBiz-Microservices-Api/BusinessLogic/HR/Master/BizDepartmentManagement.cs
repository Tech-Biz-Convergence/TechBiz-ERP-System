﻿using DataLayer.HR.MasterModels;
using Utilities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Data;
using BusinessEntities.HR.MasterModels;
using Npgsql.Replication.PgOutput.Messages;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ExcelDataReader;
using NpgsqlTypes;


namespace BusinessLogic.HR.Master
{
    
    public class BizDepartmentManagement
    {
        private DepartmentRepository m_DepartmentRepository;
        public BizDepartmentManagement()
        {
            m_DepartmentRepository = new DepartmentRepository();
        }

        public ResultMessage GetAllDepartment()
        {
            int total = 0;
            ResultMessage resultMessage = new ResultMessage();
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    dt = m_DepartmentRepository.GetAll(conn);
                    var data = dt.DataTableToList<tbm_dept_info>();
                    resultMessage.status = true;
                    resultMessage.code = GlobalMessage.SELECT_SUCCESS_CODE;
                    resultMessage.data = data;
                }
                catch (Exception ex)
                {
                    resultMessage.description = ex.ToString();
                    resultMessage.code = GlobalMessage.SELECT_ERROR_CODE;
                    resultMessage.status = false;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) conn.Close();

                }
            }//end using
            return resultMessage;
        }

        public ResultMessage GetDepartmentById(int id)
        {
            int total = 0;
            int qryRack = 0;

            ResultMessage resultMessage = new ResultMessage();
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    dt = m_DepartmentRepository.GetByKey(id, conn);
                    var data = dt.DataTableToList<tbm_dept_info>().FirstOrDefault();
                    if (data is null)
                    {
                        throw new Exception("Data not found!");

                    }

                    resultMessage.status = true;
                    resultMessage.code = GlobalMessage.SELECT_SUCCESS_CODE;
                    resultMessage.data = data;

                }
                catch (Exception ex)
                {
                    resultMessage.description = ex.ToString();
                    resultMessage.code = GlobalMessage.SELECT_ERROR_CODE;
                    resultMessage.status = false;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) conn.Close();

                }
            }//end using
            return resultMessage;
        }

        public ResultMessage AddNewDepartment(tbm_dept_info model)
        {
            ResultMessage resultMessage = new ResultMessage();

            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();

                    // เช็คว่า dept_name เป็นค่าว่างหรือไม่
                    if (string.IsNullOrWhiteSpace(model.dept_name))
                    {
                        resultMessage.description = "Department Name Null. Please Enter Department Name";
                        resultMessage.code = GlobalMessage.INSERT_ERROR_CODE;
                        resultMessage.status = false;
                        return resultMessage;
                    }

                    // เช็คว่าชื่อแผนกมีอยู่แล้วในระบบหรือไม่
                    string checkSql = @"SELECT COUNT(1) FROM hr.tbm_dept_info WHERE dept_name = @dept_name";
                    using (var checkCmd = new NpgsqlCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.Add("@dept_name", NpgsqlDbType.Varchar).Value = model.dept_name;
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            resultMessage.description = "Department name Duplicate.";
                            resultMessage.code = GlobalMessage.INSERT_ERROR_CODE;
                            resultMessage.status = false;
                            return resultMessage;
                        }
                    }

                    int id = m_DepartmentRepository.Insert(model, conn);
                    model.dept_id = id;

                    resultMessage.data = model;
                    resultMessage.description = "Department added successfully.";
                    resultMessage.code = GlobalMessage.INSERT_SUCCESS_CODE;
                    resultMessage.status = true;
                }
                catch (Exception ex)
                {
                    resultMessage.description = ex.ToString();
                    resultMessage.code = GlobalMessage.UPDATE_ERROR_CODE;
                    resultMessage.status = false;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) conn.Close();

                }
            }//end using


            return resultMessage;
        }

        public ResultMessage UpdateDepartment(tbm_dept_info model)
        {
            ResultMessage resultMessage = new ResultMessage();
            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();

                    // เช็คว่า dept_name เป็นค่าว่างหรือไม่
                    if (string.IsNullOrWhiteSpace(model.dept_name))
                    {
                        resultMessage.description = "Department Name Null. Please Enter Department Name";
                        resultMessage.code = GlobalMessage.UPDATE_ERROR_CODE;
                        resultMessage.status = false;
                        return resultMessage;
                    }

                    // เช็คว่าชื่อแผนกมีอยู่แล้วในระบบหรือไม่ และไม่ใช่ชื่อของแผนกที่กำลังอัปเดต
                    string checkSql = @"SELECT COUNT(1) FROM hr.tbm_dept_info WHERE dept_name = @dept_name AND dept_id != @dept_id";
                    using (var checkCmd = new NpgsqlCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.Add("@dept_name", NpgsqlDbType.Varchar).Value = model.dept_name;
                        checkCmd.Parameters.Add("@dept_id", NpgsqlDbType.Bigint).Value = model.dept_id;
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            resultMessage.description = "Department name Duplicate.";
                            resultMessage.code = GlobalMessage.UPDATE_ERROR_CODE;
                            resultMessage.status = false;
                            return resultMessage;
                        }
                    }

                    int id = m_DepartmentRepository.Update(model, conn);
                    model.dept_id = id;

                    resultMessage.data = model;
                    resultMessage.description = "Department updated successfully.";
                    resultMessage.code = GlobalMessage.UPDATE_SUCCESS_CODE;
                    resultMessage.status = true;
                }
                catch (Exception ex)
                {
                    resultMessage.description = ex.ToString();
                    resultMessage.code = GlobalMessage.UPDATE_ERROR_CODE;
                    resultMessage.status = false;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) conn.Close();

                }
            }//end using


            return resultMessage;
        }

        public ResultMessage DeleteDepartment(int key)
        {
            ResultMessage resultMessage = new ResultMessage();
            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    int id = m_DepartmentRepository.Delete(key, conn);

                    resultMessage.data = id;
                    resultMessage.code = GlobalMessage.UPDATE_SUCCESS_CODE;
                    resultMessage.status = true;
                }
                catch (Exception ex)
                {
                    resultMessage.description = ex.ToString();
                    resultMessage.code = GlobalMessage.UPDATE_ERROR_CODE;
                    resultMessage.status = false;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) conn.Close();

                }
            }//end using


            return resultMessage;
        }

        public ResultMessage GetAllDepartment(QueryParameter queryParameter)
        {
            int total = 0;
            ResultMessage resultMessage = new ResultMessage();
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();

                    dt = m_DepartmentRepository.GetAllPagination(queryParameter, out total, conn);

                    var data = new { total = total, data = dt.DataTableToList<tbm_dept_info>() };
                    resultMessage.status = true;
                    resultMessage.data = data;
                }
                catch (Exception ex)
                {
                    resultMessage.description = ex.ToString();
                    resultMessage.code = GlobalMessage.UPDATE_ERROR_CODE;
                    resultMessage.status = false;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }//using

            return resultMessage;
        }
        public ResultMessage GetDepartmentActive()
        {
            ResultMessage resultMessage = new ResultMessage();
            DataTable dt = new DataTable();
            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    dt = m_DepartmentRepository.GetActive(conn);
                    var data = dt.DataTableToList<tbm_dept_info>();
                    if (data is null)
                    {
                        throw new Exception("Data not found!");

                    }

                    resultMessage.status = true;
                    resultMessage.code = GlobalMessage.SELECT_SUCCESS_CODE;
                    resultMessage.data = data;

                }
                catch (Exception ex)
                {
                    resultMessage.description = ex.ToString();
                    resultMessage.code = GlobalMessage.SELECT_ERROR_CODE;
                    resultMessage.status = false;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) conn.Close();

                }

            }//end using
            return resultMessage;
        }


    }
}
