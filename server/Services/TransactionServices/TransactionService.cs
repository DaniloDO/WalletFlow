using System;
using server.DTOs;
using server.Models;
using server.Repositories.TransactionRepositories.Interfaces;
using server.Services.TransactionServices.Interface;

namespace server.Services.TransactionServices;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;

    public TransactionService(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<SimpleTransactionDTO>> GetTransactions()
    {
        var transactions = await _repository.GetTransactions();

        return transactions.Select(t => new SimpleTransactionDTO(
            t.Id,
            t.PublicId,
            t.Amount,
            t.Date,
            t.Description,
            t.CategoryId,
            t.UserId
        ));
    }

    public async Task<FullTransactionDTO?> GetTransaction(Guid publicId)
    {
        var transaction = await _repository.GetTransaction(publicId);
        if (transaction is null)
            return null;

        return new FullTransactionDTO(
            transaction.Id,
            transaction.PublicId,
            transaction.Amount,
            transaction.Date,
            transaction.Description,
            transaction.CategoryId,
            transaction.Category.Name,
            transaction.UserId,
            transaction.User.UserName
        );
    }

    public async Task<SimpleTransactionDTO> CreateTransaction(SimpleTransactionDTO dto)
    {
        var transaction = new Transaction
        {
            Amount = dto.Amount,
            Description = dto.Description,
            CategoryId = dto.CategoryId,
            UserId = dto.UserId
        };

        await _repository.CreateTransaction(transaction);
        await _repository.SaveChangesAsync();

        return new SimpleTransactionDTO(
            transaction.Id,
            transaction.PublicId,
            transaction.Amount,
            transaction.Date,
            transaction.Description,
            transaction.CategoryId,
            transaction.UserId

        );
    }

    public async Task<bool> UpdateTransaction(Guid publicId, SimpleTransactionDTO dto)
    {
        var existingTransaction = await _repository.GetTransaction(publicId);
        if (existingTransaction is null)
            return false;

        existingTransaction.Amount = dto.Amount;
        existingTransaction.Date = dto.Date;
        existingTransaction.Description = dto.Description;
        existingTransaction.CategoryId = dto.CategoryId;

        await _repository.UpdateTransaction(existingTransaction);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteTransaction(Guid publicId)
    {
        var transaction = await _repository.GetTransaction(publicId);
        if (transaction is null)
            return false;

        await _repository.DeleteTransaction(transaction);
        await _repository.SaveChangesAsync();

        return true;
    }
}
