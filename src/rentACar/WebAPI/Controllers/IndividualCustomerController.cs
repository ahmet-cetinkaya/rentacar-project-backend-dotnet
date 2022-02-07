using Application.Features.IndividualCustomers.Commands.CreateIndividualCustomer;
using Application.Features.IndividualCustomers.Commands.DeleteIndividualCustomer;
using Application.Features.IndividualCustomers.Commands.UpdateIndividualCustomer;
using Application.Features.IndividualCustomers.Models;
using Application.Features.IndividualCustomers.Queries.GetByIdIndividualCustomer;
using Application.Features.IndividualCustomers.Queries.GetListIndividualCustomer;
using Core.Application.Requests;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IndividualCustomersController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdIndividualCustomerQuery getByIdIndividualCustomerQuery)
    {
        IndividualCustomer result = await Mediator.Send(getByIdIndividualCustomerQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListIndividualCustomerQuery getListIndividualCustomerQuery = new() { PageRequest = pageRequest };
        IndividualCustomerListModel result = await Mediator.Send(getListIndividualCustomerQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateIndividualCustomerCommand createIndividualCustomerCommand)
    {
        IndividualCustomer result = await Mediator.Send(createIndividualCustomerCommand);
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateIndividualCustomerCommand updateIndividualCustomerCommand)
    {
        IndividualCustomer result = await Mediator.Send(updateIndividualCustomerCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteIndividualCustomerCommand deleteIndividualCustomerCommand)
    {
        IndividualCustomer result = await Mediator.Send(deleteIndividualCustomerCommand);
        return Ok(result);
    }
}