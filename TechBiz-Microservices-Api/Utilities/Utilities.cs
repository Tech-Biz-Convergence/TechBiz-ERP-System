using System.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Utilities
{
    public static class GlobalMessage
    {
        public const string SUCCESS_CODE = "SC-COM-0000";
        public const string SELECT_SUCCESS_CODE = "SC-COM-0001";
        public const string INSERT_SUCCESS_CODE = "SC-COM-0002";
        public const string UPDATE_SUCCESS_CODE = "SC-COM-0003";
        public const string DELETE_SUCCESS_CODE = "SC-COM-0004";
        public const string ERROR_CODE = "ER-COM-0000";
        public const string SELECT_ERROR_CODE = "ER-COM-0001";
        public const string INSERT_ERROR_CODE = "ER-COM-0002";
        public const string UPDATE_ERROR_CODE = "ER-COM-0003";
        public const string DELETE_ERROR_CODE = "ER-COM-0004 ";

        // คุณสามารถเพิ่มข้อความอื่นๆ ได้ตามต้องการ
    }
    public class ResultMessage
    {
        
        public bool status { get; set; } = true;
        // public string type { get; set; } = GlobalMessage.typeSuccess;
        public string code { get; set; }
        public string description { get; set; }
        public int createdBy { get; set; }
        public object data { get; set; }

    }

    public  class GlobalVariables
    {
        private static IConfiguration Configuration { get; }

        static GlobalVariables()
        {
            Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        }

        public static string ConnectionString
        {
            get { return Configuration.GetConnectionString("DefaultConnection"); }
        }
    }


    public static class DataTableExtensions
    {
        public static List<T> DataTableToList<T>(this DataTable dataTable) where T : new()
        {
            List<T> list = new List<T>();

            foreach (DataRow row in dataTable.Rows)
            {
                T item = new T();

                foreach (DataColumn column in dataTable.Columns)
                {
                    PropertyInfo property = typeof(T).GetProperty(column.ColumnName);
                    if (property != null && row[column] != DBNull.Value)
                    {
                        property.SetValue(item, row[column]);
                    }
                }

                list.Add(item);
            }

            return list;
        }
    }

    public class QueryParameter
    {
        public int? page { get; set; }
        public int? limit { get; set; }
        public string? sortBy { get; set; }
        public string? sortType { get; set; }

        public string? searchValue { get; set; }
        // Constructor to set default values
        public QueryParameter()
        {
            page = 1;
            limit = 10;
        }
    }
}
