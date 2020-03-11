using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigradorDapper.Models
{
    public class BaseRepository
    {
        public string connString = null;

        public BaseRepository(string connString)
        {
            this.connString = connString;
        }

        public async Task<IEnumerable<T>> EnumerableQueryAsync<T>(string query, object parameters)
        {
            using (var conn = new SqlConnection(connString))
            {
                await conn.OpenAsync();
                return await conn.QueryAsync<T>(query, parameters);
            }
        }

        public async Task<int> InsertQueryAsync<T>(string query, object parameters)
        {
            using (var conn = new SqlConnection(connString))
            {
                await conn.OpenAsync();
                var affectedRows = conn.Execute(query, parameters);
                Console.WriteLine($"Affected Rows: {affectedRows}");
                return affectedRows;
            }
        }



        public async Task<T> QuerySingleOrDefaultAsync<T>(string query, object parameters)
        {
            using (var conn = new SqlConnection(connString))
            {
                return (await conn.QueryAsync<T>(query, parameters)).SingleOrDefault();
            }
        }

        public async Task<int> ExecuteAsync(string query, object parameters)
        {
            using (var conn = new SqlConnection(connString))
            {
                return await conn.ExecuteAsync(query, parameters);
            }
        }

        public async Task<long> ExecuteInsertAsync(string query, object parameters)
        {
            using (var conn = new SqlConnection(connString))
            {
                query += "\nSELECT CAST(SCOPE_IDENTITY() as bigint)";
                return (await conn.QueryAsync<long>(query, parameters)).SingleOrDefault();
            }
        }
    }
}