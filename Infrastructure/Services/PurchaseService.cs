using System.Net;
using Dapper;
using Domain.Models;
using Infrastructure.ApiResponse;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Npgsql;

namespace Infrastructure.Services;

public class PurchaseService : IPurchaseService
{
    private readonly DapperContext _context;

    public PurchaseService(DapperContext context)
    {
        _context = context;
    }

    public Response<bool> CreatePurchase(Purchase purchase)
    {
        using var context = _context.Connection;
        var cmd =
            "INSERT INTO purchases (TicketId,CustomerId,PurchaseDateTime,Quantity,TotalPrice) values (@TicketId,@CustomerId,@PurchaseDateTime,@Quantity,@TotalPrice)";
        var response = context.Execute(cmd, purchase);
        return response > 0
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Response<bool>(HttpStatusCode.OK, "Purchase created");
    }

    public Response<List<Purchase>> GetAllPurchase(Purchase purchase)
    {
        using var context = _context.Connection;
        var cmd = "Select * from purchases";
        var response = context.Query<Purchase>(cmd).ToList();
        return new Response<List<Purchase>>(response);
    }

    public Response<Purchase> GetPurchaseById(int id)
    {
        using var context = _context.Connection;
        var cmd = "Select * from purchases where PurchaseId = @PurchaseId";
        var purchase = context.QueryFirstOrDefault<Purchase>(cmd, new { PurchaseId = id });
        return purchase == null
            ? new Response<Purchase>(HttpStatusCode.NotFound, "Purchase not found")
            : new Response<Purchase>(purchase);
    }

    public Response<bool> UpdatePurchase(Purchase purchase)
    {
        using var context = _context.Connection;
        var existingPurchase = GetPurchaseById(purchase.PurchaseId).Data;
        if (existingPurchase == null)
        {
            return new Response<bool>(HttpStatusCode.NotFound, "Purchase not found");
        }

        var cmd =
            "UPDATE Purchases SET TicketId=@TicketId,CustomerId=@CustomerId,PurchaseDateTime=@PurchaseDateTime,Quantity=@Quantity,TotalPrice=@TotalPrice WHERE PurchaseId=@PurchaseId";
        var response = context.Execute(cmd, purchase);
        return response > 0
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Response<bool>(HttpStatusCode.OK, "Purchase successfully updated");
    }

    public Response<bool> DeletePurchase(int id)
    {
        using NpgsqlConnection context = _context.Connection;
        var existingPurchase = GetPurchaseById(id).Data;
        if (existingPurchase == null)
        {
            return new Response<bool>(HttpStatusCode.NotFound, "Purchase not found");
        }

        var cmd = "DELETE FROM Purchases WHERE PurchaseId = @PurchaseId";
        int response = context.Execute(cmd, new { PurchaseId = id });
        return response > 0
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Response<bool>(HttpStatusCode.OK, "Purchase successfully deleted");
    }
}