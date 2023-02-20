using UOW_Repo_Dapper.Models;

namespace UOW_Repo_Dapper.Repositories.Interfaces;

public interface IProductRepository
{
    IEnumerable<Product> GetAll();
    void Create(Product product);
}