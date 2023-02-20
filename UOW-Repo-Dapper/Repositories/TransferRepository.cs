using System.Data;
using Dapper;
using Npgsql;
using UOW_Repo_Dapper.Models;
using UOW_Repo_Dapper.Models.ViewModels;
using UOW_Repo_Dapper.Repositories.Interfaces;

namespace UOW_Repo_Dapper.Repositories;

public class TransferRepository : RepositoryBase, ITransferRepository
{
    public TransferRepository(IDbTransaction transaction) : base(transaction)
    {
    }

    public bool AddNew(Transfer transfer)
    {
        var sql = "INSERT INTO Transfers(Id, SourceUserId, TargetUserId, Amount, Timestamp) VALUES (@Id, @SourceUserId, @TargetUserId, @Amount, @Timestamp)";
        return Connection.Execute(sql, transfer, transaction: Transaction) > 0;
    }

    public IEnumerable<TransactionViewModel> GetAllTransactionsByUserId(int userId)
    {
        // Получаем данные с помощью хранимой функции
        
        var sql = "SELECT * FROM public.get_transactions_by_user_id(@user_id)";
        var transactions = Connection.Query<TransactionViewModel>(sql, new {user_id = userId});
        
        return transactions;
    }
}