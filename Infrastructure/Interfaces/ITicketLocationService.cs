using Domain.Models;
using Infrastructure.ApiResponse;

namespace Infrastructure.Interfaces;

public interface ITicketLocationService
{
    Response<bool> CreateTicketLocation (TicketLocation ticketLocation);
    Response<List<TicketLocation>> GetAllTicketLocations(TicketLocation ticketLocation);
    Response<TicketLocation> GetTicketLocationById(int id);
    Response<bool> UpdateTicketLocation(TicketLocation ticketLocation);
    Response<bool> DeleteTicketLocation(int id);
}