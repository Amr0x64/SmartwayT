using Dapper;
using SmartWayTest.Data;

namespace SmartWayTest;

public class InitializeBd
{
    /// <summary>
    /// Create migration to DB.
    /// </summary>
    /// <param name="context">The context to use for this initialize</param>
    public async static Task Initialize(DapperContext context)
    {
        using (var connection = context.CreateConnection())
        {
            string query = File.ReadAllText(@"Scripts\Sqlinitialize.sql");

            int count = await connection.ExecuteAsync(query);
            Console.WriteLine(count);
        }
    }
}