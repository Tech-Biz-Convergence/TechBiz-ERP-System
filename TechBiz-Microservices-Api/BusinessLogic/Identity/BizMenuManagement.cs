using DataLayer.Identitys;
using Utilities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Data;
using Npgsql.Replication.PgOutput.Messages;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ExcelDataReader;
using BusinessEntities.Identity;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using BusinessEntities.HR.MasterModels;

namespace BusinessLogic.Identity
{
    public class BizMenuManagement
    {
        private MenuRepository m_MenuRepository;
        public BizMenuManagement()
        {
            m_MenuRepository = new MenuRepository();
        }

        public ResultMessage GetMenu()
        {
            int total = 0;
            ResultMessage resultMessage = new ResultMessage();
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    dt = m_MenuRepository.GetMenu(conn);
                    var data = dt.DataTableToList<tbm_menu>();
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

        public ResultMessage GetMenuUserRoleMapping(string user_name)
        {
            int total = 0;
            ResultMessage resultMessage = new ResultMessage();
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    dt = m_MenuRepository.GetMenuUserRoleMapping(user_name, conn);
                    var data = dt.DataTableToList<permissionRoleMappingModel>();
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
