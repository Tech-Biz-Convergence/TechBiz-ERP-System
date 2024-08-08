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
    public class BizHolidayManagement
    {
        private HolidayRepository m_HolidayRepository;

        public BizHolidayManagement()
        {
            m_HolidayRepository = new HolidayRepository();
        }

        public ResultMessage GetAllHoliday()
        {
            int total = 0;
            ResultMessage resultMessage = new ResultMessage();
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    dt = m_HolidayRepository.GetAll(conn);
                    var data = dt.DataTableToList<tbm_holiday_info>();
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

        public ResultMessage GetHolidayById(int id)
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
                    dt = m_HolidayRepository.GetByKey(id,conn);
                    var data = dt.DataTableToList<tbm_holiday_info>().FirstOrDefault();
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

        public ResultMessage AddNewHoliday(tbm_holiday_info model)
        {
            ResultMessage resultMessage = new ResultMessage();

            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();

                    if (string.IsNullOrWhiteSpace(model.holiday_year))
                    {
                        resultMessage.description = "Holiday Year Null. Please Enter Data.";
                        resultMessage.code = GlobalMessage.INSERT_ERROR_CODE;
                        resultMessage.status = false;
                        return resultMessage;
                    }
                    if (string.IsNullOrWhiteSpace(model.holiday_name))
                    {
                        resultMessage.description = "Holiday Name Null. Please Enter Data.";
                        resultMessage.code = GlobalMessage.INSERT_ERROR_CODE;
                        resultMessage.status = false;
                        return resultMessage;
                    }
                    if (model.holiday_day == DateTime.MinValue)
                    {
                        resultMessage.description = "Holiday Date Null. Please Enter Data.";
                        resultMessage.code = GlobalMessage.INSERT_ERROR_CODE;
                        resultMessage.status = false;
                        return resultMessage;
                    }

                    string checkSql = @"SELECT COUNT(1) FROM hr.tbm_holiday_info WHERE holiday_year = @holiday_year
                            AND holiday_name = @holiday_name
                            AND holiday_day = @holiday_day";
                    using (var checkCmd = new NpgsqlCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.Add("@holiday_year", NpgsqlDbType.Varchar).Value = model.holiday_year;
                        checkCmd.Parameters.Add("@holiday_name", NpgsqlDbType.Varchar).Value = model.holiday_name;
                        checkCmd.Parameters.Add("@holiday_day", NpgsqlDbType.Date).Value = model.holiday_day;

                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            resultMessage.description = "Department name Duplicate.";
                            resultMessage.code = GlobalMessage.INSERT_ERROR_CODE;
                            resultMessage.status = false;
                            return resultMessage;
                        }
                    }

                    int id = m_HolidayRepository.Insert(model, conn);
                    model.holiday_id = id;

                    resultMessage.data = model;
                    resultMessage.description = "Department added successfully.";
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

        public ResultMessage UpdateHoliday(tbm_holiday_info model)
        {
            ResultMessage resultMessage = new ResultMessage();
            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();

                    if (string.IsNullOrWhiteSpace(model.holiday_year))
                    {
                        resultMessage.description = "Holiday Year Null. Please Enter Data.";
                        resultMessage.code = GlobalMessage.INSERT_ERROR_CODE;
                        resultMessage.status = false;
                        return resultMessage;
                    }
                    if (string.IsNullOrWhiteSpace(model.holiday_name))
                    {
                        resultMessage.description = "Holiday Name Null. Please Enter Data.";
                        resultMessage.code = GlobalMessage.INSERT_ERROR_CODE;
                        resultMessage.status = false;
                        return resultMessage;
                    }
                    if (model.holiday_day == DateTime.MinValue)
                    {
                        resultMessage.description = "Holiday Date Null. Please Enter Data.";
                        resultMessage.code = GlobalMessage.INSERT_ERROR_CODE;
                        resultMessage.status = false;
                        return resultMessage;
                    }

                    string checkSql = @"SELECT COUNT(1) FROM hr.tbm_holiday_info WHERE holiday_year = @holiday_year
                            AND holiday_name = @holiday_name
                            AND holiday_day = @holiday_day 
                            AND holiday_id != @holiday_id";
                    using (var checkCmd = new NpgsqlCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.Add("@holiday_year", NpgsqlDbType.Varchar).Value = model.holiday_year;
                        checkCmd.Parameters.Add("@holiday_name", NpgsqlDbType.Varchar).Value = model.holiday_name;
                        checkCmd.Parameters.Add("@holiday_day", NpgsqlDbType.Date).Value = model.holiday_day;
                        checkCmd.Parameters.Add("@holiday_id", NpgsqlDbType.Bigint).Value = model.holiday_id;

                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            resultMessage.description = "Data is Duplicate.";
                            resultMessage.code = GlobalMessage.UPDATE_ERROR_CODE;
                            resultMessage.status = false;
                            return resultMessage;
                        }
                    }

                    int id = m_HolidayRepository.Update(model, conn);
                    model.holiday_id = id;

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

        public ResultMessage DeleteHoliday(int key)
        {
            ResultMessage resultMessage = new ResultMessage();
            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    int id = m_HolidayRepository.Delete(key, conn);

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

        public ResultMessage GetAllHoliday(QueryParameter queryParameter)
        {
            int total = 0 ;
            ResultMessage resultMessage = new ResultMessage();
            List<tbm_holiday_info> assyPartControlModel = new List<tbm_holiday_info>();
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                        
                    dt = m_HolidayRepository.GetAllPagination(queryParameter, out total, conn);

                    var data = new { total = total, data = dt.DataTableToList<tbm_holiday_info>() };
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


    }
}
