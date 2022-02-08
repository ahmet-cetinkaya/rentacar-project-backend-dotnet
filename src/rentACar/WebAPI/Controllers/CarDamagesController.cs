using Application.Features.CarDamages.Commands.CreateCarDamage;
using Application.Features.CarDamages.Commands.DeleteCarDamage;
using Application.Features.CarDamages.Commands.UpdateCarDamage;
using Application.Features.CarDamages.Models;
using Application.Features.CarDamages.Queries.GetByIdCarDamage;
using Application.Features.CarDamages.Queries.GetListCarDamage;
using Core.Application.Requests;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarDamagesController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdCarDamageQuery getByIdCarDamageQuery)
    {
        CarDamage result = await Mediator.Send(getByIdCarDamageQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCarDamageQuery getListCarDamageQuery = new() { PageRequest = pageRequest };
        CarDamageListModel result = await Mediator.Send(getListCarDamageQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateCarDamageCommand createCarDamageCommand)
    {
        CarDamage result = await Mediator.Send(createCarDamageCommand);
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCarDamageCommand updateCarDamageCommand)
    {
        CarDamage result = await Mediator.Send(updateCarDamageCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteCarDamageCommand deleteCarDamageCommand)
    {
        CarDamage result = await Mediator.Send(deleteCarDamageCommand);
        return Ok(result);
    }
}