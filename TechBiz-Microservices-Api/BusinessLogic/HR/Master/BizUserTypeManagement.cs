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


namespace BusinessLogic.HR.Master
{

    public class BizUserTypeManagement
    {
        private UserTypeRepository m_UserTypeRepository;
        public BizUserTypeManagement()
        {
            m_UserTypeRepository = new UserTypeRepository();
        }

        public ResultMessage GetAllUserType()
        {
            int total = 0;
            ResultMessage resultMessage = new ResultMessage();
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    dt = m_UserTypeRepository.GetAll(conn);
                    var data = dt.DataTableToList<tbm_user_type>();
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

        public ResultMessage GetUserTypeById(int id)
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
                    dt = m_UserTypeRepository.GetByKey(id, conn);
                    var data = dt.DataTableToList<tbm_user_type>().FirstOrDefault();
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

        public ResultMessage AddNewUserType(tbm_user_type model)
        {
            ResultMessage resultMessage = new ResultMessage();

            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    int id = m_UserTypeRepository.Insert(model, conn);
                    model.user_type_id = id;

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

        public ResultMessage UpdateUserType(tbm_user_type model)
        {
            ResultMessage resultMessage = new ResultMessage();
            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    int id = m_UserTypeRepository.Update(model, conn);
                    model.user_type_id = id;

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

        public ResultMessage DeleteUserType(int key)
        {
            ResultMessage resultMessage = new ResultMessage();
            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    int id = m_UserTypeRepository.Delete(key, conn);

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

        public ResultMessage GetAllUserType(QueryParameter queryParameter)
        {
            int total = 0;
            ResultMessage resultMessage = new ResultMessage();
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();

                    dt = m_UserTypeRepository.GetAllPagination(queryParameter, out total, conn);

                    var data = new { total = total, data = dt.DataTableToList<tbm_user_type>() };
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
        public ResultMessage GetUserTypeActive()
        {
            ResultMessage resultMessage = new ResultMessage();
            DataTable dt = new DataTable();
            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    dt = m_UserTypeRepository.GetActive(conn);
                    var data = dt.DataTableToList<tbm_user_type>();
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
