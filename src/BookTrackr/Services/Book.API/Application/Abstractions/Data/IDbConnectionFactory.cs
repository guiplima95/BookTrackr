using System.Data;

namespace Book.API.Application.Abstractions.Data;

public interface IDbConnectionFactory
{
    IDbConnection GetOpenConnection();
}