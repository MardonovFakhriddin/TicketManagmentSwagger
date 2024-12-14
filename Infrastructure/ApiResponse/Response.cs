using System.Net;

namespace Infrastructure.ApiResponse;

public class Response<T>
{
    public int StatusCode { get; set; }
    public T? Data { get; set; }
    public string Message { get; set; }

    public Response(T data)
    {
        Data = data;
        StatusCode = 200;
        Message = "";
    }

    public Response(HttpStatusCode statusCode, string message)
    {
        Data = default;
        StatusCode = (int) statusCode;
        Message = message;
    }
}