using Domain.Models;
using Infrastructure.ApiResponse;

namespace Infrastructure.Interfaces;

public interface IPurchaseService
{
    Response<bool> CreatePurchase(Purchase purchase);
    Response<List<Purchase>> GetAllPurchase(Purchase purchase);
    Response<Purchase> GetPurchaseById(int id);
    Response<bool> UpdatePurchase(Purchase purchase);
    Response<bool> DeletePurchase(int id);
}