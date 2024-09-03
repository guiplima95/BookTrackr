using System.Data;

namespace Book.API.Application.Abstractions.Data;

public interface IDbConnectionFactory
{
    Task<IDbConnection> GetOpenConnectionAsync();
}