using UOW_Repo_Dapper.Repositories.Interfaces;

namespace UOW_Repo_Dapper.Repositories.UnitOfWork;

public interface IUnitOfWork
{
    ITransferRepository TransferRepository { get; }
    IUserRepository UserRepository { get; }

    void Commit();
}