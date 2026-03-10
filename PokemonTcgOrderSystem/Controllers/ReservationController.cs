using Microsoft.AspNetCore.Mvc;
using PokemonTcgOrderSystem.Models;
using PokemonTcgOrderSystem.Services;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly ReservationService _service;

    // Constructor injection
    public ReservationsController(ReservationService service)
    {
        _service = service; // now _service is defined
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation([FromBody] ReservationRequest request)
    {
        try
        {
            var reservation = await _service.CreateReservationAsync(request.ProductId, request.UserId);
            return Ok(reservation);// Returns EF model with Id, Status, CreatedAt
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}