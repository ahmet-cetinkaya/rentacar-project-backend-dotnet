using Application.Features.Fuels.Commands.CreateFuel;
using Application.Features.Fuels.Commands.DeleteFuel;
using Application.Features.Fuels.Commands.UpdateFuel;
using Application.Features.Fuels.Models;
using Application.Features.Fuels.Queries.GetByIdFuel;
using Application.Features.Fuels.Queries.GetListFuel;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FuelsController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdFuelQuery getByIdFuelQuery)
    {
        Fuel result = await Mediator.Send(getByIdFuelQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] GetListFuelQuery getListFuelQuery)
    {
        FuelListModel result = await Mediator.Send(getListFuelQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateFuelCommand createFuelCommand)
    {
        Fuel result = await Mediator.Send(createFuelCommand);
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateFuelCommand updateFuelCommand)
    {
        await Mediator.Send(updateFuelCommand);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteFuelCommand deleteFuelCommand)
    {
        await Mediator.Send(deleteFuelCommand);
        return Ok();
    }
}