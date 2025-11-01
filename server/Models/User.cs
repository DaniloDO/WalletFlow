using System;
using Microsoft.AspNetCore.Identity;

namespace server.Models;

public class User : IdentityUser 
{
    public Guid PublicId { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 

    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>(); 
}
