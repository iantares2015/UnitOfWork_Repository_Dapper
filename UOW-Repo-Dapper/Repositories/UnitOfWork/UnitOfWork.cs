using System.Data;
using UOW_Repo_Dapper.Repositories.Interfaces;

namespace UOW_Repo_Dapper.Repositories.UnitOfWork;

public class UnitOfWork : IUnitofWork, IDisposable
{
    public ITransferRepository TransferRepository { get; }
    public IUserRepository UserRepository { get; }

    private readonly IDbTransaction _dbTransaction;

    public UnitOfWork(IDbTransaction dbTransaction, ITransferRepository transferRepository, IUserRepository userRepository)
    {
        TransferRepository = transferRepository;
        UserRepository = userRepository;
        _dbTransaction = dbTransaction;
    }

    public void Commit()
    {
        try
        {
            _dbTransaction.Commit();
        }
        catch (Exception ex)
        {
            _dbTransaction.Rollback();
        }
    }

    public void Dispose()
    {
        _dbTransaction.Connection?.Close();
        _dbTransaction.Connection?.Dispose();
        _dbTransaction.Dispose();
    }
}