using Application.Features.Models.Commands.CreateModel;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ModelsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateModelCommand createModelCommand)
    {
        Model result = await Mediator.Send(createModelCommand);
        return Created("", result);
    }
}