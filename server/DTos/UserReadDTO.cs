namespace server.DTos;

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
    IEnumerable<TransactionDTO> Transactions 
); 
