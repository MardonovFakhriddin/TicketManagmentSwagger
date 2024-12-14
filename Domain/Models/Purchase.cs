namespace Domain.Models;

public class Purchase
{
    public int PurchaseId { get; set; }
    public int TicketId { get; set; }
    public int CustomerId { get; set; }
    public DateTime PurchaseDateTime { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}