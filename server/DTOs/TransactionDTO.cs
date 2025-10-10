namespace server.DTOs;

public record TransactionDTO(
    int Id,
    Guid PublicId, 
    decimal Amount,
    DateTime Date,
    string Description,
    int CategoryId,
    int UserId
);
