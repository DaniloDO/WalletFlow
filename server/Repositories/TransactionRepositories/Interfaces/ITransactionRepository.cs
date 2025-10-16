using System;
using server.Models;

namespace server.Repositories.TransactionRepositories.Interfaces;

public interface ITransactionRepository
{
    Task<IEnumerable<Transaction>> GetTransactions(); 
    Task<Transaction?> GetTransaction(Guid publicId);
    Task CreateTransaction(Transaction transaction);
    Task UpdateTransaction(Transaction transaction);
    Task DeleteTransaction(Transaction transaction); 
    Task SaveChangesAsync();   
}
