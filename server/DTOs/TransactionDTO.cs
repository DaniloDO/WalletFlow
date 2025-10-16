namespace server.DTOs;

public record SimpleTransactionDTO(
    int Id,
    Guid PublicId, 
    decimal Amount,
    DateTime Date,
    string Description,
    int CategoryId,
    int UserId
);

public record FullTransactionDTO(
    int Id,
    Guid publicId,
    decimal Amount,
    DateTime Date,
    string Description,
    int CategoryId,
    string CategoryName,
    int UserId,
    string UserName
);