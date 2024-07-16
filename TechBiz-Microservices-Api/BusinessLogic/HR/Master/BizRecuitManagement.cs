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
    public class BizRecuitManagement
    {
        private RecuitRepository m_RecuitRepository;

        public BizRecuitManagement()
        {
            m_RecuitRepository = new RecuitRepository();
        }

        public ResultMessage GetAllRecuit()
        {            
            ResultMessage resultMessage = new ResultMessage();
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    dt = m_RecuitRepository.GetAll(conn);
                    var data = dt.DataTableToList<tbm_recuit_stage>();
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

       


        public ResultMessage GetRecuitById(int id)
        {           
            ResultMessage resultMessage = new ResultMessage();
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    dt = m_RecuitRepository.GetByKey(id,conn);
                    var data = dt.DataTableToList<tbm_recuit_stage>().FirstOrDefault();
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

        public ResultMessage AddNewRecuit(tbm_recuit_stage model)
        {
            ResultMessage resultMessage = new ResultMessage();

            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    int id = m_RecuitRepository.Insert(model, conn);
                    model.recuit_stage_id = id;

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

        public ResultMessage UpdateRecuit(tbm_recuit_stage model)
        {
            ResultMessage resultMessage = new ResultMessage();
            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    int id = m_RecuitRepository.Update(model, conn);
                    model.recuit_stage_id = id;

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

        public ResultMessage DeleteRecuit(int key)
        {
            ResultMessage resultMessage = new ResultMessage();
            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    int id = m_RecuitRepository.Delete(key, conn);

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

        public ResultMessage GetAllRecuit(QueryParameter queryParameter)
        {
            int total = 0 ;
            ResultMessage resultMessage = new ResultMessage();
            List<tbm_recuit_stage> assyPartControlModel = new List<tbm_recuit_stage>();
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                        
                    dt = m_RecuitRepository.GetAllPagination(queryParameter, out total, conn);

                    var data = new { total = total, data = dt.DataTableToList<tbm_recuit_stage>() };
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
        public ResultMessage ActivateCondition(int id,int user_id,bool is_active)
        {
            int total = 0;
            ResultMessage resultMessage = new ResultMessage();
            List<tbm_recuit_stage> assyPartControlModel = new List<tbm_recuit_stage>();

            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();

                    int ret = m_RecuitRepository.UpdateActive(id, user_id, is_active,conn);


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
    }
}
