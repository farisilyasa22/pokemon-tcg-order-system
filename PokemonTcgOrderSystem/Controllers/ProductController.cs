using Microsoft.AspNetCore.Mvc;
using PokemonTcgOrderSystem.Services;
using PokemonTcgOrderSystem.Models;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ReservationService _service;

    public ProductsController(ReservationService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _service.GetProductsAsync();
        return Ok(products);
    }
}