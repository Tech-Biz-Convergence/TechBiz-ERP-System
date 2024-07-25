using BusinessEntities.Identity;
using DataLayer.Identitys;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Middleware;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace BusinessLogic.Identity
{
    public class BizIdentityManagement
    {
        private readonly IJwtBuilder  m_JwtBuilder;
        private readonly IEncryptor m_Encryptor;
        UserRepository m_UserRepository;
        MenuRepository m_MenuRepository;
        public BizIdentityManagement(IJwtBuilder jwtBuilder, IEncryptor encryptor)
        {
            m_JwtBuilder = jwtBuilder;
            m_Encryptor = encryptor;
            m_UserRepository = new UserRepository();
            m_MenuRepository = new MenuRepository();


        }

        public ResultMessage Register(tbm_user_info model)
        {

            ResultMessage resultMessage = new ResultMessage();
            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();

                    tbm_user_info? user = m_UserRepository.GetUser(model.user_name, conn);
                    if (user is not null)
                    {
                        resultMessage.code = "ER-HR-0001";
                        throw new Exception("User already exists.!");
                    }

                    model.SetPassword(model.user_password, m_Encryptor);
                    //insert 
                    long id = m_UserRepository.Insert(model, conn);
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

        public ResultMessage Authenticated(user_login model)
        {
            ResultMessage resultMessage = new ResultMessage();
            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    tbm_user_info? user = m_UserRepository.GetUser(model.user_name, conn);

                    if (user is null)
                    {
                        resultMessage.code = "ER-HR-0002";
                        throw new Exception("User not found.!");
                    }
                    bool isValid = user.ValidatePassword(model.user_password, m_Encryptor);
                    if(!isValid)
                    {
                        resultMessage.code = "ER-HR-0003";
                        throw new Exception("Could not authenticate user.!");
                    }


                    //get token JWT
                    string tokenString = m_JwtBuilder.GetToken(user.user_id.ToString());

                    //get menu
                    DataTable dt = m_MenuRepository.GetMenuUserRoleMapping(model.user_name, conn);
                    var roleMapping = dt.DataTableToList<permissionRoleMappingModel>();

                    resultMessage.status = true;
                    resultMessage.code = GlobalMessage.SUCCESS_CODE;
                    resultMessage.data = new {token = tokenString,user_id = user.user_id,user_name = user.user_name, menu_role_mapping = roleMapping };
                }
                catch (Exception ex)
                {
                    resultMessage.status = false;
                    resultMessage.code = GlobalMessage.ERROR_CODE;
                    resultMessage.description = ex.Message;
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
        public ResultMessage Validate(int userId,string token )
        {
            ResultMessage resultMessage = new ResultMessage();
            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    DataTable dt = m_UserRepository.GetByKey(userId, conn);
                    if (dt is null || dt.Rows.Count == 0)
                    {
                        resultMessage.code = "ER-HR-0002";
                        throw new Exception("User not found.!");
                    }
                    var user = dt.DataTableToList<tbm_user_info>().FirstOrDefault();
                    int userIdFromToken = int.Parse( m_JwtBuilder.ValidateToken(token));
                    if (user.user_id != userIdFromToken)
                    {
                        resultMessage.code = "ER-HR-0004";
                        throw new Exception("Invalid token.");
                    }


                    resultMessage.status = true;
                    resultMessage.code = GlobalMessage.SUCCESS_CODE;
                    resultMessage.data = userIdFromToken;
                }
                catch (Exception ex)
                {
                    resultMessage.status = false;
                    resultMessage.code = GlobalMessage.ERROR_CODE;
                    resultMessage.description = ex.Message;
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

            
    }

    
}
