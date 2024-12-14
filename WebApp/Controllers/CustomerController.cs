using Domain.Models;
using Infrastructure.ApiResponse;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CustomerController(ICustomerService customerService) : ControllerBase
{
    [HttpGet]
    public Response<List<Customer>> GetAllCustomer(Customer customer)
    {
        var response = customerService.GetAllCustomer(customer);
        return response;
    }

    [HttpPost("{id:int}")]
    public Response<Customer> GetCustomerById(int id)
    {
        var response = customerService.GetCustomerById(id);
        return response;
    }

    [HttpPost]
    public Response<bool> CreateCustomer(Customer customer)
    {
        var response = customerService.CreateCustomer(customer);
        return response;
    }

    [HttpPut]
    public Response<bool> UpdateCustomer(Customer customer)
    {
        var response = customerService.UpdateCustomer(customer);
        return response;
    }

    [HttpDelete]
    public Response<bool> DeleteCustomer(int id)
    {
        var response = customerService.DeleteCustomer(id);
        return response;
    }
}