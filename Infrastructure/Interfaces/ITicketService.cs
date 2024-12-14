using Domain.Models;
using Infrastructure.ApiResponse;

namespace Infrastructure.Interfaces;

public interface ITicketService
{
    Response<bool> CreateTicket(Ticket ticket);
    Response<List<Ticket>> GetAllTicket(Ticket ticket);
    Response<Ticket> GetTicketById(int id);
    Response<bool> UpdateTicket(Ticket ticket);
    Response<bool> DeleteTicket(int id);
}