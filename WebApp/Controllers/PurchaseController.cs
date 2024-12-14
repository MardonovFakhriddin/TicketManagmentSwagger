using Domain.Models;
using Infrastructure.ApiResponse;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PurchaseController(IPurchaseService purchaseService) : ControllerBase
{
    [HttpGet]
    public Response<List<Purchase>> GetAllPurchase(Purchase purchase)
    {
        var response = purchaseService.GetAllPurchase(purchase);
        return response;
    }

    [HttpPost("{id:int}")]
    public Response<Purchase> GetPurchaseById(int id)
    {
        var response = purchaseService.GetPurchaseById(id);
        return response;
    }

    [HttpPost]
    public Response<bool> CreatePurchase(Purchase purchase)
    {
        var response = purchaseService.CreatePurchase(purchase);
        return response;
    }

    [HttpPut]
    public Response<bool> UpdatePurchase(Purchase purchase)
    {
        var response = purchaseService.UpdatePurchase(purchase);
        return response;
    }

    [HttpDelete]
    public Response<bool> DeletePurchase(int id)
    {
        var response = purchaseService.DeletePurchase(id);
        return response;
    }
}