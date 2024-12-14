using Domain.Models;
using Infrastructure.ApiResponse;

namespace Infrastructure.Interfaces;

public interface ICustomerService
{
    Response<bool> CreateCustomer(Customer customer);
    Response<List<Customer>> GetAllCustomer(Customer customer);
    Response<Customer> GetCustomerById(int id);
    Response<bool> UpdateCustomer(Customer customer);
    Response<bool> DeleteCustomer(int id);
}