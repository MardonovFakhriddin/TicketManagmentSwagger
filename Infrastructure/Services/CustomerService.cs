using System.Net;
using Dapper;
using Domain.Models;
using Infrastructure.ApiResponse;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Npgsql;

namespace Infrastructure.Services;

public class CustomerService : ICustomerService
{
    private readonly DapperContext _context;

    public CustomerService(DapperContext context)
    {
        _context = context;
    }
    public Response<bool> CreateCustomer(Customer customer)
    {
        using var context = _context.Connection;
        var cmd = "insert into customers(FullName,Email,Phone) values(@FullName,@Email,@Phone)";
        var response = context.Execute(cmd,customer);
        return response > 0
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Response<bool>(HttpStatusCode.OK, "Customer successfully created");
    }

    public Response<List<Customer>> GetAllCustomer(Customer customer)
    {
        using var context = _context.Connection;
        var cmd = "Select * from customers";
        var response = context.Query<Customer>(cmd).ToList();
        return new Response<List<Customer>>(response);
    }

    public Response<Customer> GetCustomerById(int id)
    {
        using var context = _context.Connection;
        var cmd = "Select * from Customers where CutomerId = @CustomerId";
        var customer = context.QueryFirstOrDefault<Customer>(cmd, new { CutomerId = id });
        return customer == null
            ? new Response<Customer>(HttpStatusCode.NotFound, "Customer not found")
            : new Response<Customer>(customer);
    }

    public Response<bool> UpdateCustomer(Customer customer)
    {
        using var context = _context.Connection;
        var existingCustomer = GetCustomerById(customer.CustomerId).Data;
        if (existingCustomer == null)
        {
            return new Response<bool>(HttpStatusCode.NotFound, "Customer not found");
        }
        var cmd = "UPDATE Customers SET FullName=@FullName, Email=@Email, Phone=@Phone WHERE CustomerId=@CustomerId";
        var response = context.Execute(cmd, customer);
        return response > 0
            ? new Response<bool>(HttpStatusCode.InternalServerError,"Internal Server Error")
            : new Response<bool>(HttpStatusCode.OK, "Customer successfully updated");

    }

    public Response<bool> DeleteCustomer(int id)
    {
        using NpgsqlConnection context = _context.Connection;
        var existingCustomer = GetCustomerById(id).Data;
        if (existingCustomer == null)
        {
            return new Response<bool>(HttpStatusCode.NotFound,"Customer not found");
        }
        var cmd = "DELETE FROM Customers WHERE CustomerId = @CustomerId";
        int response = context.Execute(cmd, new { CustomerId = id });
        return response > 0
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Response<bool>(HttpStatusCode.OK, "Customer successfully deleted");
    }
}