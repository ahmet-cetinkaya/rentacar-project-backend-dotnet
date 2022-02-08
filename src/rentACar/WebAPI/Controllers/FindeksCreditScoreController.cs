using Application.Features.FindeksCreditRates.Commands.CreateFindeksCreditRate;
using Application.Features.FindeksCreditRates.Commands.DeleteFindeksCreditRate;
using Application.Features.FindeksCreditRates.Commands.UpdateFindeksCreditRate;
using Application.Features.FindeksCreditRates.Commands.UpdateFindeksCreditRateFromService;
using Application.Features.FindeksCreditRates.Dtos;
using Application.Features.FindeksCreditRates.Models;
using Application.Features.FindeksCreditRates.Queries.GetByIdFindeksCreditRate;
using Application.Features.FindeksCreditRates.Queries.GetListFindeksCreditRate;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FindeksCreditRatesController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdFindeksCreditRateQuery getByIdFindeksCreditRateQuery)
    {
        FindeksCreditRateDto result = await Mediator!.Send(getByIdFindeksCreditRateQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListFindeksCreditRateQuery getListFindeksCreditRateQuery = new() { PageRequest = pageRequest };
        FindeksCreditRateListModel result = await Mediator!.Send(getListFindeksCreditRateQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateFindeksCreditRateCommand createFindeksCreditRateCommand)
    {
        CreatedFindeksCreditRateDto result = await Mediator!.Send(createFindeksCreditRateCommand);
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateFindeksCreditRateCommand updateFindeksCreditRateCommand)
    {
        UpdatedFindeksCreditRateDto result = await Mediator!.Send(updateFindeksCreditRateCommand);
        return Ok(result);
    }

    [HttpPut("FromService")]
    public async Task<IActionResult> UpdateFromService(
        [FromBody] UpdateFindeksCreditRateFromServiceCommand findeksCreditRateFromServiceCommand)
    {
        UpdatedFindeksCreditRateDto result = await Mediator!.Send(findeksCreditRateFromServiceCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteFindeksCreditRateCommand deleteFindeksCreditRateCommand)
    {
        DeletedFindeksCreditRateDto result = await Mediator!.Send(deleteFindeksCreditRateCommand);
        return Ok(result);
    }
}