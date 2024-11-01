using Dapper;
using HW.Entities;
using HW.InterFaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HW.Repositories;
public class UserDapperRepository : GeneralDapperRepository<User>, IUserRepository
{
    public User Get(string username)
    {
        using SqlConnection cn = new(_connectionString);
        cn.Open();
        string query = "SELECT * FROM Users WHERE UserName = @username";
        var user = cn.QueryFirst<User>(query, new { UserName = username });
        //return cn.QuerySingle<User>(query, new { Id = id });
        return user;
    }

    public List<User> Search(string username)
    {
        using SqlConnection cn = new(_connectionString);
        cn.Open();
        string query = "SELECT * FROM Users WHERE UserName LIKE @Prefix";
        var users = cn.Query<User>(query, new { prefix = username + "%" }).AsList();
        return users;
    }

    public void Update(User user)
    {
        using SqlConnection cn = new(_connectionString);
        cn.Open();
        string query = "UPDATE Users SET Password = @UserPassword , Status = @UserStatus  WHERE Id = @UserId";
        cn.Execute(query, new { UserPassword = user.Password, UserStatus = user.Status, UserId = user.Id });
    }
}
