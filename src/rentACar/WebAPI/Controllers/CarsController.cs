using Application.Features.Cars.Commands.CreateCar;
using Application.Features.Cars.Commands.DeleteCar;
using Application.Features.Cars.Commands.MaintainCar;
using Application.Features.Cars.Commands.UpdateCar;
using Application.Features.Cars.Models;
using Application.Features.Cars.Queries.GetByIdCar;
using Application.Features.Cars.Queries.GetListCar;
using Core.Application.Requests;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarsController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdCarQuery getByIdCarQuery)
    {
        Car result = await Mediator.Send(getByIdCarQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCarQuery getListCarQuery = new() { PageRequest = pageRequest };
        CarListModel result = await Mediator.Send(getListCarQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateCarCommand createCarCommand)
    {
        Car result = await Mediator.Send(createCarCommand);
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCarCommand updateCarCommand)
    {
        await Mediator.Send(updateCarCommand);
        return Ok();
    }

    [HttpPut("maintain")]
    public async Task<IActionResult> MaintainCar([FromBody] MaintainCarCommand maintainCarCommand)
    {
        Car result = await Mediator.Send(maintainCarCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteCarCommand deleteCarCommand)
    {
        await Mediator.Send(deleteCarCommand);
        return Ok();
    }
}