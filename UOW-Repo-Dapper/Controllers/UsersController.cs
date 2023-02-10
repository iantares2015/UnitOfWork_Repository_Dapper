using Microsoft.AspNetCore.Mvc;
using UOW_Repo_Dapper.Models;
using UOW_Repo_Dapper.Models.ViewModels;
using UOW_Repo_Dapper.Repositories.UnitOfWork;

namespace UOW_Repo_Dapper.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class UsersController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public UsersController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_unitOfWork.UserRepository.GetAllUsers());
    }

    [HttpPost]
    public IActionResult Post(UserViewModel model)
    {
        if (ModelState.IsValid)
        {
            User user = new User()
            {
                Name = model.Name,
                CurrentBalance = model.CurrentBalance
            };
            
            _unitOfWork.UserRepository.Create(user);

            return Ok("Пользователь успешно добавлен");
        }

        return BadRequest("Ошибка! Проверьте введенные данные");
    }
}