using System.Net;
using Dapper;
using Domain.Models;
using Infrastructure.ApiResponse;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Npgsql;

namespace Infrastructure.Services;

public class LocationService :ILocationService
{
    private readonly DapperContext _context;
    public LocationService(DapperContext context)
    {
        _context = context;
    }
    public Response<bool> CreateLocation(Location location)
    {
        using var context = _context.Connection;
        var cmd = "INSERT INTO locations (City,Address,LocationType) values (@city,@address,@LocationType)";
        var response = context.Execute(cmd, location);
        return response > 0
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Response<bool>(HttpStatusCode.OK, "Location created");
    }

    public Response<List<Location>> GetAllLocation(Location location)
    {
        using var context = _context.Connection;
        var cmd = "Select * from location";
        var response = context.Query<Location>(cmd).ToList();
        return new Response<List<Location>>(response);    }

    public Response<Location> GetLocationById(int id)
    {
        using var context = _context.Connection;
        var cmd = "Select * from locations where LocationId = @LocationId";
        var location = context.QueryFirstOrDefault<Location>(cmd, new { CutomerId = id });
        return location == null
            ? new Response<Location>(HttpStatusCode.NotFound, "Location not found")
            : new Response<Location>(location);
    }

    public Response<bool> UpdateLocation(Location location)
    {
        using var context = _context.Connection;
        var existingLocation = GetLocationById(location.LocationId).Data;
        if (existingLocation == null)
        {
            return new Response<bool>(HttpStatusCode.NotFound, "Location not found");
        }
        var cmd = "UPDATE Locations SET City=@City, Address=@Address, LocationType=@LocationType WHERE LocationId=@LocationId";
        var response = context.Execute(cmd, location);
        return response > 0
            ? new Response<bool>(HttpStatusCode.InternalServerError,"Internal Server Error")
            : new Response<bool>(HttpStatusCode.OK, "Location successfully updated");
    }

    public Response<bool> DeleteLocation(int id)
    {
        using NpgsqlConnection context = _context.Connection;
        var existingLocation = GetLocationById(id).Data;
        if (existingLocation == null)
        {
            return new Response<bool>(HttpStatusCode.NotFound,"Location not found");
        }
        var cmd = "DELETE FROM Locations WHERE LocationId = @LocationId";
        int response = context.Execute(cmd, new { LocationId = id });
        return response > 0
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Response<bool>(HttpStatusCode.OK, "Location successfully deleted");
    }
}