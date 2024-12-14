using Domain.Models;
using Infrastructure.ApiResponse;

namespace Infrastructure.Interfaces;

public interface ILocationService
{
    Response<bool> CreateLocation(Location location);
    Response<List<Location>> GetAllLocation(Location location);
    Response<Location> GetLocationById(int id);
    Response<bool> UpdateLocation(Location location);
    Response<bool> DeleteLocation(int id);
}