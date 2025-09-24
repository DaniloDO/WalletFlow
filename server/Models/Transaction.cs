using System;

namespace server.Models;

public class Transaction
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public string? Description { get; set; } 
    public int CategoryId { get; set; }
    public Category? Category { get; set; } 
}
