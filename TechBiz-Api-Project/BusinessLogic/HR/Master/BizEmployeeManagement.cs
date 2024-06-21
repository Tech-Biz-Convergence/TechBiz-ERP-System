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



namespace BusinessLogic.HR.Master
{
    public class BizEmployeeManagement
    {
        const double PI = 3.14158;
        const int VAT_THAI = 7;
        private EmployeeRepository m_EmployeeRepository;

        public BizEmployeeManagement()
        {
            m_EmployeeRepository = new EmployeeRepository();
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
                    dt = m_EmployeeRepository.GetAll(conn);
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


        public ResultMessage GetEmployeeById(int id)
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
                    dt = m_EmployeeRepository.GetByKey(id,conn);
                    var data = dt.DataTableToList<tm_employee_info>().FirstOrDefault();
                    if(data is null)
                    {
                        resultMessage.code = "ERR-HR-9999";
                        throw new Exception("Data not found!");
                        
                    }

                    resultMessage.status = true;
                    //resultMessage.code = GlobalMessage.success SELECT DATA
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

        public ResultMessage AddNewEmployee(tm_employee_info model)
        {
            ResultMessage resultMessage = new ResultMessage();
            using (NpgsqlConnection conn = new NpgsqlConnection(GlobalVariables.ConnectionString))
            {
                try
                {
                    conn.Open();
                    int id = m_EmployeeRepository.Insert(model, conn);
                    model.id = id;

                    resultMessage.data = model;
                    //  ultMessage.code = GlobalMessage.successInsert;
                    resultMessage.status = true;

                    //resultMessage.description = ex.ToString();
                    //  resultMessage.code = GlobalMessage.successInsert;
                    resultMessage.status = true;
                }
                catch(Exception ex)
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
