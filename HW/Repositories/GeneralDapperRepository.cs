
using Dapper;
using HW.Entities;
using HW.InterFaces;
using Microsoft.Data.SqlClient;

namespace HW.Repositories;

public class GeneralDapperRepository<T> : IGeneralRepository<T>
{
    protected readonly string _connectionString;

    public GeneralDapperRepository()
    {
        _connectionString = "Data Source=WIN10\\SQLEXPRESS;Initial Catalog=CW10;User ID=sa;Password=246850;TrustServerCertificate=True;Encrypt=True";
    }
    public void Add(T item)
    {
        using SqlConnection cn = new(_connectionString);
        cn.Open();
        var properties = typeof(T).GetProperties().ToList();
        string columns = string.Join(", ", properties.Select(p => p.Name));
        string values = string.Join(", ", properties.Select(p => "@" + p.Name));
        string query = $"INSERT INTO {typeof(T).Name}s ({columns}) VALUES ({values})";
        cn.Execute(query, item);
    }

    public List<T> GetAll()
    {
        using SqlConnection cn = new(_connectionString);
        cn.Open();
        string query = $"SELECT * FROM {typeof(T).Name}s";
        var cmd = new CommandDefinition(query);
        var result = cn.Query<T>(cmd);
        return result.ToList();
    }

    public T GetById(int id)
    {
        using SqlConnection cn = new(_connectionString);
        cn.Open();
        string query = $"SELECT * FROM {typeof(T).Name}s WHERE Id = @Id";
        var user = cn.QueryFirst<T>(query, new { Id = id });
        //return cn.QuerySingle<User>(query, new { Id = id });
        return user;
    }

    public bool Remove(int id)
    {
        using SqlConnection cn = new(_connectionString);
        cn.Open();
        string query = "DELETE FROM {typeof(T).Name}s WHERE Id = @Id";
        int rowAffected = cn.Execute(query, new { Id = id });
        return rowAffected != 0;
    }
}
