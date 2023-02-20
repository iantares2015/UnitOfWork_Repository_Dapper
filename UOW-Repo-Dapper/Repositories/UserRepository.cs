using System.Data;
using Dapper;
using Npgsql;
using NpgsqlTypes;
using UOW_Repo_Dapper.Models;
using UOW_Repo_Dapper.Models.ViewModels;
using UOW_Repo_Dapper.Repositories.Interfaces;

namespace UOW_Repo_Dapper.Repositories;

public class UserRepository : RepositoryBase, IUserRepository
{
    public UserRepository(IDbTransaction transaction) : base(transaction)
    {
    }
    
    public bool Credit(int userId, double amount)
    {
        var sql = "UPDATE Users SET CurrentBalance = CurrentBalance + @amount WHERE Id = @userId";
        return Connection.Execute(sql, new { userId = userId, amount = amount }, transaction: Transaction) > 0;
    }

    public bool Debit(int userId, double amount)
    {
        var sql = "UPDATE Users SET CurrentBalance = CurrentBalance - @amount WHERE Id = @userId";
        return Connection.Execute(sql, new { userId = userId, amount = amount }, transaction: Transaction) > 0;
    }

    public IEnumerable<User> GetAllUsers()
    {
        var sql = "SELECT * FROM Users";
        return Connection.Query<User>(sql);
    }

    public void Create(User user)
    {
        // Тут отправляется запрос и возвращается Id только что созданного пользователя до коммита
        var sql = "INSERT INTO Users (Name, CurrentBalance) VALUES (@Name, @CurrentBalance) RETURNING Id";
        user.Id = Connection.Query<int>(sql, user, transaction: Transaction).FirstOrDefault();
        
        Transaction.Commit();
    }

    public User GetUserDetails(int userId)
    {
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("_val", userId);
        

        var sql = "public.assign_demo";
        
        var user = Connection.QueryFirstOrDefault<UserDetails>(sql, parameters, commandType: CommandType.StoredProcedure);


        Console.WriteLine();
        
        
        return new User();
    }
}