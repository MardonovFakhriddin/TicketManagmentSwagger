using Domain.Models;
using Infrastructure.ApiResponse;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketLocationController(ITicketLocationService ticketLocationService) : ControllerBase
{
    [HttpGet]
    public Response<List<TicketLocation>> GetAllTicketLocation(TicketLocation ticketLocation)
    {
        var response = ticketLocationService.GetAllTicketLocations(ticketLocation);
        return response;
    }

    [HttpPost("{id:int}")]
    public Response<TicketLocation> GetTicketLocationById(int id)
    {
        var response = ticketLocationService.GetTicketLocationById(id);
        return response;
    }

    [HttpPost]
    public Response<bool> CreateTicketLocation(TicketLocation ticketLocation)
    {
        var response = ticketLocationService.CreateTicketLocation(ticketLocation);
        return response;
    }

    [HttpPut]
    public Response<bool> UpdateTicketLocation(TicketLocation ticketLocation)
    {
        var response = ticketLocationService.UpdateTicketLocation(ticketLocation);
        return response;
    }

    [HttpDelete]
    public Response<bool> DeleteTicketLocation(int id)
    {
        var response = ticketLocationService.DeleteTicketLocation(id);
        return response;
    }
}