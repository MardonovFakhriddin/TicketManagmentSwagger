using System.Net;
using Dapper;
using Domain.Models;
using Infrastructure.ApiResponse;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Npgsql;

namespace Infrastructure.Services;

public class TicketLocationService :ITicketLocationService
{
    private readonly DapperContext _context;
    public TicketLocationService(DapperContext context)
    {
        _context = context;
    }
    public Response<bool> CreateTicketLocation(TicketLocation ticketLocation)
    {
        using var context = _context.Connection;
        var cmd = "INSERT INTO ticketlocations (TicketId,LocationId) values (@TicketId,@LocationId)";
        var response = context.Execute(cmd, ticketLocation);
        return response > 0
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Response<bool>(HttpStatusCode.OK, "TicketLocation created");
    }

    public Response<List<TicketLocation>> GetAllTicketLocations(TicketLocation ticketLocation)
    {
        using var context = _context.Connection;
        var cmd = "Select * from ticketlocations";
        var response = context.Query<TicketLocation>(cmd).ToList();
        return new Response<List<TicketLocation>>(response);
    }

    public Response<TicketLocation> GetTicketLocationById(int id)
    {
        using var context = _context.Connection;
        var cmd = "Select * from ticketlocations where Id = @Id";
        var ticketLocation = context.QueryFirstOrDefault<TicketLocation>(cmd, new { TicketId = id });
        return ticketLocation == null
            ? new Response<TicketLocation>(HttpStatusCode.NotFound, "ticketLocation not found")
            : new Response<TicketLocation>(ticketLocation);
    }

    public Response<bool> UpdateTicketLocation(TicketLocation ticketLocation)
    {
        using var context = _context.Connection;
        var existingTicketLocation = GetTicketLocationById(ticketLocation.TicketId).Data;
        if (existingTicketLocation == null)
        {
            return new Response<bool>(HttpStatusCode.NotFound, "TicketLocation not found");
        }
        var cmd = "UPDATE ticketlocations SET TicketId=@TicketId,LocationId=@LocationId WHERE TicketId=@TicketId";
        var response = context.Execute(cmd, ticketLocation);
        return response > 0
            ? new Response<bool>(HttpStatusCode.InternalServerError,"Internal Server Error")
            : new Response<bool>(HttpStatusCode.OK, "TicketLocation successfully updated");
    }

    public Response<bool> DeleteTicketLocation(int id)
    {
        using NpgsqlConnection context = _context.Connection;
        var existingTicketLocation = GetTicketLocationById(id).Data;
        if (existingTicketLocation == null)
        {
            return new Response<bool>(HttpStatusCode.NotFound,"TicketLocation not found");
        }
        var cmd = "DELETE FROM ticketlocations WHERE PurchaseId = @PurchaseId";
        int response = context.Execute(cmd, new { PurchaseId = id });
        return response > 0
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Response<bool>(HttpStatusCode.OK, "TicketLocation successfully deleted");
    }
}