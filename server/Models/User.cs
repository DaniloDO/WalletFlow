using System;

namespace server.Models;

public class User
{
    public int Id { get; set; }
    public Guid PublicId { get; set; } = Guid.NewGuid();
    public string UserName { get; set; } 
    public string Email { get; set; } 
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 

    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>(); 
}
