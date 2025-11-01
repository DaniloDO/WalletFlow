namespace server.DTOs;

public record SimpleUserReadDTO(
    Guid PublicId,
    string UserName,
    string Email,
    DateTime CreatedAt
); 

public record UserReadDTO(
    Guid PublicId,
    string UserName,
    string Email,
    DateTime CreatedAt,
    IEnumerable<TransactionReadDTO> Transactions 
); 

public record UserWriteDTO(
    string UserName,
    string Email,
    string Password
);
