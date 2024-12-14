using Npgsql;

namespace Infrastructure.Data;

public class DapperContext
{
    private readonly string _connectionString =
        "Host = localhost;port=5432;database = TicketManagment;username = postgres;password = LMard1909";

    public NpgsqlConnection Connection
    {
        get
        {
            return new NpgsqlConnection(_connectionString);

        }
    }
}