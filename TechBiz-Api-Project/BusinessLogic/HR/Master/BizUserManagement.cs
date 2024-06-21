using BusinessEntities.HR.MasterModels;
using DataLayer.HR.MasterModels;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BusinessLogic.HR.Master
{
    public class BizUserManagement
    {
        private UserRepository m_UserRepository;

        public BizUserManagement()
        {
            m_UserRepository = new UserRepository();
        }

        public ResultMessage Authenticated(tbm_user_info_role model)
        {
            ResultMessage resultMessage = new ResultMessage();
            DataTable dt = new DataTable();
            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    dt = m_UserRepository.GetUser(model.user_name,conn);
                    model = dt.DataTableToList<tbm_user_info_role>().FirstOrDefault();

                    resultMessage.status = true;
                    resultMessage.data = model;
                }
                catch (Exception ex)
                {
                    resultMessage.status = false;
                    resultMessage.data = ex.Message;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }//end using

            return resultMessage;
            
        }

        public ResultMessage GetAllEmployee()
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
                    var data = dt.DataTableToList<tm_employee_info>();
                    resultMessage.status = true;
                    resultMessage.data = data;
                }
                catch (Exception ex)
                {
                    resultMessage.description = ex.ToString();
                    //  resultMessage.code = GlobalMessage.errorUpdate;
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
