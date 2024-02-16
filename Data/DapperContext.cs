using System.Data;
using Microsoft.Data.SqlClient;

namespace SmartWayTest.Data;

public class DapperContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    

    public DapperContext(IConfiguration config)
    {
        _configuration = config;
        _connectionString = _configuration.GetConnectionString("SqlConnection");
    }

    public IDbConnection CreateConnection() =>
        new SqlConnection(_connectionString);
}