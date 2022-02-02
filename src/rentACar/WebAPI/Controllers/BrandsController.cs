using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Models;
using Application.Features.Brands.Queries.GetListBrand;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandsController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] GetListBrandQuery getListBrandQuery)
    {
        BrandListModel result = await Mediator.Send(getListBrandQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateBrandCommand createBrandCommand)
    {
        Brand result = await Mediator.Send(createBrandCommand);
        return Created("", result);
    }
}