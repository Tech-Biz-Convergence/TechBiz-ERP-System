using BusinessEntities.Identity;
using DataLayer.Core;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace DataLayer.Identitys
{
    public class MenuRepository : IBigIntDataRepository<tbm_menu>
    {
        public int Delete(long Key, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public DataTable GetAll(NpgsqlConnection conn)
        {
            throw new NotImplementedException();
        }

        public DataTable GetByKey(long Key, NpgsqlConnection conn)
        {
            throw new NotImplementedException();
        }

        public long Insert(tbm_menu Model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public int Update(tbm_menu Model, NpgsqlConnection conn, NpgsqlTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public DataTable GetMenu( NpgsqlConnection conn)
        {
            try
            {
                NpgsqlCommand sqlCommand = new NpgsqlCommand();
                DataTable dataTable = new DataTable();

                String sql = @" WITH RECURSIVE menu_hierarchy AS 
                                (
                                    SELECT menu_id, menu_name,menu_icon,menu_parent_id, program_path, seq_no, 1 AS level, CAST(seq_no AS VARCHAR(255)) AS path
                                    FROM authentication.tbm_menu
                                    WHERE menu_parent_id IS NULL
                                    UNION ALL
                                    SELECT m.menu_id, m.menu_name,m.menu_icon, m.menu_parent_id, m.program_path, m.seq_no, mh.level + 1, CAST(mh.path || '.' || m.seq_no AS VARCHAR(255)) AS path
                                    FROM authentication.tbm_menu m
                                    INNER JOIN menu_hierarchy mh ON m.menu_parent_id = mh.menu_id
                                )
                                SELECT * FROM menu_hierarchy
                                ORDER BY path ";


                sqlCommand.Connection = conn;

                //get data
                sqlCommand.CommandText = sql;
                NpgsqlDataReader reader = sqlCommand.ExecuteReader();
                dataTable.Load(reader);
                return dataTable;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable GetMenuUserRoleMapping(string user_name, NpgsqlConnection conn)
        {
            try
            {
                NpgsqlCommand sqlCommand = new NpgsqlCommand();
                DataTable dataTable = new DataTable();

                String sql = @"WITH RECURSIVE menu_hierarchy AS (
    -- Select root menus
    SELECT 
        m.menu_id, 
        m.menu_parent_id, 
        m.menu_seq_no, 
        1 AS level, 
        CAST(m.menu_seq_no AS VARCHAR(255)) AS path, 
        m.permiss_add_status, 
        m.permiss_edit_status, 
        m.permiss_delete_status, 
        m.permiss_upload_status, 
        m.permiss_download_status,
        m.permiss_view_status, 
        m.permission_id
    FROM 
        authentication.tbm_permission_role_mapping m
    WHERE 
        m.menu_parent_id IS NULL 
        AND (
            (m.dep_id, m.role_id, m.user_id) IN (
                SELECT 
                    pos.dept_id, 
                    ui.role_id, 
                    ui.user_id 
                FROM 
                    authentication.tbm_user_info ui
                JOIN 
                    hr.tbm_employee_info emp ON ui.emp_id = emp.emp_id
                JOIN 
                	hr.tbm_position pos on emp.position_id =  pos.position_id
                WHERE 
                    ui.user_name = 'warut'
            )
            OR 
            (m.dep_id, m.role_id) IN (
                 SELECT 
                    pos.dept_id, 
                    ui.role_id
                FROM 
                    authentication.tbm_user_info ui
                JOIN 
                    hr.tbm_employee_info emp ON ui.emp_id = emp.emp_id
                JOIN 
                	hr.tbm_position pos on emp.position_id =  pos.position_id
                WHERE 
                    ui.user_name = 'warut'
            )
        )

    UNION ALL

    -- Select child menus recursively
    SELECT 
        m.menu_id, 
        m.menu_parent_id, 
        m.menu_seq_no, 
        mh.level + 1, 
        CAST(mh.path || '.' || m.menu_seq_no AS VARCHAR(255)) AS path, 
        m.permiss_add_status, 
        m.permiss_edit_status, 
        m.permiss_delete_status, 
        m.permiss_upload_status, 
        m.permiss_download_status,
        m.permiss_view_status,
        m.permission_id
    FROM 
        authentication.tbm_permission_role_mapping m
    INNER JOIN 
        menu_hierarchy mh ON m.menu_parent_id = mh.menu_id
    WHERE 
        (m.dep_id, m.role_id, m.user_id) IN (
             SELECT 
                    pos.dept_id, 
                    ui.role_id, 
                    ui.user_id 
                FROM 
                    authentication.tbm_user_info ui
                JOIN 
                    hr.tbm_employee_info emp ON ui.emp_id = emp.emp_id
                JOIN 
                	hr.tbm_position pos on emp.position_id =  pos.position_id
                WHERE 
                    ui.user_name = 'warut'
        )
        OR 
        (m.dep_id, m.role_id) IN (
             SELECT 
                    pos.dept_id, 
                    ui.role_id
                FROM 
                    authentication.tbm_user_info ui
                JOIN 
                    hr.tbm_employee_info emp ON ui.emp_id = emp.emp_id
                JOIN 
                	hr.tbm_position pos on emp.position_id =  pos.position_id
                WHERE 
                    ui.user_name = 'warut'
        )
)

SELECT tbHier.*, tbMenu.menu_name, tbMenu.menu_icon, tbMenu.program_path,tbMenu.program_code 
FROM menu_hierarchy tbHier
INNER JOIN authentication.tbm_menu tbMenu ON tbHier.menu_id = tbMenu.menu_id
ORDER BY path;";
                sqlCommand.Parameters.Add(new NpgsqlParameter("@user_name", SqlDbType.VarChar)).Value = user_name;


                sqlCommand.Connection = conn;

                //get data
                sqlCommand.CommandText = sql;
                NpgsqlDataReader reader = sqlCommand.ExecuteReader();
                dataTable.Load(reader);
                return dataTable;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
