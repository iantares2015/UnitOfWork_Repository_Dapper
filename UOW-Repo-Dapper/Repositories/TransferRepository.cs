using System.Data;
using Dapper;
using Npgsql;
using UOW_Repo_Dapper.Models;
using UOW_Repo_Dapper.Repositories.Interfaces;

namespace UOW_Repo_Dapper.Repositories;

public class TransferRepository : ITransferRepository
{
    private NpgsqlConnection _sqlConnection;
    private IDbTransaction _dbTransaction;

    public TransferRepository(NpgsqlConnection sqlConnection, IDbTransaction dbTransaction)
    {
        _dbTransaction = dbTransaction;
        _sqlConnection = sqlConnection;
    }

    public bool AddNew(Transfer transfer)
    {
        var sql = "INSERT INTO Transfers(Id, SourceUserId, TargetUserId, Amount, Timestamp) VALUES (@Id, @SourceUserId, @TargetUserId, @Amount, @Timestamp)";
        return _sqlConnection.Execute(sql, transfer, transaction: _dbTransaction) > 0;
    }
}