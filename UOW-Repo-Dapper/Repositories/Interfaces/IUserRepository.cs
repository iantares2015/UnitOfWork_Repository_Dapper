using UOW_Repo_Dapper.Models;

namespace UOW_Repo_Dapper.Repositories.Interfaces;

public interface IUserRepository
{
    bool Debit(int userId, double amount);

    bool Credit(int userId, double amount);

    User GetUserDetails(int userId);

    IEnumerable<User> GetAllUsers();

    void Create(User user);
}