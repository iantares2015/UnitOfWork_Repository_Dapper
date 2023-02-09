using UOW_Repo_Dapper.Models;

namespace UOW_Repo_Dapper.Repositories.Interfaces
{
    public interface ITransferRepository
    {
        bool AddNew(Transfer transfer);
    }
}