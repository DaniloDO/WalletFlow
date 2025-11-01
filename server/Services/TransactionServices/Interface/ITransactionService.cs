using System;
using server.DTOs;

namespace server.Services.TransactionServices.Interface;

public interface ITransactionService
{
    public Task<IEnumerable<TransactionReadDTO>> GetTransactions(); 
    public Task<TransactionReadDTO?> GetTransaction(Guid publicId);
    public Task<TransactionReadDTO> CreateTransaction(TransactionWriteDTO dto);
    public Task<bool> UpdateTransaction(Guid publicId, TransactionReadDTO dto);
    public Task<bool> DeleteTransaction(Guid publicId);
}
