using DataLayer.HR.MasterModels;
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
    public class BizLeaveTypeManagement
    {
        private LeaveTypeRepository m_LeaveRepository;

        public BizLeaveTypeManagement()
        {
            m_LeaveRepository = new LeaveTypeRepository();
        }

        public ResultMessage GetAllLeave()
        {
            int total = 0;
            ResultMessage resultMessage = new ResultMessage();
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    dt = m_LeaveRepository.GetAll(conn);
                    var data = dt.DataTableToList<tbm_leave_type>();
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

        public ResultMessage GetLeaveById(int id)
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
                    dt = m_LeaveRepository.GetByKey(id,conn);
                    var data = dt.DataTableToList<tbm_leave_type>().FirstOrDefault();
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

        public ResultMessage AddNewLeave(tbm_leave_type model)
        {
            ResultMessage resultMessage = new ResultMessage();

            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();

                    if (string.IsNullOrEmpty(model.leave_type_name))
                    {
                        resultMessage.description = "Leave Type Name Null. Please Enter Data.";
                        resultMessage.code = GlobalMessage.INSERT_ERROR_CODE;
                        resultMessage.status = false;
                        return resultMessage;
                    }
                    if (model.leave_max_days == null)
                    {
                        resultMessage.description = "Leave Max Null. Please Enter Data.";
                        resultMessage.code = GlobalMessage.INSERT_ERROR_CODE;
                        resultMessage.status = false;
                        return resultMessage;
                    }

                    string checkSql = @"SELECT COUNT(1) FROM hr.tbm_leave_type 
                            WHERE leave_type_name = @leave_type_name
                            AND leave_max_days = @leave_max_days";
                    using (var checkCmd = new NpgsqlCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.Add("@leave_type_name", NpgsqlDbType.Varchar).Value = model.leave_type_name;
                        checkCmd.Parameters.Add("@leave_max_days", NpgsqlDbType.Integer).Value = model.leave_max_days;

                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            resultMessage.description = "Data is Duplicate.";
                            resultMessage.code = GlobalMessage.INSERT_ERROR_CODE;
                            resultMessage.status = false;
                            return resultMessage;
                        }
                    }

                    int id = m_LeaveRepository.Insert(model, conn);
                    model.leave_type_id = id;

                    resultMessage.data = model;
                    resultMessage.description = "Leave Type added successfully.";
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

        public ResultMessage UpdateLeave(tbm_leave_type model)
        {
            ResultMessage resultMessage = new ResultMessage();
            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();

                    if (string.IsNullOrEmpty(model.leave_type_name))
                    {
                        resultMessage.description = "Leave Type Name Null. Please Enter Data.";
                        resultMessage.code = GlobalMessage.INSERT_ERROR_CODE;
                        resultMessage.status = false;
                        return resultMessage;
                    }
                    if (model.leave_max_days == null)
                    {
                        resultMessage.description = "Leave Max Null. Please Enter Data.";
                        resultMessage.code = GlobalMessage.INSERT_ERROR_CODE;
                        resultMessage.status = false;
                        return resultMessage;
                    }

                    string checkSql = @"SELECT COUNT(1) FROM hr.tbm_leave_type 
                            WHERE leave_type_name = @leave_type_name
                            AND leave_max_days = @leave_max_days
                            AND leave_type_id != @leave_type_id ";
                    using (var checkCmd = new NpgsqlCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.Add("@leave_type_name", NpgsqlDbType.Varchar).Value = model.leave_type_name;
                        checkCmd.Parameters.Add("@leave_max_days", NpgsqlDbType.Integer).Value = model.leave_max_days;
                        checkCmd.Parameters.Add("@leave_type_id", NpgsqlDbType.Bigint).Value = model.leave_type_id;

                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            resultMessage.description = "Data is Duplicate.";
                            resultMessage.code = GlobalMessage.INSERT_ERROR_CODE;
                            resultMessage.status = false;
                            return resultMessage;
                        }
                    }

                    int id = m_LeaveRepository.Update(model, conn);
                    model.leave_type_id = id;

                    resultMessage.data = model;
                    resultMessage.description = "Leave Type updated successfully.";
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

        public ResultMessage DeleteLeave(int key)
        {
            ResultMessage resultMessage = new ResultMessage();
            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    int id = m_LeaveRepository.Delete(key, conn);

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

        public ResultMessage GetAllLeave(QueryParameter queryParameter)
        {
            int total = 0 ;
            ResultMessage resultMessage = new ResultMessage();
            List<tbm_leave_type> assyPartControlModel = new List<tbm_leave_type>();
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                        
                    dt = m_LeaveRepository.GetAllPagination(queryParameter, out total, conn);

                    var data = new { total = total, data = dt.DataTableToList<tbm_leave_type>() };
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
                        var data = new { total = countContentData, data = dt.DataTableToList<tbm_holiday_info>() };
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

        //public ResultMessage GetLeaveType()
        //{
        //    ResultMessage resultMessage = new ResultMessage();
        //    List<tbm_leave_type> assyPartControlModel = new List<tbm_leave_type>();
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
        //        {
        //            conn.Open();
        //            dt = m_LeaveRepository.GetLeaveType(conn);

        //            if (dt.Rows.Count == 0)
        //            {
        //                throw new Exception("No Leave Type data found.");
        //            }

        //            var data = dt.DataTableToList<tbm_leave_type>();
        //            resultMessage.status = true;
        //            resultMessage.code = GlobalMessage.SELECT_SUCCESS_CODE;
        //            resultMessage.data = data;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resultMessage.description = ex.Message;
        //        resultMessage.code = GlobalMessage.SELECT_ERROR_CODE;
        //        resultMessage.status = false;
        //    }

        //    return resultMessage;
        //}
    }
}
