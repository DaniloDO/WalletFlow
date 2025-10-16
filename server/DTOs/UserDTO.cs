namespace server.DTOs;

public record SimpleUserReadDTO(
    int Id,
    Guid PublicId,
    string UserName,
    string Email,
    DateTime CreatedAt
); 

public record UserReadDTO(
    int Id,
    Guid PublicId,
    string UserName,
    string Email,
    DateTime CreatedAt,
    IEnumerable<SimpleTransactionDTO> Transactions 
); 

public record UserWriteDTO(
    string UserName,
    string Email,
    string Password
);
