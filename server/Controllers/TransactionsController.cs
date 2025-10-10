using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.DTOs;
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
        // var transactions = await _context.Transactions.Include(t => t.Category)
        //                                         .Select(t => new TransactionDTO(t.Id, t.PublicId, t.Amount, t.Date, t.Description, t.CategoryId))
        //                                         .ToListAsync();

        var transactions = await _context.Transactions
            .Include(t => t.Category)
            .Select(t => new TransactionDTO(
                t.Id,
                t.PublicId,
                t.Amount,
                t.Date,
                t.Description,
                t.CategoryId,
                t.UserId))
            .ToListAsync(); 

        return Ok(transactions);

    }

    [HttpGet("{publicId}")]
    public async Task<ActionResult<TransactionDTO>> GetTransaction(Guid publicId)
    {
        // var transaction = await _context.Transactions.Include(t => t.Category)
        //                                              .Where(t => t.PublicId == publicId)
        //                                              .Select(t => new TransactionDTO(t.Id, t.PublicId, t.Amount, t.Date, t.Description, t.CategoryId))
        //                                              .FirstOrDefaultAsync();

        var transaction = await _context.Transactions
            .Include(t => t.Category)
            .Include(t => t.User)
            .Where(t => t.PublicId == publicId)
            .Select(t => new TransactionDTO(
                t.Id,
                t.PublicId,
                t.Amount,
                t.Date,
                t.Description,
                t.CategoryId,
                t.UserId))
            .FirstOrDefaultAsync();
        
        if (transaction is null)
            return NotFound();

        return Ok(transaction);
    }

    [HttpPost]
    public async Task<ActionResult<TransactionDTO>> CreateTransaction(TransactionDTO dto)
    {
        // var transaction = new Transaction { Amount = dto.Amount, Date = dto.Date, Description = dto.Description, CategoryId = dto.CategoryId };
        
        var transaction = new Transaction 
        {
            Amount = dto.Amount,
            Date = dto.Date,
            Description = dto.Description,
            CategoryId = dto.CategoryId,
            UserId = dto.UserId
        };

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync(); 

        // var resultDto = new TransactionDTO(transaction.Id, transaction.PublicId, transaction.Amount, transaction.Date, transaction.Description, transaction.CategoryId);
        var resultDto = new TransactionDTO(
            transaction.Id,
            transaction.PublicId,
            transaction.Amount,
            transaction.Date,
            transaction.Description,
            transaction.CategoryId,
            transaction.UserId
        ); 
        return CreatedAtAction(nameof(GetTransaction),new { publicId = transaction.PublicId } , resultDto); 
    }

    [HttpDelete("{publicId}")]
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
