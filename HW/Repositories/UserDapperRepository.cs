using Dapper;
using HW.Entities;
using HW.InterFaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HW.Repositories;
public class UserDapperRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserDapperRepository()
    {
        _connectionString = "Data Source=WIN10\\SQLEXPRESS;Initial Catalog=CW10;User ID=sa;Password=246850;TrustServerCertificate=True;Encrypt=True";
    }
    public void Add(User item)
    {
        using SqlConnection cn = new(_connectionString);
        cn.Open();
        string query = "INSERT INTO Users (Id, UserName,Password,Status) VALUES (@Id, @UserName, @Password,@Status);";
        var command = new CommandDefinition(query, item);
        cn.Execute(command);

    }

    public User Get(string username)
    {
        using SqlConnection cn = new(_connectionString);
        cn.Open();
        string query = "SELECT * FROM Users WHERE UserName = @username";
        var user = cn.QueryFirst<User>(query, new { UserName = username });
        //return cn.QuerySingle<User>(query, new { Id = id });
        return user;
    }

    public List<User> GetAll()
    {
        using SqlConnection cn = new(_connectionString);
        cn.Open();
        string query = "SELECT * FROM Users";
        var cmd = new CommandDefinition(query);
        var result = cn.Query<User>(cmd);
        return result.ToList();
    }

    public User GetById(int id)
    {

        using SqlConnection cn = new(_connectionString);
        cn.Open();
        string query = "SELECT * FROM Users WHERE Id = @Id";
        var user = cn.QueryFirst<User>(query, new { Id = id });
        //return cn.QuerySingle<User>(query, new { Id = id });
        return user;


    }

    public bool Remove(int id)
    {
        using SqlConnection cn = new(_connectionString);
        cn.Open();
        string query = "DELETE FROM Users WHERE Id = @Id";
        int rowAffected = cn.Execute(query, new { Id = id });
        return rowAffected != 0;
    }

    public List<User> Search(string username)
    {
        using SqlConnection cn = new(_connectionString);
        cn.Open();
        string query = "SELECT * FROM Users WHERE UserName LIKE @Prefix";
        var users = cn.Query<User>(query, new { prefix = username + "%"}).AsList();
        return users;
    }

    public void Update(User user)
    {
        using SqlConnection cn = new(_connectionString);
        cn.Open();
        string query = "UPDATE Users SET Password = @UserPassword , Status = @UserStatus  WHERE Id = @UserId";
        cn.Execute(query, new {UserPassword = user.Password, UserStatus = user.Status, UserId = user.Id});
    }
}
