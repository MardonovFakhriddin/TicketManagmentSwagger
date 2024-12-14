using Domain.Models;
using Infrastructure.ApiResponse;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class TicketController(ITicketService ticketService) : ControllerBase
{
    [HttpGet]
    public Response<List<Ticket>> GetAllTicket(Ticket ticket)
    {
        var response = ticketService.GetAllTicket(ticket);
        return response;
    }

    [HttpPost("{id:int}")]
    public Response<Ticket> GetTicketById(int id)
    {
        var response = ticketService.GetTicketById(id);
        return response;
    }

    [HttpPost]
    public Response<bool> CreateTicket(Ticket ticket)
    {
        var response = ticketService.CreateTicket(ticket);
        return response;
    }

    [HttpPut]
    public Response<bool> UpdateTicket(Ticket ticket)
    {
        var response = ticketService.UpdateTicket(ticket);
        return response;
    }

    [HttpDelete]
    public Response<bool> DeleteTicket(int id)
    {
        var response = ticketService.DeleteTicket(id);
        return response;
    }
}