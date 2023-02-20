using System.Data;
using Dapper;
using UOW_Repo_Dapper.Models;
using UOW_Repo_Dapper.Repositories.Interfaces;

namespace UOW_Repo_Dapper.Repositories;

public class ProductRepository : RepositoryBase, IProductRepository
{
    public ProductRepository(IDbTransaction transaction) : base(transaction)
    {
    }

    public IEnumerable<Product> GetAll()
    {
        var sql = "SELECT * FROM products";
        
        return Connection.Query<Product>(sql);
    }

    public void Create(Product product)
    {
        // Создание товара с помощью хранимой процедуры
        
        var sql = "create_product";
        var parameters = new DynamicParameters();
        parameters.Add("title", product.Title);
        parameters.Add("price", product.Price);

        Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
        
        Transaction.Commit();
    }
}