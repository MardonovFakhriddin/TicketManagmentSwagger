using Domain.Models;
using Infrastructure.ApiResponse;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationController(ILocationService locationService) : ControllerBase
{
    [HttpGet]
    public Response<List<Location>> GetAllLocation(Location location)
    {
        var response = locationService.GetAllLocation(location);
        return response;
    }

    [HttpPost("{id:int}")]
    public Response<Location> GetLocationById(int id)
    {
        var response = locationService.GetLocationById(id);
        return response;
    }

    [HttpPost]
    public Response<bool> CreateLocation(Location location)
    {
        var response = locationService.CreateLocation(location);
        return response;
    }

    [HttpPut]
    public Response<bool> UpdateLocation(Location location)
    {
        var response = locationService.UpdateLocation(location);
        return response;
    }

    [HttpDelete]
    public Response<bool> DeleteLocation(int id)
    {
        var response = locationService.DeleteLocation(id);
        return response;
    }
}