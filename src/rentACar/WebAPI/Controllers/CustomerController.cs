using Application.Features.Customers.Commands.CreateCustomer;
using Application.Features.Customers.Commands.DeleteCustomer;
using Application.Features.Customers.Commands.UpdateCustomer;
using Application.Features.Customers.Models;
using Application.Features.Customers.Queries.GetByIdCustomer;
using Application.Features.Customers.Queries.GetListCustomer;
using Core.Application.Requests;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdCustomerQuery getByIdCustomerQuery)
    {
        Customer result = await Mediator.Send(getByIdCustomerQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCustomerQuery getListCustomerQuery = new() { PageRequest = pageRequest };
        CustomerListModel result = await Mediator.Send(getListCustomerQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateCustomerCommand createCustomerCommand)
    {
        Customer result = await Mediator.Send(createCustomerCommand);
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCustomerCommand updateCustomerCommand)
    {
        Customer result = await Mediator.Send(updateCustomerCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteCustomerCommand deleteCustomerCommand)
    {
        Customer result = await Mediator.Send(deleteCustomerCommand);
        return Ok(result);
    }
}