using BusinessEntities.HR.MasterModels;
using BusinessEntities.Identity;
using DataLayer.HR.MasterModels;
using DataLayer.Identitys;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace BusinessLogic.Identity
{
    public class BizUserManagement
    {
        private UserRepository m_UserRepository;

        public BizUserManagement()
        {
            m_UserRepository = new UserRepository();
        }
        public ResultMessage GetAllUser()
        {
            int total = 0;
            ResultMessage resultMessage = new ResultMessage();
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    dt = m_UserRepository.GetAll(conn);
                    var data = dt.DataTableToList<tbm_user_info>();
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

        public ResultMessage GetUserById(int id)
        {
            ResultMessage resultMessage = new ResultMessage();
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    dt = m_UserRepository.GetByKey(id, conn);
                    var data = dt.DataTableToList<userInfoModel>().FirstOrDefault();
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

        public ResultMessage AddNewUser(tbm_user_info model)
        {
            ResultMessage resultMessage = new ResultMessage();

            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    long id = m_UserRepository.Insert(model, conn);
                    model.user_id = id;

                    resultMessage.data = model;
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

        public ResultMessage UpdateUser(tbm_user_info model)
        {
            ResultMessage resultMessage = new ResultMessage();
            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    int id = m_UserRepository.Update(model, conn);
                    model.user_id = id;

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

        public ResultMessage DeleteUser(int key)
        {
            ResultMessage resultMessage = new ResultMessage();
            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    int id = m_UserRepository.Delete(key, conn);

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

        public ResultMessage GetAllUser(QueryParameter queryParameter)
        {
            int total = 0;
            ResultMessage resultMessage = new ResultMessage();
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();

                    dt = m_UserRepository.GetAllPagination(queryParameter, out total, conn);

                    var data = new { total, data = dt.DataTableToList<userInfoModel>() };
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
        public ResultMessage ActivateCondition(int id, string user_name, string status)
        {
            int total = 0;
            ResultMessage resultMessage = new ResultMessage();

            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();

                    int ret = m_UserRepository.UpdateActive(id, user_name, status, conn);


                    resultMessage.status = true;
                    resultMessage.data = ret;
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
            if (uploadfile != null && uploadfile.Length > 0)
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Stream stream = uploadfile.OpenReadStream();
                IExcelDataReader reader = null;
                if (uploadfile.FileName.EndsWith(".xls"))
                {
                    reader = ExcelReaderFactory.CreateReader(stream);
                }
                else if (uploadfile.FileName.EndsWith(".xlsx"))
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                else
                {

                }
                DataTable dt = new DataTable();
                DataTable dt_ = new DataTable();
                DataRow row;
                List<Dictionary<string, object>> dataExcelList = new List<Dictionary<string, object>>();
                try
                {
                    dt_ = reader.AsDataSet().Tables[0];
                    if (!"Name".Equals(dt_.Rows[0][0])
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
                            Dictionary<string, object> dataDic = new Dictionary<string, object>();
                            dataDic.Add("name", dt_.Rows[row_][0]);

                            dataExcelList.Add(dataDic);
                        }//end for
                        var data = new { total = countContentData, data = dt.DataTableToList<tbm_user_info>() };
                        resultMessage.status = true;
                        resultMessage.data = data;

                    }//end if

                }
                catch (Exception ex)
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
