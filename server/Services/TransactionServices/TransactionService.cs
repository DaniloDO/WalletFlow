using System;
using server.DTOs;
using server.Models;
using server.Repositories.CategoryRepositories.Interfaces;
using server.Repositories.TransactionRepositories.Interfaces;
using server.Repositories.UserRepositories.Interfaces;
using server.Services.TransactionServices.Interface;

namespace server.Services.TransactionServices;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUserRepository _userRepository;

    public TransactionService(ITransactionRepository transactionRepository, ICategoryRepository categoryRepository, IUserRepository userRepository)
    {
        _transactionRepository = transactionRepository;
        _categoryRepository = categoryRepository;
        _userRepository = userRepository; 
    }

    public async Task<IEnumerable<TransactionReadDTO>> GetTransactions()
    {
        var transactions = await _transactionRepository.GetTransactions();

        return transactions.Select(t => new TransactionReadDTO(
            t.PublicId,
            t.Amount,
            t.Date,
            t.Description,
            t.CategoryId,
            t.Category.Name,
            t.UserId,
            t.User.UserName
        ));
    }

    public async Task<TransactionReadDTO?> GetTransaction(Guid publicId)
    {
        var transaction = await _transactionRepository.GetTransaction(publicId);
        if (transaction is null)
            return null;

        return new TransactionReadDTO(
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

    public async Task<TransactionReadDTO> CreateTransaction(TransactionWriteDTO dto)
    {
        var user = await _userRepository.GetUser(dto.UserId); 
        var category = await _categoryRepository.GetCategory(dto.CategoryId); 
        var transaction = new Transaction
        {
            Amount = dto.Amount,
            Description = dto.Description,
            CategoryId = category.Id,
            Category = category,
            UserId = user.Id,
            User = user
        };

        await _transactionRepository.CreateTransaction(transaction);
        await _transactionRepository.SaveChangesAsync();

        return new TransactionReadDTO(
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

    public async Task<bool> UpdateTransaction(Guid publicId, TransactionReadDTO dto)
    {
        var existingTransaction = await _transactionRepository.GetTransaction(publicId);
        if (existingTransaction is null)
            return false;

        existingTransaction.Amount = dto.Amount;
        existingTransaction.Date = dto.Date;
        existingTransaction.Description = dto.Description;
        existingTransaction.CategoryId = dto.CategoryId;

        await _transactionRepository.UpdateTransaction(existingTransaction);
        await _transactionRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteTransaction(Guid publicId)
    {
        var transaction = await _transactionRepository.GetTransaction(publicId);
        if (transaction is null)
            return false;

        await _transactionRepository.DeleteTransaction(transaction);
        await _transactionRepository.SaveChangesAsync();

        return true;
    }
}
