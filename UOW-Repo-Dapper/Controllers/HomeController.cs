using Microsoft.AspNetCore.Mvc;
using UOW_Repo_Dapper.Models;
using UOW_Repo_Dapper.Repositories.UnitOfWork;

namespace UOW_Repo_Dapper.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;

    private IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    public string TransferMoney(int sourceUserId, int targetUserId, double amount)
    {
        string result;
        bool flag = true;
        try
        {               
            if (amount <= 0)
            {
                return "Недопустимая сумма";
            }

            
            //Получить исходный баланс пользователя
            User sourceUser = _unitOfWork.UserRepository.GetUserDetails(sourceUserId);

            if (sourceUser.CurrentBalance < amount)
            {
                return "Недостаточно средств";
            }

            
            //Сумма списания
            flag &= _unitOfWork.UserRepository.Debit(sourceUserId, amount);
            flag &= _unitOfWork.UserRepository.Credit(targetUserId, amount);
            Transfer transfer = new Transfer() { 
                Id = Guid.NewGuid().ToString(), 
                SourceUserId = sourceUserId,
                TargetUserId = targetUserId,
                Amount = amount,
                Timestamp = DateTime.UtcNow
            };
            flag &= _unitOfWork.TransferRepository.AddNew(transfer);

            _unitOfWork.Commit();

            result = flag == true ? "Успешно" : "Отклонено";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Транзакция провалена");
            result = "Отклонено";
        }

        return result;
    }
}