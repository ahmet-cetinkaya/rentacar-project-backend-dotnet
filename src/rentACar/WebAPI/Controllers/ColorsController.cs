using Application.Features.Colors.Commands.CreateColor;
using Application.Features.Colors.Commands.DeleteColor;
using Application.Features.Colors.Commands.UpdateColor;
using Application.Features.Colors.Models;
using Application.Features.Colors.Queries.GetByIdColor;
using Application.Features.Colors.Queries.GetListColor;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ColorsController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdColorQuery getByIdColorQuery)
    {
        Color result = await Mediator.Send(getByIdColorQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] GetListColorQuery getListColorQuery)
    {
        ColorListModel result = await Mediator.Send(getListColorQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateColorCommand createColorCommand)
    {
        Color result = await Mediator.Send(createColorCommand);
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateColorCommand updateColorCommand)
    {
        await Mediator.Send(updateColorCommand);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteColorCommand deleteColorCommand)
    {
        await Mediator.Send(deleteColorCommand);
        return Ok();
    }
}