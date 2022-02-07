using Application.Features.Rentals.Commands.CreateRental;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RentalsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateRentalCommand createRentalCommand)
    {
        Rental result = await Mediator.Send(createRentalCommand);
        return Created("", result);
    }
}