using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Npgsql;

namespace DataLayer.Core
{
    interface IDataRepository<T> where T : class
    {
        int Insert(T Model, NpgsqlConnection conn, NpgsqlTransaction transaction = null);
        int Delete(int Key, NpgsqlConnection conn, NpgsqlTransaction transaction = null);
        int Update(T Model, NpgsqlConnection conn, NpgsqlTransaction transaction = null);
        DataTable GetByKey(int Key, NpgsqlConnection conn);
        DataTable GetAll(NpgsqlConnection conn);

    }

    interface IBigIntDataRepository<T> where T : class
    {
        Int64 Insert(T Model, NpgsqlConnection conn, NpgsqlTransaction transaction = null);
        int Delete(Int64 Key, NpgsqlConnection conn, NpgsqlTransaction transaction = null);
        int Update(T Model, NpgsqlConnection conn, NpgsqlTransaction transaction = null);
        DataTable GetByKey(Int64 Key, NpgsqlConnection conn);
        DataTable GetAll(NpgsqlConnection conn);
    }

}
