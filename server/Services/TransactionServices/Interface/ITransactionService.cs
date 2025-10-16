using System;
using server.DTOs;

namespace server.Services.TransactionServices.Interface;

public interface ITransactionService
{
    public Task<IEnumerable<SimpleTransactionDTO>> GetTransactions(); 
    public Task<FullTransactionDTO?> GetTransaction(Guid publicId);
    public Task<SimpleTransactionDTO> CreateTransaction(SimpleTransactionDTO dto);
    public Task<bool> UpdateTransaction(Guid publicId, SimpleTransactionDTO dto);
    public Task<bool> DeleteTransaction(Guid publicId);
}
