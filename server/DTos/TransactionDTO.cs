namespace server.DTos;

public record TransactionDTO(
    int Id,
    decimal Amount,
    DateTime Date,
    string Description,
    int CategoryId
);
