using UOW_Repo_Dapper.Models;
using UOW_Repo_Dapper.Models.ViewModels;

namespace UOW_Repo_Dapper.Repositories.Interfaces
{
    public interface ITransferRepository
    {
        bool AddNew(Transfer transfer);
        IEnumerable<TransactionViewModel> GetAllTransactionsByUserId(int userId);
    }
}