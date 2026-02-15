using System.Data;
using CommunityHub.Application.Domain;

namespace CommunityHub.Application.Database.Repositories;

public class UserDbRepository
{
    public long GetIdByCredentials(string username, string password)
    {
        using IDbConnection connection = PostgresConnection.CreateConnection();

        IDbCommand command = connection.CreateCommand();
        command.CommandText = "SELECT id FROM users WHERE username = @username AND password = @password";

        IDbDataParameter usernameParam = command.CreateParameter();
        usernameParam.ParameterName = "@username";
        usernameParam.Value = username;
        command.Parameters.Add(usernameParam);

        IDbDataParameter passwordParam = command.CreateParameter();
        passwordParam.ParameterName = "@password";
        passwordParam.Value = password;
        command.Parameters.Add(passwordParam);

        object? result = command.ExecuteScalar();

        if (result != null)
        {
            return Convert.ToInt64(result);
        }

        return -1;
    }

    public User GetWithPosts(long userId)
    {
        using IDbConnection connection = PostgresConnection.CreateConnection();

        IDbCommand command = connection.CreateCommand();
        command.CommandText = @"
            SELECT u.id, u.username, u.password, u.name, u.surname, u.birthday,
                   p.id AS post_id, p.title, p.content, p.created_at
            FROM users u
            LEFT JOIN posts p ON u.id = p.user_id
            WHERE u.id = @userId";

        IDbDataParameter userIdParam = command.CreateParameter();
        userIdParam.ParameterName = "@userId";
        userIdParam.Value = userId;
        command.Parameters.Add(userIdParam);

        using IDataReader reader = command.ExecuteReader();

        User? user = null;

        while (reader.Read())
        {
            if (user == null)
            {
                long id = reader.GetInt64(0);
                string username = reader.GetString(1);
                string password = reader.GetString(2);
                string name = reader.GetString(3);
                string surname = reader.GetString(4);
                DateTime birthday = reader.GetDateTime(5);

                user = new User(id, username, password, name, surname, birthday);
            }

            if (!reader.IsDBNull(6))
            {
                long postId = reader.GetInt64(6);
                string title = reader.GetString(7);
                string content = reader.GetString(8);
                DateTime createdAt = reader.GetDateTime(9);

                Post post = new Post(postId, title, content, createdAt);
                user.AddPost(post);
            }
        }

        return user!;
    }
}
