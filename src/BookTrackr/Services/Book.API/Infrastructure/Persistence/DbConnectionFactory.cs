using Book.API.Application.Abstractions.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Book.API.Infrastructure.Persistence;

internal sealed class DbConnectionFactory(SqlConnection connection) : IDbConnectionFactory
{
    public async Task<IDbConnection> GetOpenConnectionAsync()
    {
        await connection.OpenAsync();

        return connection;
    }
}