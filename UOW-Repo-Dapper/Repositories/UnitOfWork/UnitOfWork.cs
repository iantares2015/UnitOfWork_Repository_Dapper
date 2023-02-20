using System.Data;
using Npgsql;
using UOW_Repo_Dapper.Repositories.Interfaces;

namespace UOW_Repo_Dapper.Repositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly IDbConnection _connection;
    private IDbTransaction _transaction;
    private bool _disposed;

    private UserRepository? _userRepository;
    private TransferRepository? _transferRepository;

    public UnitOfWork(string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString));
        }

        _connection = new NpgsqlConnection(connectionString);
        _connection.Open();
        _transaction = _connection.BeginTransaction();
    }
    
    public IUserRepository UserRepository => _userRepository ??= new UserRepository(_transaction);
    public ITransferRepository TransferRepository => _transferRepository ??= new TransferRepository(_transaction);
    

    public void Commit()
    {
        try
        {
            _transaction.Commit();
        }
        catch (Exception ex)
        {
            _transaction.Rollback();
            
            throw;
        }
        finally
        {
            _transaction.Dispose();
            _transaction = _connection.BeginTransaction();
            ResetRepositories();
        }
    }
    
    private void ResetRepositories()
    {
        _userRepository = null;
        _transferRepository = null;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if(disposing)
            {
                _transaction.Dispose();
                _connection.Dispose();
            }
            _disposed = true;
        }
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }
}