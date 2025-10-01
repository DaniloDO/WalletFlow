using server.Models;

namespace server.DTos;

public record TransactionDTO(
    int Id,
    Guid PublicId, 
    decimal Amount,
    DateTime Date,
    string Description,
    int CategoryId,
    int UserId
);
