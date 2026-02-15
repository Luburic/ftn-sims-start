using System.Data;

namespace CommunityHub.Application.Database.Repositories;

public class UserDbRepository
{
    public bool Exists(string username, string password)
    {
        using IDbConnection connection = PostgresConnection.CreateConnection();

        IDbCommand command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM users WHERE username = @username AND password = @password";

        IDbDataParameter usernameParam = command.CreateParameter();
        usernameParam.ParameterName = "@username";
        usernameParam.Value = username;
        command.Parameters.Add(usernameParam);

        IDbDataParameter passwordParam = command.CreateParameter();
        passwordParam.ParameterName = "@password";
        passwordParam.Value = password;
        command.Parameters.Add(passwordParam);

        object? result = command.ExecuteScalar();
        long count = result != null ? Convert.ToInt64(result) : 0;

        return count > 0;
    }
}
