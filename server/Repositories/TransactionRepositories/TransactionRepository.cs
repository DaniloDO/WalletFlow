using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using server.Repositories.TransactionRepositories.Interfaces;

namespace server.Repositories.TransactionRepositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Transaction>> GetTransactions()
    {
        return await _context.Transactions
            .Include(t => t.User)
            .Include(t => t.Category)
            .ToListAsync();
    }

    public async Task<Transaction?> GetTransaction(Guid publicId)
    {
        var transaction = await _context.Transactions
            .Include(t => t.User)
            .Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.PublicId == publicId);

        return transaction;

    }

    public async Task CreateTransaction(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
    }

    public async Task UpdateTransaction(Transaction transaction)
    {
        _context.Transactions.Update(transaction);
    }

    public async Task DeleteTransaction(Transaction transaction)
    {
        _context.Transactions.Remove(transaction);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
