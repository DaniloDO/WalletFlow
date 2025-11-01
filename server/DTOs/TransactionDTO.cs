namespace server.DTOs;

public record TransactionReadDTO(
    Guid PublicId,
    decimal Amount,
    DateTime Date,
    string Description,
    int CategoryId,
    string CategoryName,
    string UserId,
    string UserName
);

public record TransactionWriteDTO(
    decimal Amount,
    string Description,
    int CategoryId,
    Guid UserId
);