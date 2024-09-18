using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DapperData.Data
{
    public class DataAccess : IDataAccess
    {
        private readonly IConfiguration _configuration;

        public DataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<IEnumerable<T>> GetData<T, P>(string query, P parameters, string connectionId = "DefaultConnection")
        {
            using IDbConnection connection = 
                new SqlConnection(_configuration.GetConnectionString(connectionId));

            return await connection.QueryAsync<T>(query, parameters);
        }

        public async Task SaveData<P>(string query, P parameters, string connectionId = "DefaultConnection")
        {
            using IDbConnection connection = 
                new SqlConnection(_configuration.GetConnectionString(connectionId));

            await connection.ExecuteAsync(query, parameters);
        }
    }
}
