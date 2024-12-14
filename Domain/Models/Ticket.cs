namespace Domain.Models;

public class Ticket
{
    public int TicketId { get; set; }
    public string Type { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public DateTime EventDateTime { get; set; }
}