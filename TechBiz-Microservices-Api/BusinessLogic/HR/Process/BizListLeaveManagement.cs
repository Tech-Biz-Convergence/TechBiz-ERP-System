﻿using DataLayer.HR.ProcessModels;
using Utilities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Data;
using BusinessEntities.HR.ProcessModels;
using Npgsql.Replication.PgOutput.Messages;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ExcelDataReader;
using MongoDB.Driver.Core.Configuration;
using BusinessEntities.HR.MasterModels;



namespace BusinessLogic.HR.Process;

public class BizListLeaveManagement
{
    private ListLeaveRepository m_ListLeaveRepository;

    public BizListLeaveManagement()
    {
        m_ListLeaveRepository = new ListLeaveRepository();
    }

    public ResultMessage GetAllListLeave()
    {
        int total = 0;
        ResultMessage resultMessage = new ResultMessage();
        DataTable dt = new DataTable();

        using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
        {
            try
            {
                conn.Open();
                dt = m_ListLeaveRepository.GetAll(conn);
                var data = dt.DataTableToList<tbm_leave>();
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

    public ResultMessage GetListLeaveById(int id)
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
                dt = m_ListLeaveRepository.GetByKey(id,conn);
                var data = dt.DataTableToList<tbm_leave>().FirstOrDefault();
                if(data is null)
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

    public ResultMessage AddNewListLeave(tbm_leave model)
    {
        ResultMessage resultMessage = new ResultMessage();

        using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
        {
            try
            {
                conn.Open();
                int id = m_ListLeaveRepository.Insert(model, conn);
                model.leave_type_id = id;

                resultMessage.data = model;
                resultMessage.code = GlobalMessage.INSERT_SUCCESS_CODE;
                resultMessage.status = true;
            }
            catch(Exception ex)
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

    public ResultMessage UpdateListLeave(tbm_leave model)
    {
        ResultMessage resultMessage = new ResultMessage();
        using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
        {
            try
            {
                conn.Open();
                int id = m_ListLeaveRepository.Update(model, conn);
                model.leave_type_id = id;

                resultMessage.data = model;
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

    public ResultMessage DeleteListLeave(int key)
    {
        ResultMessage resultMessage = new ResultMessage();
        using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
        {
            try
            {
                conn.Open();
                int id = m_ListLeaveRepository.Delete(key, conn);

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

    public ResultMessage GetAllListLeave(QueryParameter queryParameter)
    {
        int total = 0 ;
        ResultMessage resultMessage = new ResultMessage();
        List<tbm_leave> assyPartControlModel = new List<tbm_leave>();
        DataTable dt = new DataTable();

        using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
        {
            try
            {
                conn.Open();
                    
                dt = m_ListLeaveRepository.GetAllPagination(queryParameter, out total, conn);

                var data = new { total = total, data = dt.DataTableToList<tbm_leave>() };
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

    public ResultMessage GetLeaveTypes()
    {
        ResultMessage resultMessage = new ResultMessage();
        List<tbm_leave> assyPartControlModel = new List<tbm_leave>();
        DataTable dt = new DataTable();
        try
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                conn.Open();
                dt = m_ListLeaveRepository.GetLeaveTypes(conn);
                var data = dt.DataTableToList<tbm_leave_type>();
                resultMessage.status = true;
                resultMessage.data = data;
                resultMessage.code = GlobalMessage.SELECT_SUCCESS_CODE;
            }
        }
        catch (Exception ex)
        {
            resultMessage.status = false;
            resultMessage.description = ex.Message;
            resultMessage.code = GlobalMessage.SELECT_ERROR_CODE;
        }
        return resultMessage;
    }

    //public ResultMessage ActivateCondition(int id,int user_id,bool is_active)
    //{
    //    int total = 0;
    //    ResultMessage resultMessage = new ResultMessage();
    //    List<tm_employee_info> assyPartControlModel = new List<tm_employee_info>();

    //    using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
    //    {
    //        try
    //        {
    //            conn.Open();

    //            int ret = m_EmployeeRepository.UpdateActive(id, user_id, is_active,conn);


    //            resultMessage.status = true;
    //            resultMessage.data = ret;
    //        }
    //        catch (Exception ex)
    //        {
    //            resultMessage.description = ex.ToString();
    //            resultMessage.code = GlobalMessage.UPDATE_ERROR_CODE;
    //            resultMessage.status = false;
    //        }
    //        finally
    //        {
    //            if (conn.State == ConnectionState.Open)
    //                conn.Close();
    //        }
    //    }//using

    //    return resultMessage;
    //}
    public ResultMessage ImportDataExcelFile(IFormFile uploadfile)
    {
        var resultMessage = new ResultMessage();
        if(uploadfile != null && uploadfile.Length >0)
        {
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            Stream stream = uploadfile.OpenReadStream();
            IExcelDataReader reader = null;
            if(uploadfile.FileName.EndsWith(".xls"))
            {
                reader = ExcelReaderFactory.CreateReader(stream);
            }else if (uploadfile.FileName.EndsWith(".xlsx"))
            {
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }
            else
            {

            }
            DataTable dt = new DataTable();
            DataTable dt_ = new DataTable();
            DataRow row;
            List<Dictionary<String,Object>> dataExcelList =new List<Dictionary<String,Object>>();
            try
            {
                dt_ = reader.AsDataSet().Tables[0];
                if (!"Holiday Name".Equals(dt_.Rows[0][0])
                && !"Holiday Day".Equals(dt_.Rows[0][1])
                && !"Holiday Year".Equals(dt_.Rows[0][2])
                )
                {
                    resultMessage.status = false;
                    resultMessage.description = "Template is wrong format!";
                }
                else
                {
                    int countContentData = dt_.Rows.Count;
                    for (int row_ = 1; row_ < countContentData; row_++)
                    {
                        Dictionary<String, Object> dataDic = new Dictionary<String, Object>();
                        dataDic.Add("hoiliday_name", dt_.Rows[row_][0]);
                        dataDic.Add("holiday_day", dt_.Rows[row_][1]);
                        dataDic.Add("holiday_year", dt_.Rows[row_][2]);

                        dataExcelList.Add(dataDic);
                    }//end for
                    var data = new { total = countContentData, data = dt.DataTableToList<tbm_leave>() };
                    resultMessage.status = true;
                    resultMessage.data = data;

                }//end if
                
            }
            catch(Exception ex)
            {
                resultMessage.status = false;
                return resultMessage;
            }
            reader.Close();
            reader.Dispose();

        }


        return resultMessage;
    }


}
