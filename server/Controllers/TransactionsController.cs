using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.DTos;
using server.Models;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TransactionsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactions()
    {
        var transactions = await _context.Transactions.Include(t => t.Category)
                                                .Select(t => new TransactionDTO(t.Id, t.PublicId, t.Amount, t.Date, t.Description, t.CategoryId))
                                                .ToListAsync();

        return Ok(transactions);

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TransactionDTO>> GetTransaction(Guid publicId)
    {
        var transaction = await _context.Transactions.Include(t => t.Category)
                                                     .Where(t => t.PublicId == publicId)
                                                     .Select(t => new TransactionDTO(t.Id, t.PublicId, t.Amount, t.Date, t.Description, t.CategoryId))
                                                     .FirstOrDefaultAsync();

        if (transaction is null)
            return NotFound();

        return Ok(transaction);
    }

    [HttpPost]
    public async Task<ActionResult<TransactionDTO>> CreateTransaction(TransactionDTO dto)
    {
        var transaction = new Transaction { Amount = dto.Amount, Date = dto.Date, Description = dto.Description, CategoryId = dto.CategoryId };
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync(); 

        var resultDto = new TransactionDTO(transaction.Id, transaction.PublicId, transaction.Amount, transaction.Date, transaction.Description, transaction.CategoryId);
        return CreatedAtAction(nameof(GetTransaction),new { id = transaction.Id } , resultDto); 
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTransaction(Guid publicId)
    {
        var transaction = await _context.Transactions.FindAsync(publicId);
        if (transaction is null)
            return NotFound();

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}
