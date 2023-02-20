using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
    private readonly Numb _numb;

    public UsersController(IUnitOfWork unitOfWork, IOptionsMonitor<Numb> numb)
    {
        _unitOfWork = unitOfWork;
        _numb = numb.CurrentValue;
    }

    [HttpGet]
    public IActionResult Get()
    {
        //IOptionsMonitor
        Console.WriteLine($"The num is {_numb.Number}");

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

    [HttpGet("Test")]
    public IActionResult TimeTest()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        // some f***ing logic
        stopwatch.Stop();

        return Ok();
    }

    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        var user = _unitOfWork.UserRepository.GetUserDetails(id);

        return Ok(user);
    }
}