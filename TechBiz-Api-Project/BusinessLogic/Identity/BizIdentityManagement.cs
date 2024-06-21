using BusinessEntities.HR.MasterModels;
using DataLayer.HR.MasterModels;
using Microsoft.Extensions.Configuration;
using Middleware;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BusinessLogic.Identity
{
    public class BizIdentityManagement
    {
        private IConfiguration m_Config { get; }

        private Encryptor m_Encryptor;
        private UserRepository m_UserRepository;
        private Jwt m_Jwt;

        public BizIdentityManagement(IConfiguration config)
        {
            m_Config = config;
            m_UserRepository = new UserRepository();
            m_Encryptor = new Encryptor();
            m_Jwt = new Jwt(m_Config);
        }

        public ResultMessage Register(tbm_user_info model)
        {
            ResultMessage resultMessage = new ResultMessage();
            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();

                    DataTable dt = new DataTable();
                    dt = m_UserRepository.GetUser(model.user_name, conn);
                    if (dt is not null && dt.Rows.Count > 0)
                    {
                        resultMessage.code = "ER-HR-0001";
                        throw new Exception("User already exists.!");
                    }

                    model.password = GetPasswordSalt(model.password);

                    //insert 
                    int id = m_UserRepository.Insert(model, conn);
                    model.user_id = id;


                    resultMessage.code = GlobalMessage.INSERT_SUCCESS_CODE;
                    resultMessage.status = true;
                }
                catch (Exception ex)
                {
                    resultMessage.description = ex.ToString();
                    resultMessage.code = GlobalMessage.INSERT_ERROR_CODE;
                    resultMessage.status = false;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) conn.Close();

                }
            }//end using

            return resultMessage;
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
                    dt = m_UserRepository.GetUser(model.user_name, conn);
                    if(dt is null || dt.Rows.Count == 0)
                    {
                        resultMessage.code = "ER-HR-0002";
                        throw new Exception("User not found.!");
                    }
                    var userInfo = dt.DataTableToList<tbm_user_info_role>().FirstOrDefault();

                    string salt = m_Encryptor.GetSalt();
                    //Validate password
                    if(!ValidatePassword(model.password, userInfo.password,salt))
                    {
                        resultMessage.code = "ER-HR-0003";
                        throw new Exception("Could not authenticate user.!");
                    }


                    //get token JWT
                   string tokenString = m_Jwt.BuildToken(model);


                    resultMessage.status = true;
                    resultMessage.code = GlobalMessage.SUCCESS_CODE;
                    resultMessage.data = new {token = tokenString};
                }
                catch (Exception ex)
                {
                    resultMessage.status = false;
                    resultMessage.code = GlobalMessage.ERROR_CODE;
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

        public string GetPasswordSalt(string password)
        {
            string salt = m_Encryptor.GetSalt();
            return m_Encryptor.GetHash(password, salt);
        }
        public bool ValidatePassword(string password,string passwordEncryp,string salt)
        {
            string pass = m_Encryptor.GetHash(password, salt);
            return passwordEncryp.Equals(pass);
        }
            
    }

    
}
