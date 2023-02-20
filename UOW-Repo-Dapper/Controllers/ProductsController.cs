using Microsoft.AspNetCore.Mvc;
using UOW_Repo_Dapper.Models;
using UOW_Repo_Dapper.Repositories.UnitOfWork;

namespace UOW_Repo_Dapper.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_unitOfWork.ProductRepository.GetAll());
    }

    [HttpPost]
    public IActionResult Create(Product product)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.ProductRepository.Create(product);

            return Ok("Продукт успешно добавлен");
        }

        return BadRequest();
    }
}