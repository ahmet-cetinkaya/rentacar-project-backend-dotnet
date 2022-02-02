using Application.Features.Brands.Commands.CreateBrand;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateBrandCommand createBrandCommand)
    {
        Brand result = await Mediator.Send(createBrandCommand);
        return Created("", result);
    }
}