using System.Data;
using Dapper;
using Npgsql;
using UOW_Repo_Dapper.Models;
using UOW_Repo_Dapper.Repositories.Interfaces;

namespace UOW_Repo_Dapper.Repositories;

public class UserRepository : IUserRepository
{
    private NpgsqlConnection _sqlConnection;

    private IDbTransaction _dbTransaction;

    public UserRepository(NpgsqlConnection sqlConnection, IDbTransaction dbTransaction)
    {
        _dbTransaction = dbTransaction;
        _sqlConnection = sqlConnection;
    }

    public bool Credit(int userId, double amount)
    {
        var sql = "UPDATE Users SET CurrentBalance = CurrentBalance + @amount WHERE Id = @userId";
        return _sqlConnection.Execute(sql, new { userId = userId, amount = amount }, transaction: _dbTransaction) > 0;
    }

    public bool Debit(int userId, double amount)
    {
        var sql = "UPDATE Users SET CurrentBalance = CurrentBalance - @amount WHERE Id = @userId";
        return _sqlConnection.Execute(sql, new { userId = userId, amount = amount }, transaction: _dbTransaction) > 0;
    }

    public IEnumerable<User> GetAllUsers()
    {
        var sql = "SELECT * FROM Users";
        return _sqlConnection.Query<User>(sql);
    }

    public void Create(User user)
    {
        //todo: Тут отправляется запрос и возвращается Id только что созданного пользователя до коммита
        var sql = "INSERT INTO Users (Name, CurrentBalance) VALUES (@Name, @CurrentBalance) RETURNING Id";
        user.Id = _sqlConnection.Query<int>(sql, user, transaction: _dbTransaction).FirstOrDefault();
        
        _dbTransaction.Commit();
    }

    public User GetUserDetails(int userId)
    {
        var sql = "SELECT * FROM Users WHERE Id=@userId";
        return _sqlConnection.QueryFirst<User>(sql, new { userId = userId }, transaction: _dbTransaction);
    }
}