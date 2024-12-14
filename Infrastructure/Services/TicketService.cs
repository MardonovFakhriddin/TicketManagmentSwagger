using System.Net;
using Dapper;
using Domain.Models;
using Infrastructure.ApiResponse;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Npgsql;

namespace Infrastructure.Services;

public class TicketService:ITicketService
{
    private readonly DapperContext _context;
    public TicketService(DapperContext context)
    {
        _context = context;
    }
    public Response<bool> CreateTicket(Ticket ticket)
    {
        using var context = _context.Connection;
        var cmd = "INSERT INTO tickets (Type,Title,Price,EventDateTime) values (@Type,@Title,@Price,@EventDateTime)";
        var response = context.Execute(cmd, ticket);
        return response > 0
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Response<bool>(HttpStatusCode.OK, "Ticket created");
    }

    public Response<List<Ticket>> GetAllTicket(Ticket ticket)
    {
        using var context = _context.Connection;
        var cmd = "Select * from tickets";
        var response = context.Query<Ticket>(cmd).ToList();
        return new Response<List<Ticket>>(response);
    }

    public Response<Ticket> GetTicketById(int id)
    {
        using var context = _context.Connection;
        var cmd = "Select * from tickets where TicketId = @TicketId";
        var ticket = context.QueryFirstOrDefault<Ticket>(cmd, new { TicketId = id });
        return ticket == null
            ? new Response<Ticket>(HttpStatusCode.NotFound, "Ticket not found")
            : new Response<Ticket>(ticket);
    }

    public Response<bool> UpdateTicket(Ticket ticket)
    {
        using var context = _context.Connection;
        var existingTicket = GetTicketById(ticket.TicketId).Data;
        if (existingTicket == null)
        {
            return new Response<bool>(HttpStatusCode.NotFound, "Ticket not found");
        }

        var cmd =
            "UPDATE tickets SET Type=@Type,Title=@Title,Price=@Price,EventDateTime=@EventDataTime WHERE TicketId=@TicketId";
        var response = context.Execute(cmd, ticket);
        return response > 0
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Response<bool>(HttpStatusCode.OK, "Ticket successfully updated");
    }

    public Response<bool> DeleteTicket(int id)
    {
        using NpgsqlConnection context = _context.Connection;
        var existingTicket = GetTicketById(id).Data;
        if (existingTicket == null)
        {
            return new Response<bool>(HttpStatusCode.NotFound, "Ticket not found");
        }

        var cmd = "DELETE FROM Tickets WHERE TicketId = @TicketId";
        int response = context.Execute(cmd, new { TicketId = id });
        return response > 0
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Response<bool>(HttpStatusCode.OK, "Ticket successfully deleted");
    }
}