using System.Data;
using Npgsql;

namespace Pde.Worker.Data.Database.Implementations;

public class PostgresConnectionFactory : IDbConnectionFactory
{
    public IDbConnection Connect(string connectionString)
    {
        return new NpgsqlConnection(connectionString);
    }

    public string CreateConnectionString(string username, string password, string host, string port, string database)
    {
        return $@"
            User ID={username};
            Password={password};
            Host={host};
            Port={port};
            Database={database};
        ";
    }
}