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
    public class BizInterviewManagement
    {
        private InterviewRepository m_InterviewRepository;

        public BizInterviewManagement()
        {
            m_InterviewRepository = new InterviewRepository();
        }

        public ResultMessage GetAllInterview()
        {
            int total = 0;
            ResultMessage resultMessage = new ResultMessage();
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    dt = m_InterviewRepository.GetAll(conn);
                    var data = dt.DataTableToList<tbm_interview>();
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

        public ResultMessage GetInterviewById(int id)
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
                    dt = m_InterviewRepository.GetByKey(id,conn);
                    var data = dt.DataTableToList<tbm_interview>().FirstOrDefault();
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

        public ResultMessage AddNewInterview(tbm_interview model)
        {
            ResultMessage resultMessage = new ResultMessage();

            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();

                    if (model.job_id == null)
                    {
                        resultMessage.description = "Job is Null. Please Enter Data.";
                        resultMessage.code = GlobalMessage.INSERT_ERROR_CODE;
                        resultMessage.status = false;
                        return resultMessage;
                    }
                    if (string.IsNullOrEmpty(model.interview_quest))
                    {
                        resultMessage.description = "Question is Null. Please Enter Data.";
                        resultMessage.code = GlobalMessage.INSERT_ERROR_CODE;
                        resultMessage.status = false;
                        return resultMessage;
                    }

                    string checkSql = @"SELECT COUNT(1) FROM hr.tbm_interview 
                            WHERE job_id = @job_id
                            AND interview_quest = @interview_quest";
                    using (var checkCmd = new NpgsqlCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.Add("@job_id", NpgsqlDbType.Bigint).Value = model.job_id;
                        checkCmd.Parameters.Add("@interview_quest", NpgsqlDbType.Varchar).Value = model.interview_quest;

                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            resultMessage.description = "Data is Duplicate.";
                            resultMessage.code = GlobalMessage.INSERT_ERROR_CODE;
                            resultMessage.status = false;
                            return resultMessage;
                        }
                    }

                    int id = m_InterviewRepository.Insert(model, conn);
                    model.interview_id = id;

                    resultMessage.data = model;
                    resultMessage.description = "Interview added successfully.";
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

        public ResultMessage UpdateInterview(tbm_interview model)
        {
            ResultMessage resultMessage = new ResultMessage();
            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();

                    if (model.job_id == null)
                    {
                        resultMessage.description = "Job is Null. Please Enter Data.";
                        resultMessage.code = GlobalMessage.INSERT_ERROR_CODE;
                        resultMessage.status = false;
                        return resultMessage;
                    }
                    if (string.IsNullOrEmpty(model.interview_quest))
                    {
                        resultMessage.description = "Question is Null. Please Enter Data.";
                        resultMessage.code = GlobalMessage.INSERT_ERROR_CODE;
                        resultMessage.status = false;
                        return resultMessage;
                    }

                    string checkSql = @"SELECT COUNT(1) FROM hr.tbm_interview 
                            WHERE job_id = @job_id
                            AND interview_quest = @interview_quest
                            AND interview_id = @interview_id ";
                    using (var checkCmd = new NpgsqlCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.Add("@job_id", NpgsqlDbType.Bigint).Value = model.job_id;
                        checkCmd.Parameters.Add("@interview_quest", NpgsqlDbType.Varchar).Value = model.interview_quest;
                        checkCmd.Parameters.Add("@interview_id", NpgsqlDbType.Bigint).Value = model.interview_id; 

                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            resultMessage.description = "Data is Duplicate.";
                            resultMessage.code = GlobalMessage.INSERT_ERROR_CODE;
                            resultMessage.status = false;
                            return resultMessage;
                        }
                    }

                    int id = m_InterviewRepository.Update(model, conn);
                    model.interview_id = id;

                    resultMessage.data = model;
                    resultMessage.description = "Interview updated successfully.";
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

        public ResultMessage DeleteInterview(int key)
        {
            ResultMessage resultMessage = new ResultMessage();
            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    int id = m_InterviewRepository.Delete(key, conn);

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

        public ResultMessage GetAllInterview(QueryParameter queryParameter)
        {
            int total = 0 ;
            ResultMessage resultMessage = new ResultMessage();
            List<tbm_interview> assyPartControlModel = new List<tbm_interview>();
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                        
                    dt = m_InterviewRepository.GetAllPagination(queryParameter, out total, conn);

                    var data = new { total = total, data = dt.DataTableToList<tbm_interview>() };
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
                        var data = new { total = countContentData, data = dt.DataTableToList<tbm_interview>() };
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

        public ResultMessage GetJobName()
        {
            ResultMessage resultMessage = new ResultMessage();
            List<interviewforModel> assyPartControlModel = new List<interviewforModel>();
            DataTable dt = new DataTable();
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
                {
                    conn.Open();
                    dt = m_InterviewRepository.GetJobName(conn);

                    if (dt.Rows.Count == 0)
                    {
                        throw new Exception("No Job data found.");
                    }

                    var data = dt.DataTableToList<interviewforModel>();
                    resultMessage.status = true;
                    resultMessage.code = GlobalMessage.SELECT_SUCCESS_CODE;
                    resultMessage.data = data;
                }
            }
            catch (Exception ex)
            {
                resultMessage.description = ex.Message;
                resultMessage.code = GlobalMessage.SELECT_ERROR_CODE;
                resultMessage.status = false;
            }

            return resultMessage;
        }
    }
}
